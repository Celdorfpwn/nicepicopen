using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using PicturesProvider.Concrete;
using Workbench.Abstract;

namespace Workbench.Concrete
{
    public class WorkbenchProfileContext : AbstractWorkbenchProfileContext
    {
        private AbstractWorkbenchProfileRepository _repository { get; set; }

        public WorkbenchProfileContext(AbstractWorkbenchProfileRepository repository)
        {
            _repository = repository;
        }

        public Photographer GetPhotographer(string userName)
        {
            Photographer photographer = _repository.GetPhotographer(userName);
            return photographer;
        }


        public bool EditProfilePicture(System.IO.Stream stream,int lenght ,string userName)
        {
            byte[] imageData = ImageProcessor.GetImageDataFromStream(stream, lenght);

            byte[] small = Resizer.Resize(imageData, "100", "100");
            byte[] result = Compressor.Compress(small, 50);
            if (!ImageProcessor.CheckIfFileIsImage(imageData))
            {
                return false;
            }
            var user = _repository.GetPhotographer(userName);
            user.SmallProfile = result;
            var profilePicture = _repository.UserProfilePicture(user.Id);
            profilePicture.Image = imageData;
            _repository.Save();

            return true;
        }
    }
}
