using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace Workbench.Concrete
{
    public class WorkbenchCameraContext : AbstractWorkbenchCameraContext
    {
        private AbstractWorkbenchCamerasRepository _repository { get; set; }

        public WorkbenchCameraContext(AbstractWorkbenchCamerasRepository repository)
        {
            _repository = repository;
        }

        public PhotographerCamera GetCamera(int cameraId)
        {
            return _repository.GetCamera(cameraId);
        }

        public void AddCamera(Stream file,PhotographerCamera camera, string userName)
        {
            var imageData = ImageProcessor.GetImageDataFromStream(file,Convert.ToInt32(file.Length));

            if (ImageProcessor.CheckIfFileIsImage(imageData))
            {
                camera.Camera = imageData;

                _repository.AddCamera(camera);
                _repository.Save();
            }
        }


        public IEnumerable<PhotographerCamera> Cameras
        {
            get { return _repository.Cameras; }
        }
    }
}
