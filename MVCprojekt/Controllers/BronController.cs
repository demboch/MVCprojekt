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
    [RoutePrefix("AtrybutPrefix")]
    public class BronController : Controller
    {
        //[Route("atrybut/routing/{paramtr}/trasa")]
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bron
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "NazwaBroni";
            ViewBag.ModelSortParm = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "Model";
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "NumerSeryjny";

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var bron = db.Bron.Include(b => b.Sklep);
            //var bron = from s in db.Bron
              //             select s;

            if (!String.IsNullOrEmpty(searchString)) {
                bron = bron.Where(b => b.NazwaBroni.Contains(searchString)
                                       || b.Model.Contains(searchString)
                                       || b.NumerSeryjny.Contains(searchString)
                                       || b.Sklep.NazwaSklepu.Contains(searchString));
            }

            switch (sortOrder) {
                case "name_desc":
                    bron = bron.OrderByDescending(b => b.NazwaBroni);
                    break;
                case "model_desc":
                    bron = bron.OrderByDescending(b => b.Model);
                    break;
                case "number_desc":
                    bron = bron.OrderByDescending(b => b.NumerSeryjny);
                    break;
                case "NazwaBroni":
                    bron = bron.OrderBy(b => b.NazwaBroni);
                    break;
                case "Model":
                    bron = bron.OrderBy(b => b.Model);
                    break;
                case "NumerSeryjny":
                    bron = bron.OrderBy(b => b.NumerSeryjny);
                    break;
                default:
                    bron = bron.OrderBy(b => b.BronId);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(bron.ToPagedList(pageNumber, pageSize));
        }

        // GET: Bron/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bron bron = db.Bron.Find(id);
            if (bron == null)
            {
                return HttpNotFound();
            }
            return View(bron);
        }

        // GET: Bron/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bron/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "BronId,NazwaBroni,Model,NumerSeryjny,Opis")] Bron bron)
        {
            if (ModelState.IsValid)
            {
                db.Bron.Add(bron);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bron);
        }

        // GET: Bron/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bron bron = db.Bron.Find(id);
            if (bron == null)
            {
                return HttpNotFound();
            }
            return View(bron);
        }

        // POST: Skleps/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BronId,NazwaBroni,Model,NumerSeryjny,Opis")] Bron bron)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bron).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bron);
        }

        // GET: Bron/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bron bron = db.Bron.Find(id);
            if (bron == null)
            {
                return HttpNotFound();
            }
            return View(bron);
        }

        // POST: Bron/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bron bron = db.Bron.Find(id);
            db.Bron.Remove(bron);
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
