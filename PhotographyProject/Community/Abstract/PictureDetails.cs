using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Community.Abstract
{
    public interface PictureDetails
    {
        Photographer Photographer { get; }
        Album Album { get; }
    }
}
