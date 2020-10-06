using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TicketsReselling.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult SetLanguage(string locale, string returnUrl, string queryString)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(locale)));

            if (!String.IsNullOrEmpty(returnUrl))
            {
                if (!String.IsNullOrEmpty(queryString))
                {
                    returnUrl += queryString;
                }

                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
