using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractWorkbenchProfileContext
    {
        Photographer GetPhotographer(string userName);
        bool EditProfilePicture(System.IO.Stream stream, int lenght,string userName);
    }
}
