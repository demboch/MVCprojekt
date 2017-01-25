using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCprojekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCprojekt.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[AllowAnonymous]
        [Authorize]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var currentUser = manager.FindById(currentUserId);

            //var info = string.Format("Imie {0} Nazwisko {1}", currentUser.informacjeKlient.Imie, currentUser.informacjeKlient.Nazwisko);

            //ViewBag.ProfileInformation = info;
            var licznik = PobierzLicznik();


            var dostepne = (from b in db.Bron
                                    orderby b.NazwaBroni descending
                                    select b).Take(5);
            ViewBag.Bron = db.Bron.Count();
            return View(dostepne.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "O nas";
            IQueryable<BronViewModels> data =
                from bron in db.Bron
                group bron by bron.Sklep.NazwaSklepu into bronGroup
                select new BronViewModels()
                {
                    Nazwa = bronGroup.Key,
                    Ilosc = bronGroup.Count(),
                };
            ViewBag.Bron = db.Bron.Count();
            ViewBag.Users = db.Users.Count();
            return View(data.ToList());
           // return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Kontakt";

            return View();
        }

        public ActionResult Api()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LicznikSesji()
        {
            var licznik = PobierzLicznik();
            licznik.LicznikSesji++;
            UstawLicznik(licznik);

            return RedirectToAction("Index");
        }

        private void UstawLicznik(Licznik licznik)
        {
            Session["licznik"] = licznik.LicznikSesji;
        }

        private Licznik PobierzLicznik()
        {
            var licznik = new Licznik();
            if (Session["licznik"] != null)
                licznik.LicznikSesji = (int)Session["licznik"];
            else
                licznik.LicznikSesji = 0;

            return licznik;
        }
    }
}