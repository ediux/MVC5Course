﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Clients
        public ActionResult Index(string search,int? CreditRanking,string Gender)
        {
            var clients = db.Clients.Include(c => c.Occupation).OrderByDescending(o => o.ClientId).Take(10);
            if (!string.IsNullOrEmpty(search))
            {
                clients = clients.Where(w => w.FirstName.Contains(search));
            }

            if (CreditRanking!=null && CreditRanking.HasValue)
            {
                clients = clients.Where(w => w.CreditRating == CreditRanking.Value);
            }

            if (!string.IsNullOrEmpty(Gender))
            {
                clients = clients.Where(w => w.Gender.Equals(Gender, StringComparison.InvariantCultureIgnoreCase));
            }

            ViewBag.CreditRanking = new SelectList(db.Clients.Select(s => s.CreditRating).Distinct().OrderBy(o => o));
            ViewBag.Gender = new SelectList(new string[]{"M","F"});
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "OccupationName");
            var client = new Client()
            {
                Gender = "M"
            };
            return View(client);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ClientLoginViewModel client)
        {
            return View("LoginResult", client);
        }
        // POST: Clients/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {

            if (ModelState.IsValid)
            {
                var client = db.Clients.Find(id);
                if (client != null)
                {
                    if (TryUpdateModel(client))
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                //db.Entry(client).State = EntityState.Modified;

                ViewBag.OccupationId = new SelectList(db.Occupations, "OccupationId", "OccupationName", client.OccupationId);
                return View(client);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
