using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Image()
        {
            string filepath = Server.MapPath("~/Content/翁茲曼_1.jpg");

            return File(filepath,"image/jpeg");
        }

        public ActionResult Download()
        {

            string filepath = Server.MapPath("~/Content/翁茲曼_1.jpg");

            return File(filepath, "image/jpeg","girl.jpg");
        }
    }
}