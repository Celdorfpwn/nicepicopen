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
    public class TextWatermark : Watermark
    {
        public int Id { get; set; }
        public int PhotographerId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string WatermarkName { get; set; }
        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Color")]
        public string FontColor { get; set; }
        [Required]
        [Display(Name = "Font Size(px)")]
        public int FontSize { get; set; }
        [Required]
        [Display(Name = "Font Weight")]
        public string FontStyle { get; set; }
        [Required]
        [Display(Name = "Font Family")]
        public string FontFamily { get; set; }
        [Required]
        [Display(Name = "Horizontal Position")]
        public string HorizonalAlign { get; set; }
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
