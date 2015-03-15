using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Abstract
{
    public interface AbstractAccountContext
    {
        void CreateUser(string platform,string accessToken,string name,byte[] defaultProfile);
        void CreateUser(string username,string fullName, byte[] defaultProfile, string email, string password);
        void SetUserCredentials(string platform, string accessToken, string accessName,string userName);
        bool UserExists(string userName);
        string GetUserName(string platform, string accessToken, string accessName);

        bool UserExists(string usernameEmail, string password);

        int GetUserId(string username);
    }
}
