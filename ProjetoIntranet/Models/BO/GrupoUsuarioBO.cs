using System.Collections.Generic;
using System;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;

namespace ProjetoIntranet.Models.BO
{
    public class GrupoUsuarioBO
    {
      

        public void Gravar(GrupoUsuario grupoUsuario)
        {

            GrupoUsuarioDAO grupoUsuarioDAO = new GrupoUsuarioDAO();

            if (grupoUsuario.id > 0)
            {
                //altera
                grupoUsuarioDAO.Update(grupoUsuario);
            }
            else
            {
                //insere
                grupoUsuarioDAO.Insert(grupoUsuario);
            }

        }



        public Boolean Delete(GrupoUsuario grupoUsuario)
        {
            GrupoUsuarioDAO grupoUsuarioDAO = new GrupoUsuarioDAO();
            Usuario usuario = new Usuario();
            GrupoUsuarioBO grupoBO = new GrupoUsuarioBO();
            Boolean situacao = grupoBO.VerificaUsuarioDoGrupo(grupoUsuario);

            if (situacao == false )
            {
                grupoUsuarioDAO.Delete(grupoUsuario.id);
            }
            return situacao;
        }



        public Boolean VerificaUsuarioDoGrupo(GrupoUsuario grupoUsuario)
        {
            GrupoUsuarioDAO grupoDAO = new GrupoUsuarioDAO();
            return grupoDAO.VerificaUsuarioDoGrupo(grupoUsuario);
        }

        
        public String pegarPrivilegios(Usuario usr)
        {

            GrupoUsuarioDAO gubDAO = new GrupoUsuarioDAO();

            return gubDAO.pegarPrivilegios(usr.id);

        }


        public IList<GrupoUsuario> ListarGrupos() {

            

            GrupoUsuarioDAO grupoDAO = new GrupoUsuarioDAO();


            return grupoDAO.ListarGrupos();
        }


        
    }
}
