
using ProjetoIntranet.Models.DAO;
using ProjetoIntranet.Models.Entity;
using System.Collections.Generic;


namespace ProjetoIntranet.Models.BO
{
    public class GrupoXMenuBO
    {

        public void Gravar( List<int> listaDeLinks, int idGrupo){

            GrupoXMenuDAO grupoEMenu = new GrupoXMenuDAO();

            grupoEMenu.Gravar(listaDeLinks,idGrupo);
        
        
        }

        public void Excluir(int idLink,int idGrupo)
        {

            GrupoXMenuDAO grupoEMenu = new GrupoXMenuDAO();
            grupoEMenu.Excluir(idLink, idGrupo);


        }

        public List<CadastroMenu> ListarPorGrupo(List<GrupoUsuario> grupos)
        {

            GrupoXMenuDAO grupoEMenu = new GrupoXMenuDAO();

            List<CadastroMenu> listaFiltrada = new List<CadastroMenu>();

            foreach (var item in grupoEMenu.ListarPorFiltroGrupo(grupos))
            {

                listaFiltrada.Add(item.Value);


            }

            return  listaFiltrada;

        }


        public List<CadastroMenu> ListarPorGrupo(int grupo)
        {

            GrupoXMenuDAO grupoEMenu = new GrupoXMenuDAO();

            List<CadastroMenu> listaFiltrada = new List<CadastroMenu>();

            foreach (var item in grupoEMenu.ListarPorFiltroGrupo(grupo))
            {

                listaFiltrada.Add(item.Value);


            }

            return listaFiltrada;

        }


    }
}