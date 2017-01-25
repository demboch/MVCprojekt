using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCprojekt.Models;
using PagedList;

namespace MVCprojekt.Controllers
{
    public class SklepController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sklep
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "NazwaSklepu";
            ViewBag.StreetSortParm = String.IsNullOrEmpty(sortOrder) ? "street_desc" : "Ulica";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "Miasto";
            ViewBag.PostalSortParm = String.IsNullOrEmpty(sortOrder) ? "postal_desc" : "KodPocztowy";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var sklep = db.Sklep.Include(b => b.Bronie);
            //var bron = from s in db.Bron
            //             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sklep = sklep.Where(s => s.NazwaSklepu.Contains(searchString)
                                       || s.Ulica.Contains(searchString)
                                       || s.Miasto.Contains(searchString)
                                       || s.KodPocztowy.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sklep = sklep.OrderByDescending(s => s.NazwaSklepu);
                    break;
                case "street_desc":
                    sklep = sklep.OrderByDescending(s => s.Ulica);
                    break;
                case "city_desc":
                    sklep = sklep.OrderByDescending(s => s.Miasto);
                    break;
                case "postal_desc":
                    sklep = sklep.OrderByDescending(s => s.KodPocztowy);
                    break;
                case "NazwaSklepu":
                    sklep = sklep.OrderBy(s => s.NazwaSklepu);
                    break;
                case "Ulica":
                    sklep = sklep.OrderBy(s => s.Ulica);
                    break;
                case "Miasto":
                    sklep = sklep.OrderBy(s => s.Miasto);
                    break;
                case "KodPocztowy":
                    sklep = sklep.OrderBy(s => s.KodPocztowy);
                    break;
                default:
                    sklep = sklep.OrderBy(s => s.SklepId);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sklep.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sklep/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sklep sklep = db.Sklep.Find(id);
            if (sklep == null)
            {
                return HttpNotFound();
            }
            return View(sklep);
        }

        // GET: Sklep/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sklep/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SklepId,NazwaSklepu,Ulica,Miasto,KodPocztowy")] Sklep sklep)
        {
            if (ModelState.IsValid)
            {
                db.Sklep.Add(sklep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sklep);
        }

        // GET: Sklep/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sklep sklep = db.Sklep.Find(id);
            if (sklep == null)
            {
                return HttpNotFound();
            }
            return View(sklep);
        }

        // POST: Sklep/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SklepId,NazwaSklepu,Ulica,Miasto,KodPocztowy")] Sklep sklep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sklep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sklep);
        }

        // GET: Sklep/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sklep sklep = db.Sklep.Find(id);
            if (sklep == null)
            {
                return HttpNotFound();
            }
            return View(sklep);
        }

        // POST: Sklep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sklep sklep = db.Sklep.Find(id);
            db.Sklep.Remove(sklep);
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
