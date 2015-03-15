using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Entities;

namespace p.Database.Concrete.Entities
{
    public class ImageWatermark : Watermark
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        [Required]
        [Display(Name="Name")]
        public string WatermarkName { get; set; }
        public byte[] Image { get; set; }
        [Required]
        [Display(Name = "Width(%)")]
        public int Width { get; set; }
        [Required]
        [Display(Name = "Height(%)")]
        public int Height { get; set; }
        [Required]
        [Display(Name = "Horizonal Position")]
        public string HorizontalAlign { get; set; }
        [Required]
        [Display(Name = "Vertical Position")]
        public string VerticalAlign { get; set; }
        [Required]
        [Display(Name = "Opacity(%)")]
        public int Opacity { get; set; }
        [Required]
        [Display(Name = "Margin(px)")]
        public int Padding { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
