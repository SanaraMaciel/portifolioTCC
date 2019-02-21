using System;

using System.Data.SqlClient;
using System.Data;
using ProjetoIntranet.Models.Entity;

namespace ProjetoIntranet.Models.DAO
{
    public class HistoricoDAO
    {
        public void Insert(Historico historico,String mensagem) // responsavel por inserir um historico(Log) de alterações no sistema em uma tabela
        {
            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Historico (dataHora, usuario_fk,mensagem) " +
                                      "values(@dataHora,@usuario_fk,@mensagem)  ";

                comando.Parameters.AddWithValue("@dataHora", historico.dataHora);
                comando.Parameters.AddWithValue("@usuario_fk", historico.usuario.id);
                comando.Parameters.AddWithValue("@mensagem", mensagem);

                ConexaoBanco.CRUD(comando);

            }
            catch (Exception ex)
            {

                throw ex;

            }
            

        }

       

        public void Delete(Historico historico)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE * FROM historico WHERE id=@id ";

            comando.Parameters.AddWithValue("@id", historico.id);
            ConexaoBanco.CRUD(comando);

        }

        public Historico BuscarPorId(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM historico WHERE Id=@Id ";

            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            Historico historico = new Historico();

            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {
                //preenche o objeto historico
                dr.Read();
                historico.id = (int)dr["id"];
                historico.dataHora = (DateTime)dr["dataHora"];
                historico.usuario.id = (int)dr["usuario"];

            }
            else
            {
                historico = null;
            }
            return historico;
        }

    }

}

