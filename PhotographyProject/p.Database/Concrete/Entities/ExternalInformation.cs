using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    [ComplexType]
    public class ExternalInformation
    {
        public string AccessToken { get; set; }

        public string AccessName { get; set; }

        private bool IsEmpty()
        {
            if (AccessToken == null && AccessName == null)
                return true;
            else
                return false;
        }

        public bool Empty()
        {
            return IsEmpty();
        }

        public bool HasUser(string accessToken, string accessName)
        {
            if (IsEmpty())
                return false;
            else
            {
                var token = accessToken.Substring(0, 7);
                if (AccessToken.Contains(token) && accessName.Equals(AccessName))
                    return true;
                else
                    return false;
            }
        }
    }
}
