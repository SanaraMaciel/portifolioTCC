using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.Models.DAO
{
    public class UsuarioEGruposDAO
    {
        public void Gravar(int idUsuario, List<int> listaDeGrupos) // grava a associação de usuario com um ou mais grupos
        {
            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO UsuarioXGrupo(usuario_fk,grupoUsuario_fk) VALUES (@idUsuario,@idGrupo)";

                foreach (int x in listaDeGrupos)
                {
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@idGrupo", x);

                    ConexaoBanco.CRUD(comando);

                    comando.Parameters.Clear();
                }
            }
            catch (Exception e)
            {

                throw e;

            }




        }


        public List<int> listaDeGrupoAQualPertence(int idUsuario) // retorna o grupos que um determinado usuario pertence
        {
            List<int> ids = null;

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT grupoUsuario_fk FROM [ExtranetFenix].[dbo].[usuarioXGrupo] WHERE [usuario_fk] = @idUsuario";
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                if (dr.HasRows)
                {

                    int id = 0;

                    ids = new List<int>();

                    while (dr.Read())
                    {
                        id = (int)dr["grupoUsuario_fk"];

                        ids.Add(id);

                    }

                }


            }
            catch (Exception ex)
            {

                ids = null;

                throw ex;

            }

            return ids;

        }

    }
}