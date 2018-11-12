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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenNumericObserver.Core
{
	public class ScreenSaverPreventer : IDisposable
	{
		public bool IsRunning { get; private set; }
		private Timer Timer { get; set; }

		public ScreenSaverPreventer()
		{
			Timer = new Timer() { Interval = 30000 };
			Timer.Tick += Timer_Tick;
		}

		public void Dispose()
		{
			Stop();
			Timer.Dispose();
		}

		public void Start()
		{
			if(IsRunning)
			{
				return;
			}
			IsRunning = true;
			Timer.Start();
			
		}

		public void Stop()
		{
			if(!IsRunning)
			{
				return;
			}
			IsRunning = false;
			Timer.Stop();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			INPUT input = new INPUT();
			input.type = INPUT_MOUSE;
			input.mi = new MOUSEINPUT();

			input.mi.dwExtraInfo = IntPtr.Zero;
			input.mi.dx = 0;
			input.mi.dy = 0;
			input.mi.time = 0;
			input.mi.mouseData = 0;
			input.mi.dwFlags = 0x0001;
			int cbSize = Marshal.SizeOf(typeof(INPUT));
			SendInput(1, ref input, cbSize);
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct MOUSEINPUT
		{
			public int dx;
			public int dy;
			public uint mouseData;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}
		private const int INPUT_MOUSE = 0;

		[StructLayout(LayoutKind.Sequential)]
		private struct KEYBDINPUT
		{
			ushort wVk;
			ushort wScan;
			uint dwFlags;
			uint time;
			IntPtr dwExtraInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct HARDWAREINPUT
		{
			uint uMsg;
			ushort wParamL;
			ushort wParamH;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct INPUT
		{
			[FieldOffset(0)]
			public int type;
			[FieldOffset(4)]
			public MOUSEINPUT mi;
			[FieldOffset(4)]
			public KEYBDINPUT ki;
			[FieldOffset(4)]
			public HARDWAREINPUT hi;
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
	}
}
