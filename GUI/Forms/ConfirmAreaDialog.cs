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
	public partial class ConfirmAreaDialog : Form
	{
		private Rectangle Area { get; set; }
		private Screen Screen { get; set; }

		private ConfirmAreaDialog()
		{
			InitializeComponent();
		}

		public ConfirmAreaDialog(Screen screen,Rectangle area)
			: this()
		{
			Screen = screen;
			StartPosition = FormStartPosition.Manual;
			Location = Screen.Bounds.Location;
			Area = area;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Close();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (var brush = new SolidBrush(Color.FromArgb(255, 0, 64, 255)))
			{
				
				e.Graphics.FillRectangle(brush, Area.X - Screen.Bounds.X,Area.Y - Screen.Bounds.Y,Area.Width,Area.Height);
			}
		}
	}
}
