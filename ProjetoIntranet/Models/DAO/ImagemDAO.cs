using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoIntranet.Models.Entity;
using System.Data.SqlClient;
using System.Data;


namespace ProjetoIntranet.Models.DAO
{
    public class ImagemDAO
    {
        public void Insert(List<Imagem> lista, Imagem arq, int id) //insere uma ou mais  imagens na tabela imagem
        {
            try
            {
                
                int x = 0;

                while (lista != null && x < lista.Count)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO imagem (nome,imagem,postagem_fk,tipoArquivo)values(@nome,@imagem,@postagem_fk,@tipoArquivo);";

                    cmd.Parameters.AddWithValue("@nome", lista[x].nome);
                    cmd.Parameters.AddWithValue("@imagem", lista[x].imagem);
                    cmd.Parameters.AddWithValue("@postagem_fk", id);
                    cmd.Parameters.AddWithValue("@tipoArquivo", lista[x].tipoArquivo);

                    ConexaoBanco.CRUD(cmd);
                    x++;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Alterar(List<Imagem> lista, Imagem arq, int idPostagem) // altera a imagem de acordo com o ID
        {
            try
            {
                int x = 0;

                while (lista != null && x < lista.Count)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE  imagem set nome=@nome, imagem=@imagem, postagem_fk=@postagem_fk, tipoArquivo=@tipoArquivo" +
                    "  WHERE imagem.postagem_fk= @postagem_fk;";

                    cmd.Parameters.AddWithValue("@nome", lista[x].nome);
                    cmd.Parameters.AddWithValue("@imagem", lista[x].imagem);
                    cmd.Parameters.AddWithValue("@postagem_fk", idPostagem);
                    cmd.Parameters.AddWithValue("@tipoArquivo", lista[x].tipoArquivo);

                    ConexaoBanco.CRUD(cmd);
                    x++;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void Delete(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE * FROM imagem WHERE id=@id ";

            comando.Parameters.AddWithValue("@id", id);
            ConexaoBanco.CRUD(comando);

        }

        public Imagem ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM imagem WHERE Id=@Id ";

            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            Imagem imagem = new Imagem();

            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {
               
                dr.Read();
                imagem.id = (int)dr["id"];
                imagem.nome = (string)dr["nome"];
                

            }
            else
            {
                imagem = null;
            }
            return imagem;
        }

        public List<Imagem> ListarImagens(int postagemId) // retorna uma lista de imagens de acordo com o ID de postagem associado
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT imagem.imagem, imagem.tipoArquivo from imagem left join postagem on postagem.id= imagem.postagem_fk " +
                "where imagem.postagem_fk = @id and imagem.imagem is not null";



            comando.Parameters.AddWithValue("@id", postagemId);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            List<Imagem> imagensPostagens = new List<Imagem>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    Imagem imagem = new Imagem();

                    imagem.imagem = (byte[])dr["imagem"];
                    imagem.tipoArquivo = (string)dr["tipoArquivo"];
                    imagensPostagens.Add(imagem);


                }

            }

            else
            {
                imagensPostagens = null;
            }

            return imagensPostagens;

        }


    }
}
