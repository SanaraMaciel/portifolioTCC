using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.BO;
using ProjetoIntranet.Models.Entity;

namespace ProjetoIntranet.Models.DAO
{
    public class AreaPretendidaDAO
    {
         public void Insert(AreaPretendida area) // insere cargos na tabela área pretendida
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO areaPretendida (cargo) values(@cargo)  ";

            comando.Parameters.AddWithValue("@cargo", area.cargo);
            
            ConexaoBanco.CRUD(comando);

        }

        public void Update(AreaPretendida area)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE area SET cargo=@cargo WHERE id=@areaId ";


            comando.Parameters.AddWithValue("@cargo", area.cargo);
            comando.Parameters.AddWithValue("@areaId", area.id);

            ConexaoBanco.CRUD(comando);

        }

        public void Delete(int  id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE * FROM areaPretendida WHERE id=@areaId ";

            comando.Parameters.AddWithValue("@areaId", id);
            ConexaoBanco.CRUD(comando);

        }

        public AreaPretendida ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM areaPretendida WHERE id=@areaId ";

            comando.Parameters.AddWithValue("@areaId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            AreaPretendida area = new AreaPretendida();

            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {

                dr.Read();
                area.cargo = (string)dr["cargo"];
                area.id = (int)dr["id"];

            }
            else
            {

                area = null;
            }

            return area;

        }

        public IList<AreaPretendida> listarPorArea(string cargo) // retorna uma lista de áreas pretendidas onde o cargo bate
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT areaPrentedida.cargo FROM areaPretendida WHERE cargo LIKE @cargo" +


            comando.Parameters.AddWithValue("@cargo", "%" + cargo + "%");

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            IList<AreaPretendida> areas = new List<AreaPretendida>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AreaPretendida area = new AreaPretendida();

                    area.cargo= (string)dr["cargo"];
                    area.id = (int)dr["id"];

                    areas.Add(area);
                }

            }
            else
            {
                areas = null;
            }

            return areas;
        }


        public List<AreaPretendida> ListarAreas() // lista todas as areas pretendidas
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM areaPretendida";

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            List<AreaPretendida> areas = new List<AreaPretendida>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AreaPretendida area = new AreaPretendida();

                    area.cargo = (string)dr["cargo"];
                    area.id = (int)dr["id"];
                    areas.Add(area);
                }

            }
            else
            {
                areas = null;
            }

            return areas;

        }

    }
}
