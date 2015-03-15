using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractWorkbenchCameraContext
    {
        void AddCamera(Stream file, PhotographerCamera camera, string userName);

        IEnumerable<PhotographerCamera> Cameras { get; }
    }
}
