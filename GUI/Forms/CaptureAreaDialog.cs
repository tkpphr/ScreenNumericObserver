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
	public partial class CaptureAreaDialog : Form
	{
		private Point StartCapturePosition { get; set; }
		private Brush Brush { get; set; }
		private bool CaptureStarted { get; set; }
		private Screen Screen { get; set; }
		public Rectangle CaptureArea { get; private set; }

		public CaptureAreaDialog(Screen screen)
		{
			InitializeComponent();
			Screen = screen;
			StartPosition = FormStartPosition.Manual;
			Location = Screen.Bounds.Location;
			Brush = new SolidBrush(Color.FromArgb(255, 0, 64, 255));
			CaptureStarted = false;
			DialogResult = DialogResult.Cancel;
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			Brush.Dispose();
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
			if (!CaptureStarted && e.Button == MouseButtons.Left)
			{
				StartCapturePosition = new Point(e.X + Screen.Bounds.X,e.Y + Screen.Bounds.Y);
				CaptureStarted = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (CaptureStarted && e.Button == MouseButtons.Left)
			{
				int x = e.X + Screen.Bounds.X;
				int y = e.Y + Screen.Bounds.Y;
				var location = new Point(Math.Min(StartCapturePosition.X, x), Math.Min(StartCapturePosition.Y, y));
				var size = new Size(Math.Abs(x - StartCapturePosition.X), Math.Abs(y - StartCapturePosition.Y));
				CaptureArea = new Rectangle(location, size);
				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left)
			{
				Close();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (!CaptureStarted)
			{
				return;
			}
			e.Graphics.FillRectangle(Brush,CaptureArea.X - Screen.Bounds.X,CaptureArea.Y - Screen.Bounds.Y,CaptureArea.Width,CaptureArea.Height);
		}
	}
}
