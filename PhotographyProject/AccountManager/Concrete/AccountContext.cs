using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace AccountManager.Concrete
{
    public class AccountContext : AbstractAccountContext
    {

        private AbstractAccountRepository _repository { get; set; }

        public AccountContext(AbstractAccountRepository repository)
        {
            _repository = repository;
        }

        public void CreateUser(string platform, string accessToken, string name,byte[] defaultProfile)
        {
            Photographer user = CreateUserAccount(name,defaultProfile);
            SetCredentials(user, name,platform, accessToken);
            CreateAccount(user,defaultProfile);
        }

        private void CreateAccount(Photographer user,byte[] profilePic)
        {
            _repository.AddAccount(user);
            var profilePicture = new UserProfilePicture
            {
                User = user,
                Image = profilePic
            };
            _repository.AddProfilePicture(profilePicture);
            _repository.Save();
        }

        public void CreateUser(string username,string fullName, byte[] defaultProfile, string email, string password)
        {
            Photographer user = CreateUserAccount(username,fullName, email, password,defaultProfile);
            CreateAccount(user,defaultProfile);
        }

        private Photographer CreateUserAccount(string username,string fullName, string email, string password,byte[] defaultProfile)
        {
            return new Photographer
            {
                Name = username,
                FullName = fullName,
                Email = email,
                Password = password
            };
        }

        private void SetCredentials(Photographer user,string name,string platform, string accessToken)
        {
            if (platform.Equals("facebook"))
            {
                user.AddFacebookCredentials(new ExternalInformation { AccessName = name, AccessToken = accessToken });
            }
            else if (platform.Equals("twitter"))
            {
                user.AddTwitterCredentials(new ExternalInformation { AccessName = name, AccessToken = accessToken });
            }
        }

        private Photographer CreateUserAccount(string name, byte[] defaultProfile)
        {
            return new Photographer { Name = name.Split('@')[0] };
        }



        public void SetUserCredentials(string platform, string accessToken, string name,string userName)
        {
            var user = _repository.GetPhotographer(userName);
            if (!CheckCredentials(platform, accessToken, name))
            {
                SetCredentials(user, name,platform,accessToken);
                _repository.Save();
            }
            
        }

        private bool CheckCredentials(string platform, string accessToken, string name)
        {
            ExternalInformation credentials = null;
            switch (platform)
            {
                case "facebook":
                    credentials = _repository.Photographers.Select(p => p.FacebookCredentials)
                        .FirstOrDefault(c => c.HasUser(accessToken,name));
                    if(credentials!=null)
                        return true;
                    else
                        break;
                case "twitter":
                    credentials = _repository.Photographers.Select(p => p.TwitterCredentials)
                        .FirstOrDefault(c => c.HasUser(accessToken,name));
                    if(credentials!=null)
                        return true;
                    else
                        break;
                default:
                    break;
            }
            return false;
        }

        public bool UserExists(string userName)
        {
            var user = _repository.GetPhotographer(userName);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public string GetUserName(string platform, string accessToken, string accessName)
        {
            var users = _repository.Photographers;
            Photographer user = null;
            switch (platform)
            {
                case "facebook":
                    user = users.FirstOrDefault(u => u.FacebookCredentials.HasUser(accessToken, accessName));
                    break;
                case "twitter":
                    user = users.FirstOrDefault(u => u.TwitterCredentials.HasUser(accessToken, accessName));
                    break;
                default:
                    return String.Empty;
            }
            if (user != null)
            {
                return user.Name;
            }
            return String.Empty;

        }



        public bool UserExists(string usernameEmail, string password)
        {
            var users = _repository.Photographers.Where(user => user.Name!=null && user.Password!=null);

            var exists = users.Where(user => user.Name.Equals(usernameEmail) || user.Email.Equals(usernameEmail));

            if (exists == null)
                return false;

            var result = exists.Where(user => user.Password.Equals(password));

            if (result == null)
                return false;
            
            if (result.Count() > 0)
                return true;
            else return false;
            
        }


        public int GetUserId(string username)
        {
            return _repository.GetPhotographer(username).Id;
        }
    }
}
