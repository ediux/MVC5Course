using MVC5Course.Filiters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [LocalDebugOnly]
    [HandleError(ExceptionType = typeof(DbEntityValidationException),
        View = "Error_DbEntityValidationException")]
    public class MBController : Controller
    {
        // GET: MB
        [Shared頁面上常用的ViewBag變數資料]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}