using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCprojekt.Models
{
    public class Sklep
    {
        public Sklep()
        {
            Bronie = new List<Bron>();
        }
        public int SklepId { get; set; }

        [UppercaseValidator]
        [Required(ErrorMessage = "Podaj nazwe sklepu!")]
        [Display(Name = "Nazwa Sklepu")]
        public string NazwaSklepu { get; set; }

        [UppercaseValidator]
        [Required(ErrorMessage = "Podaj ulice!")]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; }

        [UppercaseValidator]
        [Required(ErrorMessage = "Podaj miasto!")]
        [Display(Name = "Miasto")]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Podaj kod pocztowy!")]
        [Display(Name = "Kod Pocztowy")]
        public string KodPocztowy { get; set; }
        public virtual ICollection<Bron> Bronie { get; set; }
    }
}