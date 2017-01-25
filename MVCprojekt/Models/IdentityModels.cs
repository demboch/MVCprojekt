using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace MVCprojekt.Models
{
    // Dane profilu dla użytkownika można dodać, dodając więcej właściwości do własnej klasy ApplicationUser. Więcej informacji można znaleźć na stronie http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public virtual InformacjeKlient informacjeKlient { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class InformacjeKlient
    {
        public int InformacjeKlientId { get; set; }

        [ScaffoldColumn(false)]
        public int KlientId { get; set; }

        [UppercaseValidator]
        public string Imie { get; set; }

        [UppercaseValidator]
        public string Nazwisko { get; set; }
        public string Wiek { get; set; }
        //public string Email { get; set; }
        public string Pesel { get; set; }
        public string Numer_Telefonu { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MVCprojekt.Models.Bron> Bron { get; set; }
        public System.Data.Entity.DbSet<MVCprojekt.Models.Sklep> Sklep { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //configure one-to-many
        //    modelBuilder.Entity<Sklep>()
        //                .HasMany<Bron>(s => s.Bronie)
        //                .WithRequired(s => s.Sklep)
        //                .HasForeignKey(s => s.SklepBronId);
        //}
    }
}