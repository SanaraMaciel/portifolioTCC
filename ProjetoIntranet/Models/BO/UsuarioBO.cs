using ProjetoIntranet.Models.DAO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;



namespace ProjetoIntranet.Models.BO
{
    public class UsuarioBO
    {
        public void Gravar(Usuario usuario)
        {

            UsuarioDAO usuarioDAO = new UsuarioDAO();

            if (usuario.id > 0)
            {
                //altera
                usuarioDAO.Update(usuario);
            }
            else
            {
                //insere
                usuarioDAO.Insert(usuario);
            }

        }

        public void Desativar(int id)
        {
           
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.Desativar(id);

        }

        public void Ativar(int id)
        {

            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.Ativar(id);

        }


        public Usuario PesquisarUsuario(string usuarioLogin, string senha)
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            String[] nomeUsuario = usuarioLogin.Split('@');


            return usuarioDAO.PesquisarUsuario(nomeUsuario[0], senha);

        }



        public IList<Usuario> ListarUsuarios()
        {
            UsuarioDAO uDao = new UsuarioDAO();

            return uDao.ListarTodosUsuarios();
        }

    }

}


    

