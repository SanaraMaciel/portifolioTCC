using Newtonsoft.Json;
using ProjetoIntranet.Helpers;
using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoIntranet.Controllers
{
    [AutorizacaoCustomizada]
    public class GrupoUsuarioController : BaseController
    {

       private static List<SelectListItem> privilegios = new List<SelectListItem>();

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
                case "CriarGrupo":
                    return RedirectToAction("CriarGrupo");

                case "ListarGrupos":
                    return RedirectToAction("ListarGrupos");
                                    
                default: return RedirectToAction("Principal", "Home");

            }

        }

        [HttpPost]
        public ActionResult CriarGrupo(String json )
        {
                       

           var grupoUsuario = JsonConvert.DeserializeObject<GrupoUsuario>(json);

            ViewBag.privilegios = privilegios;


            if (grupoUsuario.nome != "" && grupoUsuario.privilegios != "")
            {


                GrupoUsuarioBO gub = new GrupoUsuarioBO();

                gub.Gravar(grupoUsuario);

            }
            

                
                        
           

            return View();

        }


        [HttpGet]
        [AutorizacaoCustomizada(Roles ="ADMINISTRADOR")]
        public ActionResult CriarGrupo()
        {
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            if (privilegios.Count == 0)
            {
                privilegios.Add(new SelectListItem { Text = "Administrador", Value = Privilegios.ADMINISTRADOR.ToString() });
                privilegios.Add(new SelectListItem { Text = "Tecnologia", Value = Privilegios.TECNOLOGIA.ToString() });
                privilegios.Add(new SelectListItem { Text = "Recursos Humanos", Value = Privilegios.RECURSOSHUMANOS.ToString() });
                privilegios.Add(new SelectListItem { Text = "Produção", Value = Privilegios.PRODUCAO.ToString() });
                privilegios.Add(new SelectListItem { Text = "Marketing", Value = Privilegios.MARKETING.ToString() });

            }
            

            ViewBag.privilegios = privilegios;

           
                       
                        
           

            return View();
        }

        

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        public ActionResult ListarGrupos()
        {
            GrupoUsuarioBO gbo = new GrupoUsuarioBO();


            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;



            return View(gbo.ListarGrupos());

            

            
        }

        
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        public ActionResult Excluir(int id) // 
        {
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            GrupoUsuario gu = new GrupoUsuario();
           

                gu.id = id;

                GrupoUsuarioBO gub = new GrupoUsuarioBO();


            Boolean NaoDeletado = gub.Delete(gu);
            
            if(NaoDeletado == true)
            {

                return Json(new { success = false, responseText = "Existe(m) usuario(s) associado(s) ao grupo!!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            

           
        }
        [HttpPost]
        public ActionResult TesteId(GrupoUsuario gu)
        {

           

            return View();
        }


        [HttpGet]
        public ActionResult TesteId()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Linkagem()
        {



            return View();
        }

        [HttpPost]
        public ActionResult Linkagem(int temp)// parametro temporario
        {



            return View();
        }



    }
}