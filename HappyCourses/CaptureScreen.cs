using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyCourses
{
    public static class CaptureScreen
    {
        public static bool CaptureFullScreen()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                bitmap.Save(Parameter._screenshotImagePath, ImageFormat.Png);
            }

            if (File.Exists(Parameter._screenshotImagePath))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
