using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;
using System.Web.Configuration;

namespace ProjetoIntranet.Models.DAO
{
    public class ConexaoBanco
    {


        public static SqlConnection conexao;


        public static SqlConnection Conectar()
        {
            

            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            conn.Open();
            return conn;
        }

        public static void CRUD(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            comando.ExecuteNonQuery();
            con.Close();
        }

        public static SqlDataReader Selecionar(SqlCommand comando)
        {

            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }


            


    }


}

