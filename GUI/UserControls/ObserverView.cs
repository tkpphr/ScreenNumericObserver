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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenNumericObserver.Core;
using System.Speech.Synthesis;
using ScreenNumericObserver.GUI.Forms;
using ScreenNumericObserver.Properties;
using System.IO;

namespace ScreenNumericObserver.GUI.UserControls
{
	public partial class ObserverView : UserControl
	{
		private Observer Observer { get; set; }
		public event Action<string> TitleChanged; 

		public ObserverView()
		{
			InitializeComponent();
		}

		public ObserverView(Observer observer)
			: this()
		{
			Observer = observer;
			Observer.Started += Observer_Started;
			Observer.Stopped += Observer_Stopped;
			Observer.Recognized += Observer_Recognized;
			Observer.Unrecognized += Observer_Unrecognized;
			CaptureMessageLabel.Text = "";
			RefreshView();
		}

		private void Observer_Started(Observer observer)
		{
			RefreshView();
			UpdateLog();
		}

		private void Observer_Stopped(Observer observer)
		{
			RefreshView();
			UpdateLog();
		}

		private void Observer_Recognized(Observer observer, decimal value, Bitmap capture)
		{
			UpdateCapture(value.ToString(), capture);
			UpdateLog();
		}

		private void Observer_Unrecognized(Observer observer, Bitmap capture)
		{
			UpdateCapture(Resources.Unrecognized, capture);
			UpdateLog();
		}

		private void StartButton_Click(object sender, EventArgs e)
		{
			Observer.Start();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{	
			Observer.Stop();
		}

		private void NotifyCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Observer.NotifyEnabled = NotifyCheckBox.Checked;
		}

		private void TitleTextBox_TextChanged(object sender, EventArgs e)
		{
			Observer.Title = TitleTextBox.Text;
			TitleChanged?.Invoke(TitleTextBox.Text);
		}

		private void CaptureButton_Click(object sender, EventArgs e)
		{
			Screen selectedScreen;
			if (Screen.AllScreens.Length > 1)
			{
				using (var dialog = new SelectScreenDialog())
				{
					dialog.ShowDialog(ParentForm);
					selectedScreen = dialog.SelectedScreen; 
				}
			}
			else
			{
				selectedScreen = Screen.PrimaryScreen;
			}
			using (var dialog = new CaptureAreaDialog(selectedScreen))
			{
				ParentForm.Hide();
				dialog.ShowDialog(ParentForm);
				ParentForm.Show();
				var area = dialog.CaptureArea;
				if (area.Width == 0 || area.Height == 0)
				{
					return;
				}
				Observer.CaptureX = area.X;
				Observer.CaptureY = area.Y;
				Observer.CaptureWidth = area.Width;
				Observer.CaptureHeight = area.Height;
				RefreshView();
			}
		}

		private void ConfirmButton_Click(object sender, EventArgs e)
		{
			var screen = Screen.FromPoint(new Point(Observer.CaptureX,Observer.CaptureY));
			var area = new Rectangle(Observer.CaptureX, Observer.CaptureY, Observer.CaptureWidth, Observer.CaptureHeight);
			if (screen.Bounds.IntersectsWith(area))
			{
				using (var dialog = new ConfirmAreaDialog(screen, area))
				{
					ParentForm.Hide();
					dialog.ShowDialog(ParentForm);
					ParentForm.Show();
				}
			}
			else
			{
				using (var centerAligner = new DialogCenterAligner(ParentForm))
				{
					MessageBox.Show(ParentForm, Resources.AreaOutOfRange, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void CaptureGrayScaleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Observer.CaptureGrayScale = CaptureGrayScaleCheckBox.Checked;
		}

		private void CaptureScaleUpDown_ValueChanged(object sender, EventArgs e)
		{
			Observer.CaptureScale = (float)CaptureScaleUpDown.Value;
		}

		private void UpdateIntervalUpDown_ValueChanged(object sender, EventArgs e)
		{
			Observer.UpdateInterval = decimal.ToInt32(UpdateIntervalUpDown.Value * 1000);
		}

		private void SaveLogButton_Click(object sender, EventArgs e)
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = Resources.Save;
				dialog.Filter = Resources.LogFileFilter;
				dialog.RestoreDirectory = true;
				dialog.FileName = Observer.Title;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						File.WriteAllText(dialog.FileName, Observer.Log);
					}
					catch (Exception exception)
					{
						Console.WriteLine(exception);
						using (var centerAligner = new DialogCenterAligner(ParentForm))
						{
							MessageBox.Show(Resources.FailedSaveFile, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void ClearLogButton_Click(object sender, EventArgs e)
		{
			Observer.ClearLog();
			UpdateLog();
		}

		private void WordWrapCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			LogTextBox.WordWrap = WordWrapCheckBox.Checked;
		}

		private void RefreshView()
		{
			if (Observer.IsObserving)
			{
				StopButton.Enabled = true;
				StartButton.Enabled = false;
				TitleTextBox.Enabled = false;
				CaptureButton.Enabled = false;
				CaptureGrayScaleCheckBox.Enabled = false;
				CaptureScaleUpDown.Enabled = false;
				UpdateIntervalUpDown.Enabled = false;
			}
			else
			{
				StopButton.Enabled = false;
				StartButton.Enabled = true;
				TitleTextBox.Enabled = true;
				CaptureButton.Enabled = true;
				CaptureGrayScaleCheckBox.Enabled = true;
				CaptureScaleUpDown.Enabled = true;
				UpdateIntervalUpDown.Enabled = true;
				NotifyCheckBox.Checked = Observer.NotifyEnabled;
				TitleTextBox.Text = Observer.Title;
				CaptureGrayScaleCheckBox.Checked = Observer.CaptureGrayScale;
				CaptureScaleUpDown.Value = (decimal)Observer.CaptureScale;
				UpdateIntervalUpDown.Value = Observer.UpdateInterval / 1000;
				if (Observer.AreaCaptured)
				{
					StartButton.Enabled = true;
					StopButton.Enabled = false;
					ConfirmButton.Enabled = true;
				}
				else
				{
					StartButton.Enabled = false;
					StopButton.Enabled = false;
					ConfirmButton.Enabled = false;
				}
				CaptureAreaLabel.Text = Observer.AreaInfo;
			}
		}

		private void UpdateCapture(string message, Bitmap capture)
		{
			CaptureMessageLabel.Text = message;
			if (capture.Width > CapturePictureBox.Width || capture.Height > CapturePictureBox.Height)
			{
				CapturePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			}
			else
			{
				CapturePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			}
			CapturePictureBox.Image = capture;
		}

		private void UpdateLog()
		{ 
			LogTextBox.Text = Observer.Log;
			LogTextBox.SelectionStart = LogTextBox.TextLength;
			LogTextBox.ScrollToCaret();
		}

		
	}
}
