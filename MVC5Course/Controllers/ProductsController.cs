﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{

    [HandleError(ExceptionType = typeof(DbEntityValidationException),
        View = "Error_DbEntityValidationException")]
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();



        // GET: Products
        public async Task<ActionResult> Index()
        {
            var data = await db.All().ToListAsync();
            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Add(product);
                db.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                ((FabricsEntities)db.UnitOfWork).Entry(product).State = EntityState.Modified;
                db.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public async Task<ActionResult> JsonTest()
        {
            db.UnitOfWork.LazyLoadingEnabled = false;
            var result = await db.All().Take(25).ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Find(id);
            db.Delete(product);

            db.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
