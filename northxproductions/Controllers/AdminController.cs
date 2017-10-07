using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using northxproductions.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace northxproductions.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login(AdminAccount login)
        {
            return View();
        }
        
        public ActionResult VerifyLogin(AdminAccount login)
        {
            string user = login.username;
            string pass = login.password;

            if (user.Equals("Admin") && pass.Equals("camelblue12"))
            {
                Session["UserIsAdmin"] = "Admin";
                return View("Login");
            }
            return View("Login");
        }

        public ActionResult AdminControls()
        {
            if (Session["UserIsAdmin"].Equals("Admin"))
            {
                return View(NxVideoPostManager.Read());
            }
            return View("Login");
        }

        public ActionResult AddVideo(NxVideo video)
        {
            const string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
            const string replace = "https://www.youtube.com/embed/$1";

            var rgx = new Regex(pattern);
            var result = rgx.Replace(video.Url, replace);

            video.Embed = result;
            video.CreateTime = DateTime.Now;

            NxVideoPostManager.Create(JsonConvert.SerializeObject(video));

            return View("AdminControls");
        }

        public ActionResult DeleteVideo(NxVideo video)
        {
            NxVideoPostManager.Delete(video.Id);
            return View("AdminControls");
        }
    }
}