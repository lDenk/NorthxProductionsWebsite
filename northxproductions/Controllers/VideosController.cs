using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using northxproductions.Models;
using Newtonsoft.Json;

namespace northxproductions.Controllers
{
    public class VideosController : Controller
    {
        // GET: Videos
        public ActionResult Index()
        {
            NxVideoProjectsListModel projects = new NxVideoProjectsListModel();
            projects.Create(NxVideoPostManager.Read());
            return View(projects);
        }
    }
}