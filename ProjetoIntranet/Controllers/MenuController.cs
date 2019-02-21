using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;

using Newtonsoft.Json;
using ProjetoIntranet.Models.Entity;

using System.IO;
using System.Web.UI;


using ProjetoIntranet.MV;
using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Helpers;
using System.Web;

namespace ProjetoIntranet.Controllers
{
    public class MenuController : BaseController
    {
        public object StringWr { get; private set; }
      
        private CadastroMenuBO cmBO;

        private IList<CadastroMenu> linksDeMenu;

        private  List<CadastroMenu> ListaLinksFiltrados ;

        private static IList<SelectListItem> itens = new List<SelectListItem>();

        private HashSet<int> ordemUsuada = new HashSet<int>();

       private static StringWriter stringWriter = new StringWriter();
       private static  HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        
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
                case "AdicionarLinks":
                    return RedirectToAction("AdicionarLinks");

                case "AssociarLinksEGrupos":
                    return RedirectToAction("AssociarLinksEGrupos");

                case "DesassociarLinksEGrupos":
                    return RedirectToAction("DesassociarLinksEGrupos");

                case "EditarMenu":
                    return RedirectToAction("EditarMenu");

                case "ListarLinks":
                    return RedirectToAction("ListarLinks");

                default: return RedirectToAction("Principal", "Home");

            }

        }


        [ChildActionOnly]
        public ActionResult MenuDinamico()  // responsavel por renderizar o menu na view enviando html
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;

            var usuarioLogado = Session["UsuarioLogado"] ;

            if (usuarioLogado.Equals(false)) // usar a flag de usuario para rederizar os links uma vez por usuario
            {
                Session.Remove("UsuarioLogado");

                Session.Add("UsuarioLogado", true);

                GrupoXMenuBO gXm = new GrupoXMenuBO();

                String MenuView = String.Empty;

              

                linksDeMenu = gXm.ListarPorGrupo(usr.getGrupos());



                Dictionary<int, ItemDeMenu> mapa = preencherMapa(linksDeMenu);


                List<ItemDeMenu> lista = preencherList(mapa);




                foreach (var item in lista)
                {
                    //if (item.pai == null)
                    //{


                       

                    //}
                    percorrer(item);

                }

            }

            


            
           
               
                return PartialView("_PartialDynamicMenu", (Object)stringWriter.ToString());

            
        }

        private Dictionary<int, ItemDeMenu> preencherMapa(IList<CadastroMenu> links) // responsavel por filtrar links repetidos fruto de uma intersecção entre gurpos
        {
            Dictionary<int, ItemDeMenu> mapa = new Dictionary<int, ItemDeMenu>(); 
           



            foreach (var item in links) 
            {


                ItemDeMenu i = new ItemDeMenu(); 

                i.id = item.id;  
                i.ordem = item.ordem;  
                i.nome = item.nome;  
                i.url = item.url;  

                mapa.Add(item.id, i);  


            }

           

            foreach (CadastroMenu entity in links) 
            {
               
                ItemDeMenu i = new ItemDeMenu();  

                i.id = entity.id; 
                i.ordem = entity.ordem;  
                i.nome = entity.nome;  
                i.url = entity.url;  

                if (!entity.codigoPai.Equals(0))  
                {
                    ItemDeMenu pai = new ItemDeMenu();  

                    mapa.TryGetValue(entity.codigoPai, out pai);  


                    pai.filhosList.Add(i);  
                    i.pai = pai;  


                }

               

                if (mapa.ContainsKey(entity.id))  
                {
                    ItemDeMenu it;  
                    mapa.TryGetValue(entity.id, out it);  



                    if (it.filhosList.Count == 0)  
                    {
                        mapa.Remove(entity.id);  
                        mapa.Add(entity.id, i);  

                    }
                    else
                    {

                        List<ItemDeMenu> temp = new List<ItemDeMenu>();  

                        foreach ( var item in it.filhosList)  
                        {
                            i.filhosList.Add(item);  

                        }

                        mapa.Remove(entity.id);  
                        mapa.Add(entity.id, i);  



                    }

                   

                }

            }
           

            return mapa;  //  1
        } 


        private static List<ItemDeMenu> preencherList(Dictionary<int, ItemDeMenu> mapa) // prepara uma lista de itens de menu
        {
            List<ItemDeMenu> itemlista = new List<ItemDeMenu>();

            foreach (var id in mapa.Keys)
            {

                ItemDeMenu it;
                    mapa.TryGetValue(id,out it);

                if (it.pai == null)
                {

                    itemlista.Add(it);

                }
               
            }



            return itemlista;

        }

        private static void percorrer(ItemDeMenu itemMenu) /// responsvale por percorres uma lista eceriar o codigo html para cada item de menu preparando para ser renderizado na view
        {


            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "li-menu"); 
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Li); 
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Href, itemMenu.url); 
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.A); 
            htmlWriter.Write(itemMenu.nome); 
            htmlWriter.RenderEndTag(); //A  
            htmlWriter.RenderEndTag();//li  

            foreach (var item in itemMenu.filhosList) 
            {

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Ul); 

                percorrer(item); 

                htmlWriter.RenderEndTag();//ul//  
            }

        }

         [ChildActionOnly]
        public static void descarregar() // desaloca objetos em desuso
        {

            stringWriter.Dispose();
            htmlWriter.Dispose();

            stringWriter = new StringWriter();

            htmlWriter = new HtmlTextWriter(stringWriter);

        }

        

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpGet]
        public ActionResult EditarMenu() // responsavel por chamar a view editer menu
        {
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            cmBO = new CadastroMenuBO();

            

            return View(cmBO.listaDeLinks());
        }

        [HttpPost]
        
         public ActionResult EditarMenu(String dados )
        {

            CadastroMenuBO cmBO = new CadastroMenuBO();

            List<CadastroMenu> listaDeLinks = null;

            listaDeLinks = JsonConvert.DeserializeObject<List<CadastroMenu>>(dados);

            Dictionary<int, CadastroMenu> conjunto = new Dictionary<int, CadastroMenu>();

            foreach (CadastroMenu item in listaDeLinks)
            {

                if (conjunto.ContainsKey(item.id))
                {

                    conjunto.Remove(item.id);

                    conjunto.Add(item.id, item);


                }
                else {

                    conjunto.Add(item.id, item);
                }

                
                
            }

            

            foreach (var valor in conjunto)
            {
               

                cmBO.Gravar(valor.Value);
               

            }
           

            return RedirectToAction("EditarMenu");
        }


       

        public JsonResult JsonLinksFilhos() // responsavel por retonar para view 
        {
            CadastroMenuBO cmBO = new CadastroMenuBO();

            return Json(cmBO.listaDeLinks(), JsonRequestBehavior.AllowGet);
        }

        

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpPost]
        public ActionResult AdicionarLinks(String link)
        {

            CadastroMenu cm = JsonConvert.DeserializeObject<CadastroMenu>(link);


            if(cm.nome != "" &&  cm.url != "")
            {

                CadastroMenuBO cmBO = new CadastroMenuBO();

                cmBO.Gravar(cm);

            }

                        

            return View();
        }
        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpGet]
        public ActionResult AdicionarLinks()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;



            return View();
        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpGet]
        public ActionResult ListarLinks()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;

            CadastroMenuBO cmBO = new CadastroMenuBO();

            

            return View(cmBO.listaDeLinks());
        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpPost]
        public ActionResult ListarLinks(int id)
        {

            
            return View();
        }

        
        public void Excluir(int id)
        {

            CadastroMenuBO cmBO = new CadastroMenuBO();

            cmBO.Delete(id);

           
            
        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpGet]
        public ActionResult AssociarLinksEGrupos()
        {
            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;


            CadastroMenuBO cBO = new CadastroMenuBO();
            GrupoUsuarioBO gBO = new GrupoUsuarioBO();

            MVlinkEGrupo MV = new MVlinkEGrupo();

            List<SelectListItem> listaGrupo = new List<SelectListItem>();

            foreach (var obj in gBO.ListarGrupos())
            {

               listaGrupo.Add(new SelectListItem { Text = obj.nome, Value = obj.id.ToString() });


            }

            MV.ListaDeGrupos = listaGrupo;
            MV.ListaDeLinks = cBO.listaDeLinks().ToList();



            return View(MV);
        }


        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpPost]
        public ActionResult AssociarLinksEGrupos(String associarGrupos)
        {



            MVlinkEGrupo MV = JsonConvert.DeserializeObject<MVlinkEGrupo>(associarGrupos);
        
            GrupoXMenuBO gXm = new GrupoXMenuBO();
                                

            gXm.Gravar(MV.LinksIds, MV.GrupoId);


            
            return RedirectToAction("AssociarLinksEGrupos");
        }

        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpGet]
        public ActionResult DesassociarLinksEGrupos()
        {

            Usuario usr = Session["UsuarioAtual"] as Usuario;


            ViewBag.UsuarioLogado = usr.nome;


            CadastroMenuBO cBO = new CadastroMenuBO();
            GrupoUsuarioBO gBO = new GrupoUsuarioBO();

            MVlinkEGrupo MV = new MVlinkEGrupo();

            List<SelectListItem> listaGrupo = new List<SelectListItem>();

            foreach (var obj in gBO.ListarGrupos())
            {

                listaGrupo.Add(new SelectListItem { Text = obj.nome, Value = obj.id.ToString() });


            }

            MV.ListaDeGrupos = listaGrupo;

            TempData["grupos"] = listaGrupo;

            MV.ListaDeLinks = cBO.listaDeLinks().ToList();
          
            return View(MV);
        }



        [AutorizacaoCustomizada(Roles = "ADMINISTRADOR,TECNOLOGIA")]
        [HttpPost]
        public ActionResult DesassociarLinksEGrupos(String x)
        {
            GrupoXMenuBO gXm = new GrupoXMenuBO();

            ListaLinksFiltrados = gXm.ListarPorGrupo(Convert.ToInt32(x));

            MVlinkEGrupo MV = new MVlinkEGrupo();

          

            MV.ListaDeGrupos = TempData["grupos"] as List<SelectListItem>;

          


            return View(MV);
                
        }

       
        public JsonResult ListarLinksAssociacao(String GrupoSelecionado)
        {
            GrupoXMenuBO gXm = new GrupoXMenuBO();
            MVlinkEGrupo MV = new MVlinkEGrupo();

            if (GrupoSelecionado != "")
            {
                ListaLinksFiltrados = gXm.ListarPorGrupo(Convert.ToInt32(GrupoSelecionado));

               

                MV.ListaDeLinks = ListaLinksFiltrados;


            }
            else
            {

                MV.ListaDeLinks = null;

            }




            return Json(MV.ListaDeLinks,JsonRequestBehavior.AllowGet);
        }


        public void ExcluirAssociacao(String ids)
        {

            GrupoXMenuBO gXm = new GrupoXMenuBO();

            MVlinkEGrupo MV = JsonConvert.DeserializeObject<MVlinkEGrupo>(ids);

            gXm.Excluir(MV.LinksIds[0], MV.GrupoId);

        }

    }

}