using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using p.WebUI.Models;
using WebMatrix.WebData;

namespace p.WebUI
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            //WebSecurity.InitializeDatabaseConnection("DatabaseContext", "Photographers", "Id", "Name", autoCreateTables: false);
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "NtQO5V3Fk8HF45rlgMDYEA",
                consumerSecret: "oVb4ipCdBj0DbAp9wvlpt2rZrgivRYTvmSfzgjQ");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "237791036405660",
                appSecret: "e9bc532970dc02883980679d8ee2c668");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
