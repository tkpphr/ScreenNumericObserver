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
using ScreenNumericObserver.Core;
using ScreenNumericObserver.GUI.UserControls;
using ScreenNumericObserver.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenNumericObserver.GUI.CustomControls
{
	public class ObserverTabPage : TabPage
	{
		private int _Index;
		public int Index
		{
			get => _Index;
			set
			{
				Text = Text.Remove(0, 1);
				_Index = value;
				Text = _Index.ToString() + Text;
			}
		}
		private Observer Observer { get; set; }

		public ObserverTabPage(Control parent,int index,Observer observer)
		{
			Parent = parent;
			_Index = index;
			Observer = observer;
			Text = string.Format("{0}. {1}", Index, Observer.Title);
			var observerView = new ObserverView(observer);
			observerView.Dock = DockStyle.Fill;
			observerView.Parent = this;
			observer.Started += Observer_Started;
			observer.Stopped += Observer_Stopped;
			observer.Recognized += Observer_Recognized;
			observer.Unrecognized += Observer_Unrecognized;
			observerView.TitleChanged += ObserverView_TitleChanged;
			Controls.Add(observerView);
		}

		private void ObserverView_TitleChanged(string title)
		{
			Text = string.Format("{0}. {1}",Index, Observer.Title);
		}

		private void Observer_Unrecognized(Observer observer,Bitmap capture)
		{
			Text = string.Format("{0}. {1} [{2} : {3}]", Index, Observer.Title, Resources.Observing, Resources.Unrecognized);
		}

		private void Observer_Recognized(Observer observer, decimal value, Bitmap capture)
		{
			Text = string.Format("{0}. {1} [{2} : {3}]", Index, Observer.Title, Resources.Observing, value);
		}

		private void Observer_Stopped(Observer observer)
		{
			Text = string.Format("{0}. {1}", Index, Observer.Title);
		}

		private void Observer_Started(Observer observer)
		{
			Text = string.Format("{0}. {1} [{2}]", Index, Observer.Title,Resources.Observing);
		}
	}
}
