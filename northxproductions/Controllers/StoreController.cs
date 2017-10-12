using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace northxproductions.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult StoreIndex()
        {
            return View();
        }
    }
}