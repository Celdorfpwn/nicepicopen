using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using p.Database.Abstract.Entities;

namespace p.Database.Concrete.Entities
{
    public class Picture
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public int PhotographerId { get; set; }

        public string PhotographerName { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public bool Downloadable { get; set; }

        public string ImageToBase64 { get; set; }

        public int? TextWatermarkId { get; set; }
        [ForeignKey("TextWatermarkId")]
        public virtual TextWatermark TextWatermark { get; set; }

        public int? ImageWatermarkId { get; set; }
        [ForeignKey("ImageWatermarkId")]
        public virtual ImageWatermark ImageWatermark { get; set; }

        public int? CameraId { get; set; }
        [ForeignKey("CameraId")]
        public virtual PhotographerCamera Camera { get; set; }

        public bool DownloadWithWatermark { get; set; }

        public virtual PictureCategory Category { get; set; }

        public int? PortofolioId { get; set; }

        public byte[] Image { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<Photographer> PhotographerNice { get; set; }

        public virtual ICollection<Photographer> PhotographerViews { get; set; }

        public virtual ICollection<Photographer> PhotographerNotNice { get; set; }

        public virtual ICollection<PictureComment> Comments { get; set; }

        public int Views { get; set; }

        public bool HasNice(string userName)
        {
            var names = PhotographerNice.Select(p => p.Name);
            return names.Contains(userName);
        }

        public bool HasNotSo(string userName)
        {
            var names = PhotographerNotNice.Select(p => p.Name);
            return names.Contains(userName);
        }

        public byte[] ImageWithWatermark
        {
            get
            {
                WebImage image = new WebImage(Image);

                return AddWatermarkOrReturnImage(image);
            }
        }

        private byte[] AddWatermarkOrReturnImage(WebImage image)
        {

            if (TextWatermarkId != null)
            {
                int fontSize = image.Height * TextWatermark.FontSize / 100;

                image.AddTextWatermark(TextWatermark.Text, TextWatermark.FontColor
                    , fontSize, TextWatermark.FontStyle
                    , TextWatermark.FontFamily, TextWatermark.HorizonalAlign,
                    TextWatermark.VerticalAlign, TextWatermark.Opacity, TextWatermark.Padding);
                return image.GetBytes();
            }
            else if (ImageWatermarkId != null)
            {
                int width = image.Width * ImageWatermark.Width / 100;
                int height = image.Height * ImageWatermark.Height / 100;
                WebImage markImage = new WebImage(ImageWatermark.Image);
                image.AddImageWatermark(markImage, width, height, ImageWatermark.HorizontalAlign
                    , ImageWatermark.VerticalAlign, ImageWatermark.Opacity, ImageWatermark.Padding);
                return image.GetBytes();
            }

            return Image;
        }

        public void SetWatermark(Watermark mark)
        {
            TextWatermarkId = null;
            ImageWatermarkId = null;
            if (mark is TextWatermark)
                TextWatermarkId = mark.Id;
            if (mark is ImageWatermark)
                ImageWatermarkId = mark.Id;
        }

        public string GetShorterDescription()
        {
            if (Description != null)
            {
                if (Description.Length > 20)
                {

                }
            }

            return String.Empty;
        }

        public float Rating { get; set; }
    }
}
