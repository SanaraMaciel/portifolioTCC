using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;
using System.Web.Mvc;
using ProjetoIntranet.Helpers;

namespace ProjetoIntranet.Controllers
{
    public class GrupoLinksController : BaseController
    {
        [AllowAnonymous]
        public ActionResult SetCulture(string culture, string View)
        {

            culture = CultureHelper.GetImplementedCulture(culture);

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            switch (View)
            {
                case "GrupoELinks":
                    return RedirectToAction("GrupoELinks");                  

                default: return RedirectToAction("Principal", "Home");

            }

        }

        // GET: GrupoLinks
        public ActionResult GrupoELinks()
        {
            return View();
        }
    }
}