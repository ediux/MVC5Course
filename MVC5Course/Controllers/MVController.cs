using MVC5Course.Filiters;
using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   
    [HandleError(ExceptionType = typeof(DbEntityValidationException),
        View = "Error_DbEntityValidationException")]
    public class MVController : BaseController
    {
        // GET: MV
        [Filiters.Shared頁面上常用的ViewBag變數資料]
        public ActionResult Index()
        {
            ViewData["Temp1"] = "站存資料 Temp1";
            return View();
        }
        [Filiters.Shared頁面上常用的ViewBag變數資料]
        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(Models.ViewModels.ClientLoginViewModel c)
        {
            if (c != null)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }

        
        public ActionResult ProductionList()
        {
            var result = db.All().OrderByDescending(o => o.ProductId);

            return View(result);
        }

        [HttpPost]
        public ActionResult BatchUpdate(List<ProductBatchUpdateViewModel> items)
        {
            //if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var product = db.Find(item.ProductId);
                    if (product != null)
                    {
                        product.ProductName = item.ProductName;
                        product.Active = item.Active;
                        product.Stock = item.Stock;
                        product.Price = item.Price;
                    }


                }

                db.UnitOfWork.Commit();

            }

            return RedirectToAction("ProductionList");
        }

        public ActionResult MyError()
        {
            throw new InvalidOperationException("ERROR");
            return View();
        }
    }
}