using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class EstadoCivilDAO 
    {
        public void Insert(EstadoCivil estado) // insere um estado civil na tabela estado civil
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO estadoCivil (estado) values(@estado)  ";

            comando.Parameters.AddWithValue("@estado", estado.estado);
            
            ConexaoBanco.CRUD(comando);

        }
       
        public void Update(EstadoCivil estado)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE estadoCivil SETestado=@estado WHERE id=@estadoId ";

            
            comando.Parameters.AddWithValue("@estado", estado.estado);
            comando.Parameters.AddWithValue("@estadoId", estado.id);

            ConexaoBanco.CRUD(comando);

        }

        public void Delete(EstadoCivil estado)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE * FROM estadoCivil WHERE id=@estadoId ";

            comando.Parameters.AddWithValue("@estadoId", estado.id);
            ConexaoBanco.CRUD(comando);

        }

        public EstadoCivil ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM estadoCivil WHERE id=@estadoId ";

            comando.Parameters.AddWithValue("@estadoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            EstadoCivil estado = new EstadoCivil();
            
            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {
                
                dr.Read();
                estado.estado = (String)dr["estado"];
                estado.id = (int)dr["id"];
                
            }
            else
            {

                estado = null;
            }

            return estado;

        }

        public List<EstadoCivil> ListarPorEstado(string estado)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM estadoCivil WHERE estado LIKE @estado" +


            comando.Parameters.AddWithValue("@estado", "%" + estado + "%");

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            List<EstadoCivil> estados = new List<EstadoCivil>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EstadoCivil estadoCivil = new EstadoCivil();

                    estadoCivil.estado = (string)dr["estado"];
                    estadoCivil.id = (int)dr["id"];

                    estados.Add(estadoCivil);
                }

            }
            else
            {
                estados = null;
            }

            return estados;
        }

        

        public List<EstadoCivil> ListarEstadoCivil()// retorna uma lista de estado civil
        {
            List<EstadoCivil> estados = new List<EstadoCivil>();


            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT id,estado FROM [ExtranetFenix].[dbo].[estadoCivil]ORDER BY [dbo].[estadoCivil].estado ASC";

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EstadoCivil estado = new EstadoCivil();
                      
                        estado.estado = (String)dr["estado"];
                        estado.id = (int)dr["id"];
                        estados.Add(estado);
                    }

                }
                

            } catch{

                
                estados = null;
                throw ;
            }

           

            return estados;

        }


    }
}
