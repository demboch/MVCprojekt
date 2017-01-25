using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCprojekt.Helpers
{
    public static class MyHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string id, string url, string tekstAlternatywny, string styles,object htmlAttributes)
        { 
            var builder = new TagBuilder("img");

            builder.GenerateId(id);

            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", tekstAlternatywny);
            builder.MergeAttribute("style", styles);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing)); 
        }
    }
}

