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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenNumericObserver.GUI.Forms
{
	public partial class SelectScreenDialog : Form,IComparer<Screen>
	{
		private Screen[] Screens { get; set; }
		public Screen SelectedScreen { get; set; }

		public SelectScreenDialog()
		{
			InitializeComponent();
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			DisplayComboBox.SelectedIndexChanged -= DisplayComboBox_SelectedIndexChanged;
			Screens = Screen.AllScreens;
			Array.Sort(Screens, this);
			DisplayComboBox.Items.Clear();
			var deviceNames = Screens.Select(screen =>
								{
									return string.Format("{0} (x:{1}, y:{2}, w:{3}, h{4})",
														screen.DeviceName,
														screen.Bounds.X,
														screen.Bounds.Y,
														screen.Bounds.Width,
														screen.Bounds.Height);
								}).ToArray();
			DisplayComboBox.Items.AddRange(deviceNames);
			if (SelectedScreen == null)
			{
				DisplayComboBox.SelectedIndex = 0;
				SelectedScreen = Screens[0];
			}
			else
			{
				int index = Array.IndexOf(deviceNames, SelectedScreen.DeviceName);
				if (index == -1)
				{
					index = 0;
					SelectedScreen = Screens[index];
				}
				DisplayComboBox.SelectedIndex = index;
			}
			DisplayComboBox.SelectedIndexChanged += DisplayComboBox_SelectedIndexChanged;
		}

		private void DisplayComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedScreen = Screens[DisplayComboBox.SelectedIndex];
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		public int Compare(Screen x, Screen y)
		{
			return x.DeviceName.CompareTo(y.DeviceName);
		}
	}
}
