using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class CadastroMenuDAO
    {
        public void Insert(CadastroMenu menu) // responsavel po inserir um link para ser usado como item de menu
        {
            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Menu (nome,url,ordem,codigoPai) values(@nome,@url,@ordem,@codigoPai)  ";

                comando.Parameters.AddWithValue("@url", menu.url);
                comando.Parameters.AddWithValue("@ordem", menu.ordem);
                comando.Parameters.AddWithValue("@nome", menu.nome);
                comando.Parameters.AddWithValue("@codigoPai", menu.codigoPai);

                ConexaoBanco.CRUD(comando);

            } catch  {
                throw ;
            }

            

        }

        public void Update(CadastroMenu menu)
        {

            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE Menu SET nome=@nome, url=@url, ordem=@ordem,codigoPai=@codigoPai WHERE id=@menuId ";

                comando.Parameters.AddWithValue("@nome", menu.nome);
                comando.Parameters.AddWithValue("@url", menu.url);
                comando.Parameters.AddWithValue("@ordem", menu.ordem);

                comando.Parameters.AddWithValue("@codigoPai", menu.codigoPai);
                comando.Parameters.AddWithValue("@menuId", menu.id);

                ConexaoBanco.CRUD(comando);

            } catch {

                throw ;
            }

           

        }

        public void Delete(int id)
        {

            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE [ExtranetFenix].[dbo].[grupoXMenu] WHERE Menu_fk = @menuId"; 

                comando.Parameters.AddWithValue("@menuId", id);
                ConexaoBanco.CRUD(comando);

                comando.Dispose();


                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM Menu WHERE id=@menuId ";

                comando.Parameters.AddWithValue("@menuId", id);
                ConexaoBanco.CRUD(comando);

            } catch  {

                throw ;
            }



           

        }

        public CadastroMenu ReadById(int id)
        {
            CadastroMenu menu = new CadastroMenu();

            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM Menu WHERE id=@menuId ";

                comando.Parameters.AddWithValue("@menuId", id);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                

                if (dr.HasRows)
                {

                    dr.Read();
                    menu.nome = Convert.ToString(dr["nome"]);
                    menu.url = Convert.ToString(dr["url"]);
                    menu.ordem = Convert.ToInt32(dr["ordem"]);

                    menu.codigoPai = Convert.ToInt32(dr["codigoPai"]);
                    menu.id = Convert.ToInt32(dr["id"]);


                }
               

            } catch{

                menu = null;
                throw ;

            }

           

            return menu;

        }

        public IList<CadastroMenu> ListarTodosLinks() //retorna um lista de links/itens de menu
        {

            IList<CadastroMenu> menus = new List<CadastroMenu>();

            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM Menu ";


                

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CadastroMenu menucadastro = new CadastroMenu();

                        menucadastro.nome = Convert.ToString(dr["nome"]);
                        menucadastro.url = Convert.ToString(dr["url"]);
                        menucadastro.ordem = Convert.ToInt32(dr["ordem"]);
                        menucadastro.codigoPai = Convert.ToInt32(dr["codigoPai"]);
                        menucadastro.id = Convert.ToInt32(dr["id"]);


                        menus.Add(menucadastro);
                    }

                }
              
            } catch {
                menus = null;
                throw ;

            }

           

            return menus;
        }

        

    }
}
