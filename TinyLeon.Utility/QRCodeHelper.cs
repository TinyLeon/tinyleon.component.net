using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class QRCodeHelper
    {
        //public static Image CreateQRCode(string input)
        //{
        //    //return CreateQRCode(input, new Size { Height = 400, Width = 400 });
        //    Image middleImg = Image.FromStream(System.Net.WebRequest.Create("http://tcw-public.b0.upaiyun.com/headimg/tclogo.jpg").GetResponse().GetResponseStream());
        //    var ms = QRCodeGenerate.BuildQrCode(input, QrCodeLevel.H, 200, Color.Black, Color.White, middleImg);
        //    return Bitmap.FromStream(ms);
        //}

        //public static Bitmap CreateQRCode(string input, Size size)
        //{
        //    var qrCodeEncoder = new QRCodeEncoder();
        //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        //    qrCodeEncoder.QRCodeScale = 30;
        //    qrCodeEncoder.QRCodeVersion = 0;

        //    var bmp = new Bitmap(size.Width, size.Height);
        //    using (Graphics g = Graphics.FromImage(bmp))
        //    {
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //        System.Drawing.Color bordcolor = System.Drawing.Color.White;
        //        System.Drawing.Rectangle rec = new Rectangle(0, 0, size.Width, size.Height);

        //        using (System.Drawing.Bitmap image = new System.Drawing.Bitmap(qrCodeEncoder.Encode(input, System.Text.Encoding.UTF8), size))
        //        {
        //            g.DrawImage(image, rec, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        //        }
        //        g.Dispose();
        //    }
        //    return bmp;
        //}
    }

    public enum QrCodeLevel
    {
        H = 0,
        L = 1,
        M = 2,
        Q = 3,
    }
}
