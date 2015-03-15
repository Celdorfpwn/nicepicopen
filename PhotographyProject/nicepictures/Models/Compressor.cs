using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace nicepictures.Models
{
    public class Compressor
    {
        public static byte[] Compress(byte[] imageData, int quality)
        {

            var jpegQuality = quality;
            Image image;
            Byte[] outputBytes;
            MemoryStream stream = new MemoryStream(imageData);
            try
            {
                image = Image.FromStream(stream);
            }
            catch (Exception e)
            {
                e = null;
                return imageData;
            }

            using (var inputStream = stream)
            {
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality);

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