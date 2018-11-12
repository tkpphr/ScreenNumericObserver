/*
   Copyright 2018 tkpphr

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using Newtonsoft.Json;
using ScreenNumericObserver.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ScreenNumericObserver.Core
{
	public class Observer : IDisposable
	{
		public event Action<Observer,decimal,Bitmap> Recognized;
		public event Action<Observer,Bitmap> Unrecognized;
		public event Action<Observer> Started;
		public event Action<Observer> Stopped;
		public string Title { get; set; }
		public int CaptureX { get; set; }
		public int CaptureY { get; set; }
		public int CaptureWidth { get; set; }
		public int CaptureHeight { get; set; }
		public float CaptureScale { get; set; }
		public bool CaptureGrayScale { get; set; }
		public bool NotifyEnabled { get; set; }
		public int UpdateInterval { get; set; }
		public bool IsObserving { get; private set; }
		public string Log => LogBuilder.ToString();
		public string AreaInfo => AreaCaptured ? string.Format("{0}\nX:{1},Y:{2}\nW:{3},H:{4}",Resources.Captured, CaptureX, CaptureY, CaptureWidth, CaptureHeight) : Resources.NoCapture;
		public bool AreaCaptured => CaptureWidth > 0 && CaptureHeight > 0;
		private Bitmap Capture { get; set; }
		private decimal Value { get; set; }
		private Timer UpdateTimer { get; set; }
		private TesseractEngine TesseractEngine { get; set; }
		private SpeechSynthesizer SpeechSynthesizer { get; set; }
		private StringBuilder LogBuilder { get; set; }
		private Prompt SpeechMessage { get; set; }

		public Observer(TesseractEngine tesseractEngine, SpeechSynthesizer speechSynthesizer)
			: this(tesseractEngine,speechSynthesizer,new ObserverSettings() { Title = "", CaptureScale = 1.0f, CaptureGrayScale = false, UpdateInterval = 30000, NotifyEnabled = true })
		{
			
		}

		public Observer(TesseractEngine tesseractEngine,SpeechSynthesizer speechSynthesizer,ObserverSettings settings)
		{
			Title = settings.Title;
			CaptureX = settings.CaptureX;
			CaptureY = settings.CaptureY;
			CaptureWidth = settings.CaptureWidth;
			CaptureHeight = settings.CaptureHeight;
			CaptureScale = settings.CaptureScale;
			CaptureGrayScale = settings.CaptureGrayScale;
			UpdateInterval = settings.UpdateInterval;
			NotifyEnabled = settings.NotifyEnabled;
			TesseractEngine = tesseractEngine;
			SpeechSynthesizer = speechSynthesizer;
			LogBuilder = new StringBuilder();
			UpdateTimer = new Timer();
			UpdateTimer.Tick += UpdateTimer_Tick;
			IsObserving = false;
		}

		public void Dispose()
		{
			Stop();
			UpdateTimer.Dispose();
			Capture?.Dispose();
		}

		public void Start()
		{
			if(IsObserving)
			{
				return;
			}
			UpdateTimer.Interval = UpdateInterval;
			UpdateTimer.Start();
			LogBuilder.AppendLine(string.Format("[{0}] : {1}", DateTime.Now.ToString(), Resources.StartObserve));
			IsObserving = true;
			Started?.Invoke(this);
			Update();
		}

		public void Stop()
		{
			if (!IsObserving)
			{
				return;
			}
			UpdateTimer.Stop();
			LogBuilder.AppendLine(string.Format("[{0}] : {1}", DateTime.Now.ToString(), Resources.StopObserve));
			IsObserving = false;
			Stopped?.Invoke(this);
		}

		public void ClearLog()
		{
			LogBuilder.Clear();
		}

		private void UpdateTimer_Tick(object sender, EventArgs e)
		{
			Update();
		}

		private void Update()
		{
			Capture?.Dispose();
			Capture = BitmapFactory.CreateScreenCapture(CaptureX, CaptureY, CaptureWidth, CaptureHeight, CaptureGrayScale, CaptureScale);
			using (var page = TesseractEngine.Process(Capture,PageSegMode.SingleChar|PageSegMode.SingleLine))
			{
				decimal value;
				if (decimal.TryParse(page.GetText(), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
				{
					Value = value;
					LogBuilder.AppendLine(string.Format("[{0}] : {1}", DateTime.Now.ToString(), value));
					SpeechMessage = new Prompt(string.Format("{0}\n{1}", Title, value));
					
					Recognized?.Invoke(this,value, Capture);
				}
				else
				{
					LogBuilder.AppendLine(string.Format("[{0}] : {1}", DateTime.Now.ToString(), Resources.Unrecognized));
					SpeechMessage=new Prompt(string.Format("{0}\n{1}", Title, Resources.Unrecognized));
					Unrecognized?.Invoke(this,Capture);
				}
				if (NotifyEnabled)
				{
					SpeechSynthesizer.SpeakAsync(SpeechMessage);
				}
			}

		}

	}
}
