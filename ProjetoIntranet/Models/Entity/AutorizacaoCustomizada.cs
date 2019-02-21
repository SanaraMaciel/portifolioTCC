using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoIntranet.Models.Entity
{
    public class AutorizacaoCustomizada : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                filterContext.Result = new HttpUnauthorizedResult();

            }
            else
            {

                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "AcessoNegado", action = "Negado" }));

            }

        }


    }
}