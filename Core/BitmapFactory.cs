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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenNumericObserver.Core
{
	public static class BitmapFactory
	{
		private static ColorMatrix GrayScaleColorMatrix= new ColorMatrix(
														   new float[][]{
																new float[]{0.299f, 0.299f, 0.299f, 0 ,0},
																new float[]{0.587f, 0.587f, 0.587f, 0, 0},
																new float[]{0.114f, 0.114f, 0.114f, 0, 0},
																new float[]{0, 0, 0, 1, 0},
																new float[]{0, 0, 0, 0, 1}
														   });
		
		public static Bitmap CreateScreenCapture(int x,int y,int width,int height,bool grayScaling,float scale=1.0f,PixelFormat pixelFormat=PixelFormat.Format24bppRgb, InterpolationMode interpolationMode = InterpolationMode.Default)
		{
			var bitmap = new Bitmap(width, height, pixelFormat);
			using (var graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = interpolationMode;
				graphics.CopyFromScreen(x, y, 0, 0, new Size(width,height));
				if(grayScaling)
				{
					var imageAttr = new ImageAttributes();
					imageAttr.SetColorMatrix(GrayScaleColorMatrix);
					graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
				}
			}
			if(scale==1.0f)
			{
				return bitmap;
			}
			var resizedBitmap=CreateResizedBitmap(bitmap,scale);
			bitmap.Dispose();
			return resizedBitmap;
		}

		public static Bitmap CreateResizedBitmap(Bitmap srcBitmap, float scale, InterpolationMode interpolationMode = InterpolationMode.Default)
		{
			var dstBitmap = new Bitmap((int)(srcBitmap.Width * scale), (int)(srcBitmap.Height * scale));
			using (var graphics = Graphics.FromImage(dstBitmap))
			{
				graphics.InterpolationMode = interpolationMode;
				graphics.DrawImage(srcBitmap, 0,0,dstBitmap.Width, dstBitmap.Height);
			}
			return dstBitmap;
		}
	}
}
