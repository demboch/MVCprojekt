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
    public class Bron
     {
         public int BronId { get; set; }

         [UppercaseValidator]
         [Required(ErrorMessage = "Podaj nazwe broni!")]
         [Display(Name = "Nazwa Broni")]
         public string NazwaBroni { get; set; }

         [Required(ErrorMessage = "Podaj model!")]
         [Display(Name = "Model")]
         public string Model { get; set; }

         [Required(ErrorMessage = "Podaj numer seryjny!")]
         [Display(Name = "Numer Seryjny")]
         public string NumerSeryjny { get; set; }

         [UppercaseValidator]
         [Display(Name = "Opis")]
         public string Opis { get; set; }
         
         //[ForeignKey("SklepBronId")]
         public int SklepBronId { get; set; }

         public virtual Sklep Sklep { get; set; }
     }
}
