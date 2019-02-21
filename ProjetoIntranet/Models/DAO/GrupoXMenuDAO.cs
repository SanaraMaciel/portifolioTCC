
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProjetoIntranet.Models.DAO
{
    public class GrupoXMenuDAO
    {


        public void Gravar( List<int> listaDeLinks, int idGrupo) // grava associação entra grupo de usuairio e os links que esse grupo pode acessar
        {


            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO [ExtranetFenix].[dbo].[grupoXMenu] (grupoUsuario_fk,Menu_fk) VALUES (@idGrupo,@idLink)";

                foreach (int x in listaDeLinks)
                {
                    comando.Parameters.AddWithValue("@idLink", x);
                    comando.Parameters.AddWithValue("@idGrupo", idGrupo);

                    ConexaoBanco.CRUD(comando);

                    comando.Parameters.Clear();
                }



            }
            catch  {

                throw ;

            }


        }


        public void Excluir(int idLink, int idGrupo) // exclui associação entre grupo e links/itens de menu
        {


            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM [ExtranetFenix].[dbo].[grupoXMenu] where [ExtranetFenix].[dbo].[grupoXMenu].grupoUsuario_fk = @idGrupo and [ExtranetFenix].[dbo].[grupoXMenu].Menu_fk = @idLink";

              
                
                    comando.Parameters.AddWithValue("@idLink", idLink);
                    comando.Parameters.AddWithValue("@idGrupo", idGrupo);

                    ConexaoBanco.CRUD(comando);
                

            }
            catch 
            {
                throw ;

            }


        }


        public Dictionary<int, CadastroMenu> ListarPorFiltroGrupo(List<GrupoUsuario> grupos) // retorna uma lista de links/itens de menu filtrado por 2 ou mais grupos de usuario
        {

          //  List<CadastroMenu> ListaDelinks = null;
            Dictionary<int, CadastroMenu> conjunto = new Dictionary<int, CadastroMenu>();

            try
            {

                foreach(var grupo in grupos)
                {


                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT [ExtranetFenix].[dbo].menu.* FROM [ExtranetFenix].[dbo].[grupoXMenu] JOIN [ExtranetFenix].[dbo].menu on [ExtranetFenix].[dbo].grupoXMenu.Menu_fk = [ExtranetFenix].[dbo].menu.id where [ExtranetFenix].[dbo].[grupoXMenu].grupoUsuario_fk = @idGrupo";
                    comando.Parameters.AddWithValue("@idGrupo", grupo.id);
                    SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                    if (dr.HasRows)
                    {

                        CadastroMenu link = new CadastroMenu();



                        while (dr.Read())
                        {

                            link.id = Convert.ToInt32(dr["id"]);

                            link.nome = Convert.ToString(dr["nome"]);

                            link.ordem = Convert.ToInt32(dr["ordem"]);

                            link.codigoPai = Convert.ToInt32(dr["codigoPai"]);

                            link.url = Convert.ToString(dr["URL"]);

                            

                            if (!conjunto.ContainsKey(link.id))
                            {

                                conjunto.Add(link.id, link);

                            }




                            link = new CadastroMenu();




                        }



                    }


                }

               


                

              

               

               


            }
            catch (Exception e)
            {

                conjunto = null;
                throw e;

            }


            return conjunto;

        }


        public Dictionary<int, CadastroMenu> ListarPorFiltroGrupo(int grupo) // retorna conjuto de link/itens de menu filtra por apenas um grupo
        {

            //  List<CadastroMenu> ListaDelinks = null;
            Dictionary<int, CadastroMenu> conjunto = new Dictionary<int, CadastroMenu>();

            try
            {

               


                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT [ExtranetFenix].[dbo].menu.* FROM [ExtranetFenix].[dbo].[grupoXMenu] JOIN [ExtranetFenix].[dbo].menu on [ExtranetFenix].[dbo].grupoXMenu.Menu_fk = [ExtranetFenix].[dbo].menu.id where [ExtranetFenix].[dbo].[grupoXMenu].grupoUsuario_fk = @idGrupo";
                    comando.Parameters.AddWithValue("@idGrupo", grupo);
                    SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                    if (dr.HasRows)
                    {

                        CadastroMenu link = new CadastroMenu();



                        while (dr.Read())
                        {

                            link.id = Convert.ToInt32(dr["id"]);

                            link.nome = Convert.ToString(dr["nome"]);

                            link.ordem = Convert.ToInt32(dr["ordem"]);

                            link.codigoPai = Convert.ToInt32(dr["codigoPai"]);

                            link.url = Convert.ToString(dr["URL"]);



                            if (!conjunto.ContainsKey(link.id))
                            {

                                conjunto.Add(link.id, link);

                            }




                            link = new CadastroMenu();




                        }



                    }

                    




            }
            catch (Exception e)
            {

                conjunto = null;
                throw e;

            }


            return conjunto;

        }

    }
}