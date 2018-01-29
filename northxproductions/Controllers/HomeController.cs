using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using northxproductions.Models;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Newtonsoft.Json;

namespace northxproductions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TestAdd();
            return View(GetVideo());
        }

        /*public ActionResult Music()
        {
            return View();
        }*/

        public ActionResult NewsFeed()
        {
            return View();
        }

        [ActionName("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public NxVideo GetVideo()
        {
            var videos = NxVideoPostManager.Read();

            /*Creates json db*/
            if (videos.Count == 0)
            {
                ViewBag.Empty = true;

                return new NxVideo();
            }
            else
            {
                videos = (from video in videos
                          orderby video.CreateTime descending
                          select video).ToList();
                ViewBag.Empty = false;
                
                /**/
                return videos[0];
            }
        }
        
        [HttpPost]
        [Route("Home")]
        public ActionResult GetNext(NxVideo nxVideoModel)
        {
            /*Gets list of videos from NxVideoManager*/
            var videos = NxVideoPostManager.Read();

            /*Creates json db*/
            if (videos.Count == 0)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                videos = (from video in videos
                          orderby video.CreateTime descending
                          select video).ToList();
                ViewBag.Empty = false;

                /**/
                int idx = (videos.Count - 1) - nxVideoModel.Id;
                if (idx < (videos.Count - 1))
                {
                    idx ++;
                }
                else if (idx == (videos.Count - 1))
                {
                    idx = 0;
                }
                nxVideoModel = videos[idx];
                return View("Index", nxVideoModel); 
            }
        }

        [HttpPost]
        [Route("Home")]
        public ActionResult GetPrev(NxVideo nxVideoModel)
        {
            /*Gets list of videos from NxVideoManager*/
            var videos = NxVideoPostManager.Read();

            /*Creates json db*/
            if (videos.Count == 0)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                /*Orders list of videos*/
                videos = (from video in videos
                          orderby video.CreateTime descending
                          select video).ToList();
                ViewBag.Empty = false;

                /**/
                int idx = (videos.Count - 1) - nxVideoModel.Id;
                if (idx > 0)
                {
                    idx --;
                }
                else if(idx == 0)
                {
                    idx = videos.Count - 1;
                }
                nxVideoModel = videos[idx];
                return View("Index", nxVideoModel);
            }
        }

        public void TestAdd()
        {
            NxVideo one = new NxVideo();
            one.Url = "https://www.youtube.com/watch?v=D3dn-i4Hzdg";
            one.Name = "Parachute";
            one.Embed = "https://www.youtube.com/embed/D3dn-i4Hzdg";
            one.ProjectName = "Parachute";
            one.Id = 0;
            one.CreateTime = DateTime.Now;

            NxVideoPostManager.Create(JsonConvert.SerializeObject(one));           
        }
    }
}