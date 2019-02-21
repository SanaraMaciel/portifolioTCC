using System;
using System.Collections.Generic;
using System.Web;
using ProjetoIntranet.Models.Entity;
using System.Data.SqlClient;
using System.Data;
using ProjetoIntranet.Models.BO;



namespace ProjetoIntranet.Models.DAO
{
    public class PostagemDAO
    {

        public int Insert(Postagem postagem, DateTime dataHora, int post)// insere uma postagem na tablea postagem com autor e data hora
        {
            Usuario usuario = HttpContext.Current.Session["UsuarioAtual"] as Usuario;
            try
            {


                SqlConnection conn = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO postagem(usuario_fk,corpo,dataHora,titulo,etiqueta,rascunho) " +
                               "values(@usuario_fk,@corpo,@dataHora,@titulo,@etiqueta,@rascunho) SELECT scope_identity();";

                comando.Parameters.AddWithValue("@usuario_fk", usuario.id);
                comando.Parameters.AddWithValue("@corpo", postagem.corpo);
                comando.Parameters.AddWithValue("@titulo", postagem.titulo);
                comando.Parameters.AddWithValue("@etiqueta", postagem.etiqueta);
                comando.Parameters.AddWithValue("@dataHora", dataHora);
                comando.Parameters.AddWithValue("@rascunho", post);

                conn = ConexaoBanco.Conectar();
                comando.Connection = conn;
                Int32 id = Convert.ToInt32(comando.ExecuteScalar());

                

                return id;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(Postagem postagem, DateTime dataHora, List<Imagem> lista, Imagem arq, int post, int id) // realiza alterações nas postagens
        {
            Usuario usuario = HttpContext.Current.Session["UsuarioAtual"] as Usuario;
            int postagemId = postagem.id;
            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE  postagem set usuario_fk=@usuario_fk,corpo=@corpo,dataHora=@dataHora,titulo=@titulo," +
                    "etiqueta=@etiqueta,rascunho=@rascunho WHERE postagem.id=@id SELECT scope_identity();";

                comando.Parameters.AddWithValue("@usuario_fk", usuario.id);
                comando.Parameters.AddWithValue("@corpo", postagem.corpo);
                comando.Parameters.AddWithValue("@titulo", postagem.titulo);
                comando.Parameters.AddWithValue("@etiqueta", postagem.etiqueta);
                comando.Parameters.AddWithValue("@dataHora", dataHora);
                comando.Parameters.AddWithValue("@rascunho", post);
                comando.Parameters.AddWithValue("@id", postagemId);

                conn = ConexaoBanco.Conectar();
                comando.Connection = conn;
                Int32 idPostagem = Convert.ToInt32(comando.ExecuteScalar());

                ImagemBO imagemBO = new ImagemBO();
                imagemBO.Gravar(lista, arq, idPostagem);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Delete(int id)
        {
            Postagem rascunho = new Postagem();
            Imagem img = new Imagem();
            ImagemBO imgBO = new ImagemBO();

            SqlConnection conn = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;

            comando.CommandText = "Delete imagem from imagem left join postagem on postagem.id = imagem.postagem_fk" +
                " where imagem.postagem_fk = @id ";
            comando.Parameters.AddWithValue("@id", id);

            ConexaoBanco.CRUD(comando);

            comando.CommandText = "DELETE postagem FROM postagem WHERE id=@id;";
            comando.Parameters.AddWithValue("@postagemId", id);

            ConexaoBanco.CRUD(comando);


        }

        public Postagem ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT postagem.* from postagem where id=@rascunhoId ";

            comando.Parameters.AddWithValue("@rascunhoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            Postagem postagem = new Postagem();

            if (dr.HasRows)
            {

                dr.Read();
                postagem.corpo = (string)dr["corpo"];
                postagem.titulo = (string)dr["titulo"];
                postagem.etiqueta = (string)dr["etiqueta"];
                postagem.autor.id = (int)dr["usuario_fk"];
                postagem.rascunho = (int)dr["rascunho"];
                postagem.id = (int)dr["id"];

            }
            else
            {

                postagem = null;
            }

            return postagem;

        }

        public List<Postagem> ListarPostagens() // retorna uma lista de postagens
        {
            List<Postagem> postagens = new List<Postagem>();

            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select [ExtranetFenix].[dbo].[postagem].*,[ExtranetFenix].[dbo].[usuario].nome FROM [ExtranetFenix].[dbo].[postagem] LEFT JOIN  [ExtranetFenix].[dbo].[usuario] ON " +
                    "[ExtranetFenix].[dbo].[postagem].usuario_fk = [ExtranetFenix].[dbo].[usuario].id WHERE [ExtranetFenix].[dbo].[postagem].rascunho = 0 ORDER BY postagem.dataHora DESC;";
                
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                          
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Postagem postagem = new Postagem();

                        postagem.corpo = Convert.ToString(dr["corpo"]);
                        postagem.titulo = Convert.ToString(dr["titulo"]);
                        postagem.etiqueta = Convert.ToString(dr["etiqueta"]);
                        postagem.autor.id = Convert.ToInt32(dr["usuario_fk"]);
                        postagem.id = Convert.ToInt32( dr["id"]);

                        ImagemBO imagemBO = new ImagemBO();


                        postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);


                        postagens.Add(postagem);
                    }

                }
               



            } catch (Exception ex) {

                
                postagens = null;

                throw ex;
            }

            

