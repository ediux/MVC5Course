using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();
            var data = db.Products;
            return View(data);
        }

        public ActionResult Create()
        {
            var db = new FabricsEntities();
            var data = new Product()
            {
                ProductName = "White Cat",
                Active = true,
                Price = 199,
                Stock = 5
            };
            db.Products.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var db = new FabricsEntities();
            var data = db.Products.Find(id);
            db.OrderLines.RemoveRange(data.OrderLines);
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var db = new FabricsEntities();
            var data = db.Products.Find(id);
            return View(data);
        }

        public ActionResult Update(int id)
        {
            var db = new FabricsEntities();
            var data = db.Products.Find(id);
            data.ProductName += "!";
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is System.Data.Entity.Validation.DbEntityValidationException)
                {
                    var efException = (System.Data.Entity.Validation.DbEntityValidationException)ex;

                    List<String> _vError = new List<string>();

                    foreach (var EntityErrors in efException.EntityValidationErrors)
                    {
                        foreach (var vErrors in EntityErrors.ValidationErrors)
                        {
                            _vError.Add(string.Format("{1}:{0}", vErrors.ErrorMessage, vErrors.PropertyName));
                        }
                    }
                    throw new Exception(string.Concat(_vError.ToArray()));
                }
                else
                {
                    throw ex;
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Add20Percent()
        {
            var db = new FabricsEntities();
            foreach (var item in db.Products)
            {
                if (item.Price.HasValue)
                {
                    item.Price = item.Price.Value * 1.2m;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ClientContribution()
        {
            var db = new FabricsEntities();
            var data = db.vw_ClientContribution;
            return View(data);
        }
    }
}