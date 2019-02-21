using ProjetoIntranet.Helpers;
using System;
using System.Web;
using System.Web.Mvc;

namespace ProjetoIntranet.Controllers
{
    public class AcessoNegadoController : BaseController
    {
        // GET: AcessoNegado
        public ActionResult Negado()
        {


            return View();
        }

        public ActionResult Entrar()
        {


            return RedirectToAction("Index", "Home");
        }

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
                case "Negado":
                    return RedirectToAction("Negado");

                default: return RedirectToAction("Index", "Home");

            }

        }

    }
}