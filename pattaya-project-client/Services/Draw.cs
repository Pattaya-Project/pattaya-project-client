
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace pattaya_project_client.Services
{
    public class Draw
    {
        public static string Cap()
        {

            Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics graphics = Graphics.FromImage(screenCapture))
            {
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, screenCapture.Size);
            }

            byte[] screenCaptureBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                screenCapture.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                screenCaptureBytes = memoryStream.ToArray();
            }
            string screenCaptureBase64 = Convert.ToBase64String(screenCaptureBytes);

            return screenCaptureBase64;
        }
    }
}
