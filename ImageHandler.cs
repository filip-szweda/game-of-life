using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace game_of_life
{
    internal class ImageHandler
    {
        public static void SaveGridAsImage(Grid grid, string fileName)
        {
            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap((int)grid.ActualWidth, (int)grid.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            renderTargetBitmap.Render(grid);

            using (FileStream outputStream = new FileStream(fileName, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                encoder.Save(outputStream);
            }
        }
    }
}
