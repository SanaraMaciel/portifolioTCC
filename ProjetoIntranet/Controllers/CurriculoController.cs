using PagedList;
using ProjetoIntranet.Helpers;
using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoIntranet.Controllers

{

    [AutorizacaoCustomizada]
    public class CurriculoController : BaseController
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
            return RedirectToAction("Curriculo");
        }

        private IList<SelectListItem> tempListArea = new List<SelectListItem>();
        private IList<SelectListItem> tempListEstadoCivil = new List<SelectListItem>();
        private AreaPretendidaBO areaBO = new AreaPretendidaBO();
        private EstadoCivilBO estadoBO = new EstadoCivilBO();
        private static IList<Curriculo> curriculos = null;

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Curriculo() // action responsavel pela requisiçao GET e montar a view
        {
            foreach (AreaPretendida a in areaBO.ListarAreas())
            {
                tempListArea.Add(new SelectListItem { Text = a.cargo, Value = Convert.ToString(a.id) });
            }

            ViewBag.Areas = tempListArea;


            foreach (EstadoCivil a in estadoBO.ListarEstadoCivil())
            {
                tempListEstadoCivil.Add(new SelectListItem { Text = a.estado, Value = Convert.ToString(a.id) });

            }

            ViewBag.Estados = tempListEstadoCivil;


            ViewBag.list = tempListEstadoCivil.ToArray();
            ViewBag.areas = tempListArea.ToArray();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Mensagem(MensagemEmail obj) //ainda em construção
        {

            if (ModelState.IsValid)
            {

                MensagemEmailBO service = new MensagemEmailBO();
                service.EnviarMsg(obj);


            }




            return RedirectToAction("Curriculo");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Mensagem() // ainda em construção
        {


            return RedirectToAction("Curriculo");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Curriculo(Curriculo cv,HttpPostedFileBase arq,FormCollection form) //action responsavel por enviar por POST um formulario de curriculo
        {

           

            if (form["site"] == null) {


                cv.siteBlog = " ";

            }

            if ( form["sky"] == null){

                cv.skype = " ";

            }

            if (form["tel"] == null)
            {

                cv.telefoneFixo = " ";

            }

            var remu =  form["remuneracao"] as String;
            cv.remuneracao = Convert.ToDouble(remu);


            cv.estado.id = Convert.ToInt32(form["estado"]);
            cv.area.id = Convert.ToInt32(form["area"]);

            
                CurriculoBO cBO = new CurriculoBO();

                var dataEnvio = DateTime.Now;

                if (arq != null && arq.ContentLength > 0) // verrifica se o anexo não é nulo e verifica sua extensão
                {
                    var extensoePermitidas = new String[] { ".pdf", ".docx", ".doc" };

                    var checarExtensao = Path.GetExtension(arq.FileName).ToLower();

                    var tipo = Path.GetExtension(arq.FileName).ToLower();

                    if (extensoePermitidas.Contains(checarExtensao))
                    {
                        string caminho = Path.Combine(Server.MapPath("~/arquivos"), Path.GetFileName(arq.FileName));
                        arq.SaveAs(caminho);
                        Anexo anx = new Anexo();
                        anx.arquivo = System.IO.File.ReadAllBytes(caminho);
                        anx.nome = arq.FileName;
                        anx.tipoArquivo = arq.ContentType;
                        
                       
                        cBO.Gravar(cv, anx, dataEnvio);

                    }
                    else
                    {
                        ViewBag.aviso = "arquivo com extensão inválida!";
                    }

                }
                else
                {
                
                    
                    cBO.Gravar(cv, dataEnvio);
                }


            


           

            return RedirectToAction("Curriculo");


        }

        private void CacheCurriculos() //uma cache de curriculos
        {
            CurriculoBO cuBO = new CurriculoBO();
            curriculos = cuBO.ListarCurriculos();

        }

        [HttpGet]
        [AutorizacaoCustomizada(Roles ="ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarCurriculos(int? pagina) // paginação para listar curriculos 
        {
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            int tamanhoPagina = 4;
            int numeroPagina = pagina ?? 1;
            

            CacheCurriculos();

            TempData["lista"] = curriculos;
           curriculos.ToList();
            return View(curriculos.ToPagedList(numeroPagina, tamanhoPagina));


        }
        [HttpPost]
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult ListarCurriculos(int? pagina,FormCollection form) // paginação para listar curriculos 
        {
            int tamanhoPagina = 4;
            int numeroPagina = pagina ?? 1;
            IList<Curriculo> lista =null;
            CurriculoBO cuBO = new CurriculoBO();

          

            if (form["dataInicial"] == null && form["dataFim"] == null)
            {
               
                if (form["area"] != String.Empty)
                {

                    lista = cuBO.ListarPorArea(form["area"].ToString());

                }
                else
                {

                    lista = curriculos;

                }


            }
            else
            {

                lista = cuBO.ListarPorData(form["dataInicial"].ToString(), form["dataFim"].ToString());

            }


            TempData["lista"] = lista;


            curriculos.ToList();
            return View(lista.ToPagedList(numeroPagina, tamanhoPagina));


        }

               
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult Excluir(int id)
        {
            CurriculoBO cuBO = new CurriculoBO();
            Curriculo cu = new Curriculo();
            cuBO.Delete(id);
            return RedirectToAction("ListarCurriculos");
        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        [HttpGet]
        [OutputCache(Duration = 120)]
       
        public JsonResult Detalhes() // responsavel po retornar um Json para mostrar detelhes de um curriculo
        {


            var curriculos = TempData["lista"] as IList<Curriculo>;
           

            
            return Json(curriculos,JsonRequestBehavior.AllowGet);

        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,RECURSOSHUMANOS")]
        public ActionResult Arquivo(int id)
        {
            AnexoBO anexoBO = new AnexoBO();
            Anexo a = new Anexo();

            a = anexoBO.ReadById(id);

            if (a != null)
            {
                return File(a.arquivo, a.tipoArquivo, a.nome);
            }

            return RedirectToAction("ListarCurriculos");

        }

    }
}