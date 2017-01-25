using MVCprojekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace MVCprojekt.App_Start
{
    public class IndentityDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var info = new InformacjeKlient() { Imie = "Maciej", Nazwisko = "Nowak", Wiek = "24", Pesel = "92122707270", Numer_Telefonu = "609-609-609" };
            string roleName = "Admin";
            string password = "P@ssw0rd";

            UserManager.Create(new ApplicationUser() { UserName = "dembochdc@wp.pl", Email = "dembochdc@wp.pl", informacjeKlient = info }, password);

            if (!RoleManager.RoleExists(roleName))
            {
                var roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            var info2 = new InformacjeKlient() { Imie = "Marian", Nazwisko = "Kiepski", Wiek = "69", Pesel = "69696969696", Numer_Telefonu = "609-609-609" };
            var user = new ApplicationUser();
            user.UserName = "Marian@wp.pl";
            user.Email = "Marian@wp.pl";
            user.informacjeKlient = info2;
            var createResult = UserManager.Create(user, password);

            if (createResult.Succeeded)
            {
                var result = UserManager.AddToRoles(user.Id, roleName);
            }

            base.Seed(context);
        }
    }
}