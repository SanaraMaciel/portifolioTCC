using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class AnexoDAO
    {

        

        public int Insert(Anexo anexo) // insere dados na tabela anexo retornando um ID
        {

            SqlConnection conn = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO anexo (nome,arquivo,tipoArquivo) values(@nome,@arquivo,@tipoArquivo); SELECT CAST(scope_identity() AS int);";

            comando.Parameters.AddWithValue("@nome", anexo.nome);
            comando.Parameters.AddWithValue("@arquivo", anexo.arquivo);
            comando.Parameters.AddWithValue("@tipoArquivo", anexo.tipoArquivo);

            conn = ConexaoBanco.Conectar();
            comando.Connection = conn;
            int id = (Int32)comando.ExecuteScalar();


            ConexaoBanco.CRUD(comando);

            return id;



        }

        public void Update(Anexo anexo)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE anexo SET nome=@nome, arquivo=@arquivo, tipoArquivo=@tipoArquivo WHERE id=@anexoId ";


            comando.Parameters.AddWithValue("@nome", anexo.nome);
            comando.Parameters.AddWithValue("@arquivo", anexo.arquivo);
            comando.Parameters.AddWithValue("@tipoArquivo", anexo.tipoArquivo);
            comando.Parameters.AddWithValue("@anexoId", anexo.id);

            ConexaoBanco.CRUD(comando);

        }

        public void Delete(int  id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE * FROM anexo WHERE id=@anexoId ";

            comando.Parameters.AddWithValue("@anexoId", id);
            ConexaoBanco.CRUD(comando);

        }

        public Anexo ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM anexo WHERE id=@anexoId ";

            comando.Parameters.AddWithValue("@anexoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            Anexo anexo = new Anexo();

            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {

                dr.Read();
                anexo.nome = (string)dr["nome"];
                anexo.arquivo = (Byte[])dr["arquivo"];
                anexo.tipoArquivo = (string)dr["tipoArquivo"];
                anexo.id = (int)dr["id"];

            }
            else
            {

                anexo = null;
            }

            return anexo;

        }

        public IList<Anexo> ListarPorNome(string nome) //seleciona o anexo por nome
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM anexo WHERE descricao LIKE @nome" +


            comando.Parameters.AddWithValue("@nome", "%" + nome + "%");

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);


            List<Anexo> anexos = new List<Anexo>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Anexo anexo = new Anexo();

                    anexo.nome = (string)dr["nome"];
                    anexo.arquivo = (byte[])dr["arquivo"];
                    anexo.tipoArquivo = (string)dr["tipoArquivo"];
                    anexo.id = (int)dr["id"];

                    anexos.Add(anexo);
                }

            }
            else
            {
                anexos = null;
            }

            return anexos;
        }


        public Anexo Download(int id) // responsável por fazer o donwload do anexo
        {
            byte[] bytes;
            string fileName, contentType;

            SqlConnection conn = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            comando.CommandText = "select nome, arquivo, tipoConteudo from anexo where id=@anexoId";
            comando.Parameters.AddWithValue("@anexoId", id);
            comando.Connection = conn;
            conn.Open();
            Anexo anexo = new Anexo();

            SqlDataReader sdr = comando.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                bytes = (byte[])sdr["arquivo"];
                contentType = sdr["tipoConteudo"].ToString();
                fileName = sdr["nome"].ToString();
            }
            else
            {
                anexo = null;
            }

            conn.Close();
            return anexo;
        }


    }
}



