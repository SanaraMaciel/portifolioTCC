using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ProjetoIntranet.Helpers;

namespace ProjetoIntranet.Controllers
{
    [AutorizacaoCustomizada]
    public class UsuarioController : BaseController
    {

        private static IList<SelectListItem> users = new List<SelectListItem>();

        private static  IList<SelectListItem> itens = new List<SelectListItem>();

        private static IList<Usuario> usuarios = null;

        private static int contagemAnterior = 0;

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
                case "Associar":
                    return RedirectToAction("Associar");

                case "DesativarUsuario":
                    return RedirectToAction("DesativarUsuario");

                case "CadastroUsuario":
                    return RedirectToAction("CadastroUsuario");

                default: return RedirectToAction("Principal", "Home");

            }

        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles ="ADMINISTRADOR")]
        public ActionResult CadastroUsuario(Usuario user)
        {

            if (ModelState.IsValid)
            {

                UsuarioBO ubo = new UsuarioBO();
                ubo.Gravar(user);

                


            }


            return RedirectToAction("CadastroUsuario");
        }


        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        public ActionResult CadastroUsuario()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            return View();
        }

        

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        public ActionResult Associar(FormCollection form)
        {

            bool isFormOk = ValidacaoForm(form);

            if (isFormOk)
            {
                var idUsuario = form["Lista"];
                var idGrupos = form["check"];

                String[] idGruposAssociados = idGrupos.Split(',');



                UsuarioEGruposBO ueg = new UsuarioEGruposBO();

                ueg.Gravar(idUsuario, idGruposAssociados);

               

            }
            
            ViewBag.users = users;
            ViewBag.lista = itens;


            return View();
        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        public ActionResult Associar()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            GrupoUsuarioBO gub = new GrupoUsuarioBO();

            UsuarioBO ubo = new UsuarioBO();


                users.Clear();
                itens.Clear();

            ViewBag.UsuarioLogado = usr.nome;

            //if ((users.Count <=0 && itens.Count <= 0)|| contagemAnterior != users.Count)
            //{


            //    users.Clear();
            //    itens.Clear();

              

               


            //    contagemAnterior = itens.Count;
            //}

            foreach (var obj1 in ubo.ListarUsuarios())
            {


                users.Add(new SelectListItem { Text = obj1.nome, Value = obj1.id.ToString() });

            }

            foreach (var obj in gub.ListarGrupos())
            {

                itens.Add(new SelectListItem { Text = obj.nome, Value = obj.id.ToString() });


            }

            ViewBag.users = users;
            ViewBag.lista = itens;

            

            return View();
        }
                

        private bool ValidacaoForm(FormCollection f)
        {

            bool retorno = true;
            var idUsuario = f["Lista"];
            var idGrupos = f["check"];
            ViewBag.users = users;
            ViewBag.lista = itens;

            if (idUsuario.Equals(""))
            {
                retorno = false;
                ViewData["msg1"] = "Escolha um funcionario!!";

            }

            if (idGrupos == null)
            {
                retorno = false;
                ViewData["msg2"] = "Escolha um Grupo!!";
            }


            return retorno;
        }

        
        

        [AutorizacaoCustomizada(Roles ="ADMINISTRADOR")]
        [HttpGet]
        public ActionResult DesativarUsuario(int ? pagina)
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;



            ViewBag.UsuarioLogado = usr.nome;

            int tamanhoPagina = 4;
            int numeroPagina = pagina ?? 1;

            UsuarioBO uBO = new UsuarioBO();

            usuarios = uBO.ListarUsuarios();

            TempData["listaDeUsuarios"] = usuarios;

            return View(usuarios.ToPagedList(numeroPagina,tamanhoPagina));
        }
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public ActionResult DesativarUsuario(int? pagina, FormCollection form,String id)
        {



            int userID = Convert.ToInt32(id);

            UsuarioBO uBO = new UsuarioBO();
            uBO.Desativar(userID);



            return View(TempData["listaDeUsuarios"] as IList<Usuario>);
        }

        [HttpPost]
        public ActionResult AtivarUsuario(int? pagina, FormCollection form, String id)
        {

            int userID = Convert.ToInt32(id);

            UsuarioBO uBO = new UsuarioBO();
            uBO.Ativar(userID);


            return View("DesativarUsuario", TempData["listaDeUsuarios"] as IList<Usuario>);
        }


    }
}