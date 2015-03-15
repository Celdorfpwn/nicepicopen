using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p.WebUI.Models
{
    public class ImageSizeModel
    {
        public ImageSizeModel() { }
        public ImageSizeModel(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}