using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageResizer;

namespace PicturesProvider.Concrete
{
    public class Resizer
    {
        public static byte[] Resize(byte[] image, string maxWidth, string maxHeight)
        {
            
            ResizeSettings setting = GetSettings(maxWidth,maxHeight);

            MemoryStream source = CreateStream(image);
            MemoryStream destination = CreateStream();
            try
            {
                ImageBuilder.Current.Build(source, destination, setting, true);
            }
            catch (Exception e)
            {
                e = null;
                return image;
            }

            return destination.ToArray();
        }

        private static MemoryStream CreateStream(byte[] source)
        {
            return new MemoryStream(source);
        }

        private static MemoryStream CreateStream()
        {
            return new MemoryStream();
        }

        private static ResizeSettings GetSettings(string maxWidth, string maxHeight)
        {
            return new ResizeSettings(BuildSetting(maxWidth, maxHeight));
        }

        private static string BuildSetting(string maxWidht, string maxHeight)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("maxwidth=");
            builder.Append(maxWidht);
            builder.Append('&');
            builder.Append("maxheight=");
            builder.Append(maxHeight);
            return builder.ToString();
        }



    }
}
