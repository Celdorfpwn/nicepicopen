using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Abstract.Entities
{
    public interface Watermark
    {
        int Id { get; set; }

        string WatermarkName { get; set; }
    }
}