            return postagens;

        }
        
        public List<Postagem> ListarPostagensPrincipal() //retorna uma lista de postagens que vai direto para a tela principal da intrenet
        {
            List<Postagem> postagens = new List<Postagem>();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select [ExtranetFenix].[dbo].[postagem].*,[ExtranetFenix].[dbo].[usuario].nome FROM [ExtranetFenix].[dbo].[postagem] LEFT JOIN  [ExtranetFenix].[dbo].[usuario] ON " +
                    "[ExtranetFenix].[dbo].[postagem].usuario_fk = [ExtranetFenix].[dbo].[usuario].id WHERE [ExtranetFenix].[dbo].[postagem].rascunho = 0 ORDER BY postagem.dataHora DESC;";

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Postagem postagem = new Postagem();

                        postagem.corpo = Convert.ToString(dr["corpo"]);
                        postagem.titulo = Convert.ToString(dr["titulo"]);
                        postagem.etiqueta = Convert.ToString(dr["etiqueta"]);
                        postagem.autor.id = Convert.ToInt32(dr["usuario_fk"]);
                        postagem.id = Convert.ToInt32(dr["id"]);

                        ImagemBO imagemBO = new ImagemBO();


                        postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);


                        postagens.Add(postagem);
                    }

                }
                
            }
            catch (Exception ex)
            {


                postagens = null;

                throw ex;
            }

            return postagens;

        }

        
        
        public List<Postagem> ListarRascunhos() // retorna uma lista de postagens marcada como rascunho
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT postagem.*,usuario.nome FROM postagem LEFT JOIN usuario ON " +
                "postagem.usuario_fk=usuario.id  WHERE postagem.rascunho = 1 ORDER BY postagem.dataHora DESC;";

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            List<Postagem> postagens = new List<Postagem>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Postagem postagem = new Postagem();

                    postagem.corpo = (string)dr["corpo"];
                    postagem.titulo = (string)dr["titulo"];
                    postagem.etiqueta = (string)dr["etiqueta"];
                    postagem.autor.id = (int)dr["usuario_fk"];
                    postagem.id = (int)dr["id"];


                    ImagemBO imagemBO = new ImagemBO();
                    postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);

                    postagens.Add(postagem);
                }

            }
            else
            {
                postagens = null;
            }

            return postagens;

        }

        public List<Postagem> ListarPorTituloPostagens(string titulo) //retorna uma lista de postagens filtrada por titulo
        {
            List<Postagem> postagens = new List<Postagem>();

            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select * from " +

                   " (SELECT[ExtranetFenix].[dbo].[postagem].*, [ExtranetFenix].[dbo].[usuario].nome FROM[ExtranetFenix].[dbo].[postagem]" +
                    " left join[ExtranetFenix].[dbo].[usuario] ON [ExtranetFenix].[dbo].[postagem].usuario_fk = [ExtranetFenix].[dbo].[usuario].id)tb" +

                 " where tb.rascunho = 0 and tb.titulo like @titulo ORDER BY tb.titulo ASC";

                comando.Parameters.AddWithValue("@titulo", "%" + titulo + "%");

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Postagem postagem = new Postagem();

                        postagem.titulo = Convert.ToString( dr["titulo"]);
                        postagem.etiqueta = Convert.ToString( dr["etiqueta"]);
                        postagem.corpo = Convert.ToString( dr["corpo"]);
                        postagem.autor.id = Convert.ToInt32( dr["usuario_fk"]);
                        postagem.id = Convert.ToInt32( dr["id"]);

                        ImagemBO imagemBO = new ImagemBO();

                        postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);
                        
                        postagens.Add(postagem);
                    }

                }

            } catch (Exception ex) {

                postagens = null;
                throw ex;

            }

           
           

            return postagens;
        }

        public List<Postagem> ListarPorTituloRascunhos(string titulo) // retorna uma lista de rascunhos filtrad apor titulo
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT postagem.*,usuario.nome FROM postagem LEFT JOIN usuario " +
                "ON postagem.usuario_fk = usuario.id " +
                "WHERE postagem.rascunho = 1 AND postagem.titulo LIKE @titulo " +
                "ORDER BY postagem.titulo ASC, postagem.dataHora DESC;";

            comando.Parameters.AddWithValue("@titulo", "%" + titulo + "%");

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            List<Postagem> postagens = new List<Postagem>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Postagem postagem = new Postagem();

                    postagem.titulo = (string)dr["titulo"];
                    postagem.etiqueta = (string)dr["etiqueta"];
                    postagem.corpo = (string)dr["corpo"];
                    postagem.id = (int)dr["id"];

                    ImagemBO imagemBO = new ImagemBO();

                    postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);

                    postagens.Add(postagem);
                }

            }
            else
            {
                postagens = null;
            }

            return postagens;
        }

        public List<Postagem> ListarPorDataPostagens(string dataInicial, string dataFim) //retorna uma lista de postagens iltrada por data
        {
            List<Postagem> postagens = new List<Postagem>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select postagem.* from postagem WHERE " +
                "(postagem.dataHora >= @dataInicial OR postagem.dataHora <= @dataFim) AND postagem.rascunho = 1 ORDER BY postagem.dataHora DESC ";

            comando.Parameters.AddWithValue("@dataInicial", dataInicial);
            comando.Parameters.AddWithValue("@dataFim", dataFim);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Postagem postagem = new Postagem();

                    postagem.titulo = (string)dr["titulo"];
                    postagem.etiqueta = (string)dr["etiqueta"];
                    postagem.corpo = (string)dr["corpo"];
                    postagem.autor.id = (int)dr["usuario_fk"];
                    postagem.id = (int)dr["id"];

                    ImagemBO imagemBO = new ImagemBO();

                    postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);
                    
                    postagens.Add(postagem);
                }

            }
            else
            {
                postagens = null;
            }

            return postagens;

        }

        public List<Postagem> ListarPorDataRascunhos(string dataInicial, string dataFim) // retorna uma lista de rascunho filtradas por data
        {
            List<Postagem> postagens = new List<Postagem>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select postagem.*from postagem where " +
                "(postagem.dataHora >= @dataInicial OR postagem.dataHora <= @dataFim) AND postagem.rascunho = 0 ORDER BY postagem.dataHora DESC ";

            comando.Parameters.AddWithValue("@dataInicial", dataInicial);
            comando.Parameters.AddWithValue("@dataFim", dataFim);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Postagem postagem = new Postagem();

                    postagem.titulo = (string)dr["titulo"];
                    postagem.etiqueta = (string)dr["etiqueta"];
                    postagem.corpo = (string)dr["corpo"];
                    postagem.autor.id = (int)dr["usuario_fk"];
                    postagem.id = (int)dr["id"];

                    postagens.Add(postagem);
                }

            }
            else
            {
                postagens = null;
            }

            return postagens;

        }

        public void PublicarRascunho(int id) // ativa uma postagem que esta em modo rascunho para ser publicada
        {
            try
            {
                Postagem postagem = new Postagem();
                postagem.rascunho = 0;
                SqlConnection conn = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE  postagem set rascunho=@rascunho WHERE postagem.id = @id ";

                comando.Parameters.AddWithValue("@rascunho", postagem.rascunho);
                comando.Parameters.AddWithValue("@id", id);

                ConexaoBanco.CRUD(comando);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<Postagem> ListarPorTagPostagens(string tag) // retorna uma lista de postagens por tag
        {
            List<Postagem> postagens = new List<Postagem>();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select * from " +

                   " (SELECT[ExtranetFenix].[dbo].[postagem].*, [ExtranetFenix].[dbo].[usuario].nome FROM[ExtranetFenix].[dbo].[postagem]" +
                   " left join[ExtranetFenix].[dbo].[usuario] ON [ExtranetFenix].[dbo].[postagem].usuario_fk = [ExtranetFenix].[dbo].[usuario].id)tb" +
                   " WHERE tb.rascunho = 0 AND tb.titulo like @etiqueta ORDER BY tb.etiqueta ASC ";

                comando.Parameters.AddWithValue("@etiqueta", "%" + tag + "%");

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Postagem postagem = new Postagem();

                        postagem.titulo = Convert.ToString(dr["titulo"]);
                        postagem.etiqueta = Convert.ToString(dr["etiqueta"]);
                        postagem.corpo = Convert.ToString(dr["corpo"]);
                        postagem.autor.id = Convert.ToInt32(dr["usuario_fk"]);
                        postagem.id = Convert.ToInt32(dr["id"]);

                        ImagemBO imagemBO = new ImagemBO();
                        
                        postagem.imagensPostagens = imagemBO.ListarImagens(postagem.id);
                        
                        postagens.Add(postagem);
                    }

                }

            }
            catch (Exception ex)
            {

                postagens = null;
                throw ex;

            }
            
            return postagens;
        }


    }
}
