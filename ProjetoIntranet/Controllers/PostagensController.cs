using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using PagedList;
using ProjetoIntranet.MV;
using ProjetoIntranet.Helpers;
using System.Web;

namespace ProjetoIntranet.Controllers
{
    [AutorizacaoCustomizada]
    public class PostagensController : BaseController
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
                case "CriarPostagem":
                    return RedirectToAction("CriarPostagem");

                case "ListarPostagens":
                    return RedirectToAction("ListarPostagens");

                case "ListarRascunhos":
                    return RedirectToAction("ListarRascunhos");

                case "ListarPostagensPrincipal":
                    return RedirectToAction("LIstarPostagensPrincipal");

                case "ListarPorDataRascunhos":
                    return RedirectToAction("ListarPorDataRascunhos");

                case "ListarPorTituloRascunhos":
                    return RedirectToAction("ListarPorTituloRascunhos");

                case "ListarPorDataPostagens":
                    return RedirectToAction("ListarPorDataPostagens");

                case "ListarPorTituloPostagens":
                    return RedirectToAction("ListarPorTituloPostagens");

                case "EditarRascunho":
                    return RedirectToAction("EditarRascunho");

                default: return RedirectToAction("Principal","Home");

            }

        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult CriarPostagem(Postagem post, FormCollection form, Imagem arq )
        {
           
            Imagem img = new Imagem();
            List<Imagem> lista = new List<Imagem>();
            var dataHora = DateTime.Now;

           var x = form["checkbox"];

            if (form["checkbox"] == null)
            {
                post.rascunho = 0;
            }
            else
            {
                post.rascunho = 1;
            }

            foreach (var imagem in arq.imagens)
            {
                if (imagem != null && imagem.ContentLength > 0)
                {
                    var extensoePermitidas = new String[] { ".jpg", ".jpeg", ".png" };

                    var checarExtensao = Path.GetExtension(imagem.FileName).ToLower();

                    var tipo = Path.GetExtension(imagem.FileName).ToLower();

                    if (extensoePermitidas.Contains(checarExtensao))
                    {
                        string caminho = Path.Combine(Server.MapPath("~/imagens"), Path.GetFileName(imagem.FileName));
                        imagem.SaveAs(caminho);

                        img.imagem = System.IO.File.ReadAllBytes(caminho);
                        img.nome = imagem.FileName;
                        img.tipoArquivo = imagem.ContentType;

                        lista.Add(img);
                        img = new Imagem();

                        

                    }
                    else {

                       

                        ViewData["sucesso"] = 1;

                    }
                  

                }

            }

            PostagemBO postagemBO = new PostagemBO();

            int id = postagemBO.Gravar(post, dataHora, post.rascunho); // colocar um tste de condição aqui


            Postagem postagem = postagemBO.ReadById(id);

            int postagem_fk = postagem.id;

           

                ImagemBO imagemBO = new ImagemBO();

            imagemBO.Gravar(lista, arq, postagem_fk);
            
            return RedirectToAction("CriarPostagem");

        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult CriarPostagem()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;


            return View();
        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult EditarRascunho(Postagem rascunho, Imagem arq, FormCollection form)
        {
            PostagemBO postagemBO = new PostagemBO();
            Imagem img = new Imagem();


            List<Imagem> lista = new List<Imagem>();

            var dataEnvio = DateTime.Now;

            //pegando o valor do campo rascunho  e do id da postagem
            Postagem postagem = new Postagem();
            int post = postagem.rascunho;
            int id = postagem.id;

            foreach (var imagem in arq.imagens) // colocar em um metodo
            {
                if (imagem != null && imagem.ContentLength > 0)
                {
                    var extensoePermitidas = new String[] { ".jpg", ".jpeg", ".png" };

                    var checarExtensao = Path.GetExtension(imagem.FileName).ToLower();

                    var tipo = Path.GetExtension(imagem.FileName).ToLower();

                    if (extensoePermitidas.Contains(checarExtensao))
                    {
                        string caminho = Path.Combine(Server.MapPath("~/imagens"), Path.GetFileName(imagem.FileName));
                        imagem.SaveAs(caminho);

                        img.imagem = System.IO.File.ReadAllBytes(caminho);
                        img.nome = imagem.FileName;
                        img.tipoArquivo = imagem.ContentType;
                        lista.Add(img);
                        img = new Imagem();

                    }
                    else
                    {
                        ViewBag.aviso = "arquivo com extensão inválida!";
                    }

                }

            }

            postagemBO.Alterar(rascunho, dataEnvio, lista, arq, id, post);
            return RedirectToAction("ListarRascunhos");

        }


        public JsonResult RascunhoID(int id)
        {

            PostagemBO pBO = new PostagemBO();

            var rascunho = pBO.ReadById(id);


            return Json(rascunho, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult EditarRascunho(FormCollection form,int id)
        {
            //int idPostagem = Convert.ToInt32(form["id"]);
            //TempData["IdRascunhos"] = idPostagem;
            //return View(idPostagem);

            Postagem postagem = new Postagem();
            PostagemBO postagemBO = new PostagemBO();
            postagem = postagemBO.ReadById(id);
            return View(postagem);

          
        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ExcluirRascunho(int id)
        {
            PostagemBO rascunhoBO = new PostagemBO();

            rascunhoBO.DeletePostagem(id);

            return RedirectToAction("ListarRascunhos");
        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ExcluirRascunho(int id, FormCollection form)
        {
            PostagemBO rascunhoBO = new PostagemBO();

            rascunhoBO.DeleteRascunho(id);

            return RedirectToAction("ListarRascunhos");
        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ExcluirPostagem(int id)
        {
            PostagemBO rascunhoBO = new PostagemBO();

            rascunhoBO.DeletePostagem(id);

            return RedirectToAction("ListarPostagens");
        }


        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ExcluirPostagem(int id, FormCollection form)
        {
            PostagemBO rascunhoBO = new PostagemBO();

            rascunhoBO.DeletePostagem(id);

            return RedirectToAction("ListarPostagens");
        }

       


        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarRascunhos(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;


            PostagemBO postagemBO = new PostagemBO();
            IList<Postagem> rascunhos = postagemBO.ListarRascunhos();

           if(rascunhos == null)
            {
                return View("SemRascunho");

            }
            else
            {

                TempData["TitulosRascunhos"] = rascunhos;
                rascunhos.ToList();
                return View(rascunhos.ToPagedList(numeroPagina, tamanhoPagina));

            }

               
           
            

        }

               
        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPostagens(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            PostagemBO postagemBO = new PostagemBO();
            List<Postagem> postagens = postagemBO.ListarPostagens();
            TempData["TitulosPostagens"] = postagens;
            postagens.ToList();
            return View(postagens.ToPagedList(numeroPagina, tamanhoPagina));

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ListarPostagensPrincipal(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            
            PostagemBO postagemBO = new PostagemBO();
            List<Postagem> postagens = postagemBO.ListarPostagens();
            TempData["postagens"] = postagens;
            postagens.ToList();
            return View(postagens.ToPagedList(numeroPagina, tamanhoPagina));

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ListarPostagensPrincipal(int? pagina, FormCollection form)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;


            PostagemBO postagemBO = new PostagemBO();
            List<Postagem> postagens;

            if (form["tag"] != String.Empty)
            {

                postagens = postagemBO.ListarPorTagPostagens(form["tag"].ToString());

            }
            else
            {

                postagens = postagemBO.ListarPostagensPrincipal();

            }

            TempData["postagens"] = postagens;


            postagens.ToList();
            return View(postagens.ToPagedList(numeroPagina, tamanhoPagina));

        }

       
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        [HttpGet]
        public ActionResult ListarPorTituloRascunhos(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            IList<Postagem> TitulosRascunhos;
            Postagem Rascunho = new Postagem();
            TitulosRascunhos = TempData["TitulosRascunhos"] as IList<Postagem>;
            return View(TitulosRascunhos.ToPagedList(numeroPagina, tamanhoPagina));
        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPorTituloRascunhos(string titulo, FormCollection form, int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO rascunhoBO = new PostagemBO();
            IList<Postagem> rascunhos = rascunhoBO.ListarPorTituloRascunhos(titulo);
            TempData["TitulosRascunhos"] = rascunhos;

            if(rascunhos == null)
            {
              return RedirectToAction("ListarRascunhos");

            }else
            {
                return View(rascunhos.ToPagedList(numeroPagina, tamanhoPagina));

            }

            
        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPorTituloPostagens(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            IList<Postagem> TitulosRascunhos;
            Postagem Rascunho = new Postagem();
            TitulosRascunhos = TempData["TitulosRascunhos"] as IList<Postagem>;
            return View(TitulosRascunhos.ToPagedList(numeroPagina, tamanhoPagina));
        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPorTituloPostagens(string titulo,  int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO pBO = new PostagemBO();
            IList<Postagem> postagens = pBO.ListarPorTituloPostagens(titulo);
           
            return View("ListarPostagens", postagens.ToPagedList(numeroPagina, tamanhoPagina));

        }
               
        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPorDataPostagens(string dataInicial, string dataFim,  int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO postagemBO = new PostagemBO();
            IList<Postagem> postagens = postagemBO.ListarPorDataPostagens(dataInicial, dataFim);
           // TempData["dataRascunhos"] = postagens;

            return View("ListarPostagens", postagens.ToPagedList(numeroPagina, tamanhoPagina));

        }

        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarPorDataRascunhos(string dataInicial, string dataFim, FormCollection form, int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO postagemBO = new PostagemBO();
            IList<Postagem> rascunhos = postagemBO.ListarPorDataRascunhos(dataInicial, dataFim);
            TempData["dataRascunhos"] = rascunhos;
            return View(rascunhos.ToPagedList(numeroPagina, tamanhoPagina));

        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")] 
        public ActionResult PublicarRascunho(int id)
        {
            Postagem postagem = new Postagem();
            PostagemBO postagemBO = new PostagemBO();
            postagemBO.PublicarRascunho(id);
            return RedirectToAction("ListarPostagens");
        }

        
        [HttpGet]
        public ActionResult ListarPorTagPostagens(string tag, int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO postagemBO = new PostagemBO();
            IList<Postagem> postagens = postagemBO.ListarPorTagPostagens(tag);

            TempData["lista"] = postagens;
            
            postagens.ToList();

            return View(postagens.ToPagedList(numeroPagina, tamanhoPagina));
            
            
        }

        [HttpPost]
        public ActionResult ListarPorTagPostagens(string tag, FormCollection form, int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            PostagemBO postagemBO = new PostagemBO();
            IList<Postagem> postagens;

            if (form["tag"] != String.Empty)
            {

                postagens = postagemBO.ListarPorTagPostagens(form["tag"].ToString());

            }
            else
            {

                postagens = postagemBO.ListarPostagensPrincipal();

            }

            TempData["postagens"] = postagens;


            postagens.ToList();
            return View(postagens.ToPagedList(numeroPagina, tamanhoPagina));
        }
        
    }
}