using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Explorer.Abstract
{
    public interface PicturesUpdate
    {
        Photographer Photographer { get; }
        IEnumerable<Picture> Pictures { get; }
        Album Album { get; }
    }
}
