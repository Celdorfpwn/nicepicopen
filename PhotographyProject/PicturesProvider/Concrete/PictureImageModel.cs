using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesProvider.Concrete
{
    public class PictureImageModel
    {
        public PictureImageModel() { }

        public PictureImageModel(int pictureId,int width,int height)
        {
            PictureId = pictureId;
            Width = width;
            Height = height;
            WidthList = CreateList(width);
            HeightList = CreateList(height);
        }

        public int PictureId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<int> WidthList { get; set; }

        public List<int> HeightList { get; set; }

        private List<int> CreateList(int max)
        {
            List<int> list = new List<int>();
            for (int index = 200; index <= max; index++)
            {
                list.Add(index);
            }
            return list;
        }
    }
}
