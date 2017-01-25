using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCprojekt.Controllers
{
    [RoutePrefix("AtrybutPrefix")]
    public class StareController : Controller
    {
        [Route("atrybut/routing/{paramtr}/trasa")]
        public string Index(string parametr)
        {
            return "Definiowanie tras - Atrybuty";
        }

        [Route("{paramtr:int}")]
        public string Test(string parametr)
        {
            return parametr;
        }
    }
}