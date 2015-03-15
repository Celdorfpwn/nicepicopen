using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.Concrete
{
    public class ImageProcessor
    {
        public static byte[] GetImageDataFromStream(Stream image,int lenght)
        {
            byte[] imageData;
            using (var reader = new BinaryReader(image))
            {
                imageData = reader.ReadBytes(lenght);
            }
            return imageData;
        }

        public static bool CheckIfFileIsImage(byte[] content)
        {
           try
            {
                MemoryStream stream = new MemoryStream(content);
                var image = Image.FromStream(stream);
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
            return true;
        }

        public static string GetImageCameraModel(byte[] content)
        {
            MemoryStream stream = new MemoryStream(content);
            string cameraModel = null;
            PropertyItem propertyItem = null;
            var image = Image.FromStream(stream);
            try
            {
                propertyItem = image.GetPropertyItem(272);
            }
            catch (Exception e)
            {
                e.ToString();
            }
            if (propertyItem != null)
            {
                var decode = System.Text.ASCIIEncoding.Default.GetString(propertyItem.Value);
                var split = decode.Split('\0');
                cameraModel = split.ElementAt(0);
            }

            return cameraModel;
        }
    }
}
