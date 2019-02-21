using ProjetoIntranet.Models.DAO;
using System;
using System.Collections.Generic;


namespace ProjetoIntranet.Models.BO
{
    public class UsuarioEGruposBO
    {


        public void Gravar(String idUsuario, String[] idGrupos)
        {
            List<int> grupos = new List<int>();

            int idUser = Convert.ToInt32(idUsuario);

            foreach(String str in idGrupos)
            {
                
                grupos.Add(Convert.ToInt32(str));


            }

            UsuarioEGruposDAO ueg = new UsuarioEGruposDAO();
            ueg.Gravar(idUser, grupos);


        }


        public List<int> listaDeGrupoAQualPertence(int idUsuario)
        {

            

            UsuarioEGruposDAO uXg = new UsuarioEGruposDAO();



            return uXg.listaDeGrupoAQualPertence(idUsuario);

        }

    }
}