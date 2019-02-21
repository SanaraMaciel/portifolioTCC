using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using System.IO;

using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using ProjetoIntranet.Helpers;

namespace ProjetoIntranet.Controllers
{
    [AutorizacaoCustomizada]
    public class HomeController : BaseController
    {

        public object StringWr { get; private set; }

        private HashSet<int> ordemUsuada = new HashSet<int>();

        private static StringWriter stringWriter = new StringWriter();
        private static HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

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
                case "Index":
                return RedirectToAction("Index");

                case "Principal":
                return RedirectToAction("Principal");

                case "Curriculo":
                return RedirectToAction("Curriculo");

                case "ListarPostagensPrincipal":
                return RedirectToAction("LIstarPostagensPrincipal");

                default: return RedirectToAction("Index");

            }
            
        }


        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult _Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection form,String retunrUrl)// action responsavel por autenticação
        {


            UsuarioBO uBO = new UsuarioBO();

            GrupoUsuario grupo = new GrupoUsuario();

            String[] txbNomeUsuario = form["nomeUsuario"].ToString().Split('@'); // nonme de logon (ex: Fenix/gsc) no Active DIrectory

            

            if ((form["nomeUsuario"] == String.Empty || form["Senha"] == String.Empty)) // verifico se os campos nãoe stão em brancos
            {

                ViewBag.LoginError = "Nome de usuário ou senha inválidos.";
                return RedirectToAction("Index");
            }

            else
            {
                GrupoUsuarioBO guBO = new GrupoUsuarioBO();
                UsuarioEGruposBO uXg = new UsuarioEGruposBO();


                var usuario = uBO.PesquisarUsuario(form["nomeUsuario"].ToString(), form["Senha"].ToString()); // procura o usuario no banco da aplicação pelo user e senha digitado
                
               

               if(usuario != null)
                {


                    AutenticadorLDAP autenticador = new AutenticadorLDAP("LDAP://fenix.net", txbNomeUsuario[0], usuario.senha); //procura usuario no AD  com as credenciais passadas

                    if (/*autenticador.autenticar()*/ true) 
                    {


                        foreach (var idGrupo in uXg.listaDeGrupoAQualPertence(usuario.id))
                        {

                            GrupoUsuario g = new GrupoUsuario();
                            g.id = idGrupo;

                            usuario.setGrupos(g);

                        }

                        usuario.privilegios = guBO.pegarPrivilegios(usuario); 

                        FormsAuthentication.SetAuthCookie(usuario.usuarioLogin, true);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario.usuarioLogin, DateTime.Now, DateTime.Now.AddMinutes(20), true, usuario.privilegios);

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);


                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);



                        Response.Cookies.Add(authCookie);


                        Session.Add("UsuarioAtual", usuario);


                        Session.Add("UsuarioLogado", false); // criar uma flag para usuario logado


                        Historico log = new Historico(); // crio uma entra no log de eventos da aplicação

                        log.dataHora = DateTime.Now;
                        log.usuario = usuario;
                        log.mensagem = "Logon em intranet.";

                        HistoricoBO logBO = new HistoricoBO();

                        logBO.Gravar(log, log.mensagem);


                        return RedirectToAction("Principal", "Home");


                    }
                    else
                    {


                        return RedirectToAction("Index");
                    }


                }
                else
                {


                    return RedirectToAction("Index");
                }

               
               

            }


        }
       
       
        public ActionResult LogOff() // finaliza a sessão
        {

            Usuario usuario = Session["UsuarioAtual"] as Usuario;

            FormsAuthentication.SignOut();
            Session.Remove("UsuarioLogado");
            Session.Remove("UsuarioAtual");
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            SessionStateSection s = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(s.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);


            MenuController.descarregar();

            Historico log = new Historico();

            log.dataHora = DateTime.Now;
            log.usuario = usuario;
            log.mensagem = "Logoff em intranet.";

            HistoricoBO logBO = new HistoricoBO();

            logBO.Gravar(log, log.mensagem);


            return RedirectToAction("Index", "Home");
        }

        [AutorizacaoCustomizada(Roles ="ADMINISTRADOR,RECURSOSHUMANOS,TECNOLOGIA,PRODUCAO,MARKETING")]
        public ActionResult Principal()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;
           

            ViewBag.UsuarioLogado = usr.nome;

            

            
            return View();
        }

       
      

        public JsonResult Graph2() { // responsavel por gerar o grafico na parte principal da intranet recebendo json

            List<GChart> lista = new List<GChart>();

            lista.Add(new GChart() { Id = 1, Nome = "Placa Mãe", Qtd = 12 });
            lista.Add(new GChart() { Id = 2, Nome = "Placa Wifi", Qtd = 5 });
            lista.Add(new GChart() { Id = 3, Nome = "Placa Rede", Qtd = 8 });

            
            
            return (Json(lista,JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        [ActionName("debugUploadTest")]
        public ActionResult Upload(HttpPostedFileBase arq) {

            


            if (arq!= null && arq.ContentLength > 0)
            {
                var extensoePermitidas = new String[] { ".pdf", ".docx",".doc" };

                var checarExtensao = Path.GetExtension(arq.FileName).ToLower();

                if (extensoePermitidas.Contains(checarExtensao))
                {

                    string caminho = Path.Combine(Server.MapPath("~/pdf"), Path.GetFileName(arq.FileName));
                    arq.SaveAs(caminho);
                    
                    byte[] toBytes = System.IO.File.ReadAllBytes(caminho);

                    Anexo a = new Anexo();
                    a.arquivo = toBytes;
                    a.nome = arq.FileName;
                    AnexoBO b = new AnexoBO();
                    b.Gravar(a);
                }
                else
                {


                    ViewBag.aviso = "arquivo com extensão inválida!";
                }

               

            }
            


            return View();
        }


        [HttpGet]
        public ActionResult Upload()
        {


            return View();
        }


        [HttpPost]
        public ActionResult Mensagem(MensagemEmail obj)
        {

            if (ModelState.IsValid)
            {

                MensagemEmailBO service = new MensagemEmailBO();
                service.EnviarMsg(obj);


            }


            

            return View("Index");
        }


        [HttpGet]
        public ActionResult Mensagem()
        {


            return View("Index");
        }




    }
}