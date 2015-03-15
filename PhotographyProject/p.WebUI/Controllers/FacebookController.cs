using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Microsoft.Web.WebPages.OAuth;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class FacebookController : Controller
    {

        private const string app = "http://nicepic.azurewebsites.net/";

        public ActionResult Index()
        {
            return null;
        }


        public ActionResult SharePicture(int id,int albumId)
        {

            string link = app + "Explorer/Album/" + albumId + "?picture=" + id;
            string pictureLink = app + "Images/DetailsPicture/"+id;
            string message = "Check this out";

            CheckAuthorization(link, pictureLink,message);
            return RedirectToAction("Album","Explorer",new { id = albumId,picture = id});
        }

        private void CheckAuthorization(string link,string imageLink,string message)
        {

            string app_id = "237791036405660";

            string app_secret = "e9bc532970dc02883980679d8ee2c668";

            string scope = "publish_stream,manage_pages";



            if (Request["code"] == null)
            {

                Response.Redirect(string.Format(

                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, Request.Url.AbsoluteUri, scope));
            }
            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();



                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",

                    app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);



                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;



                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string vals = reader.ReadToEnd();
                    foreach (string token in vals.Split('&'))
                    {
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }

                }

                string access_token = tokens["access_token"];
                dynamic messagePost = new ExpandoObject();
                messagePost.picture = imageLink;
                messagePost.link = link;
                messagePost.message = message;


                var client = new FacebookClient(access_token);

               try
                {
                    client.Post("/me/feed", messagePost);
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }

        }   

    }
}
