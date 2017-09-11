using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_EmojiAPI.Controllers
{

        public class HomeController : Controller
        {
            public ActionResult Index()
            {
                ViewBag.Title = "Home Page";

                return View();
            }
        
            public ActionResult Login()
            {
                return View();
            }
        public ActionResult Result(string Username)
        {
            if (Username != "User 1" && Username != "User 2")
            {
                return View("Error");
            }
            else
            {

                string json = "";
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = "https://api.github.com/emojis";

                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 1.1.4322; .NET CLR 3.5.20404)");


                    json = wc.DownloadString(url);

                    ////var json = File.ReadAllText("input.txt");
                    //var a = new { serverTime = "", data = new object[] { } };
                    //var c = new JsonSerializer();
                    //dynamic jsonObject = c.Deserialize(new StringReader(json), a.GetType());
                    //Console.WriteLine(jsonObject.data[0]);
                    var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);


                    //var firstItem = jsonResult["data"][1];

                    List<MVC_EmojiAPI.Models.Images> emojiurl = new List<MVC_EmojiAPI.Models.Images>();
                    foreach (var item in jsonResult)
                    {
                        MVC_EmojiAPI.Models.Images images = new Models.Images();
                        images.URL = item.Value;
                        emojiurl.Add(images);
                    }
                    return View(emojiurl);
                }
            }
        }



            [HttpPost]
            public ActionResult Login(MVC_EmojiAPI.Models.LoginParameters LPModel)
            {
                if (ModelState.IsValid)
                {
                    var Username = LPModel.Username;
                    var Password = LPModel.Password;
                    string UserDisplayName = "";
                    if (Username == "user1" && Password == "password1")
                    {
                        UserDisplayName = "User 1";
                    }
                    if (Username == "user2" && Password == "password1")
                    {
                        UserDisplayName = "User 2";
                    }
                    if (UserDisplayName != "")
                    {
                        ViewBag.UserDisplayName = UserDisplayName;
                        ViewBag.CredentialsAuthenticated = "Yes";
                    }
                    else
                    {
                        ViewBag.CredentialsAuthenticated = "No";
                    }
                    return View(LPModel);
                }
                return View(LPModel);
            }
        }
    }
