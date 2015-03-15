using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace mvcapp.Infrastructure
{
    public class ImageCompressor
    {
        public static byte[] Compress(byte[] imageData,int quality)
        {
            var jpegQuality = quality;
            Image image;
            Byte[] outputBytes;
            using (var inputStream = new MemoryStream(imageData))
            {
                image = Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);

                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    outputBytes = outputStream.ToArray();
                }
            }
            return outputBytes;
        }
    }
}