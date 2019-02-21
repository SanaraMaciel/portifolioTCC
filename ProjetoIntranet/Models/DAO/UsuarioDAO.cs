using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class UsuarioDAO
    {
        public void Insert(Usuario usuario) // insere usuario na tbael ausuario
        {

            try
            {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO usuario(nome,cargo,usuarioLogin,senha,centroCusto,email,bU,setor,situacao) " +
                                      "values(@nome,@cargo,@usuarioLogin,@senha,@centroCusto,@email,@bU,@setor,@situacao)  ";

                comando.Parameters.AddWithValue("@nome", usuario.nome);
                comando.Parameters.AddWithValue("@cargo", usuario.cargo);
                comando.Parameters.AddWithValue("@usuarioLogin", usuario.usuarioLogin);
                comando.Parameters.AddWithValue("@senha", usuario.senha);
                comando.Parameters.AddWithValue("@centroCusto", usuario.centroCusto);
                comando.Parameters.AddWithValue("@email", usuario.email);
                comando.Parameters.AddWithValue("@bU", usuario.bU);
                comando.Parameters.AddWithValue("@setor", usuario.setor);
                comando.Parameters.AddWithValue("@situacao", usuario.situacao);



                ConexaoBanco.CRUD(comando);


            }
            catch (Exception e) {

                throw e;
            }

            
            
            

        }




        public void Update(Usuario usuario)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE usuario SET nome=@nome,cargo=@cargo,usuarioLogin=@usuarioLogin,senha=@senha," +
                "centroCusto=@centroCusto, email=@email, bU=@bU, setor=@setor,situacao=@situacao WHERE id=@id ";

            comando.Parameters.AddWithValue("@nome", usuario.nome);
            comando.Parameters.AddWithValue("@cargo", usuario.cargo);
            comando.Parameters.AddWithValue("@usuarioLogin", usuario.usuarioLogin);
            comando.Parameters.AddWithValue("@senha", usuario.senha);
            comando.Parameters.AddWithValue("@centroCusto", usuario.centroCusto);
            comando.Parameters.AddWithValue("@email", usuario.email);
            comando.Parameters.AddWithValue("@bU", usuario.bU);
            comando.Parameters.AddWithValue("@setor", usuario.setor);
            comando.Parameters.AddWithValue("@situacao", usuario.situacao);

            ConexaoBanco.CRUD(comando);

        }

        public void Desativar(int id) // responsavel por desativar um usuario
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "  UPDATE [ExtranetFenix].[dbo].[usuario] SET [situacao] = 0 WHERE [ExtranetFenix].[dbo].[usuario].id =@id ";

            comando.Parameters.AddWithValue("@id", id);
            ConexaoBanco.CRUD(comando);

        }


        public void Ativar(int id) // responsavel por ativar um usuairo
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "  UPDATE [ExtranetFenix].[dbo].[usuario] SET [situacao] = 1 WHERE [ExtranetFenix].[dbo].[usuario].id =@id ";

            comando.Parameters.AddWithValue("@id", id);
            ConexaoBanco.CRUD(comando);

        }

        public Usuario ReadById(int id)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM usuario WHERE id=@id ";

            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            Usuario usuario = new Usuario();
            GrupoUsuarioDAO gpUserDao = new GrupoUsuarioDAO();
            if (dr.HasRows) //verifica se o dr tem alguma coisa
            {
                //preenche o objeto usuario
                dr.Read();
                usuario.nome = (string)dr["nome"];
                usuario.cargo = (string)dr["cargo"];
                usuario.usuarioLogin = (string)dr["titulo"];
                usuario.senha = (string)dr["etiqueta"];
                usuario.centroCusto = (string)dr["publicada"];
                usuario.email = (string)dr["email"];
                usuario.bU = (string)dr["bU"];
                usuario.setor = (string)dr["setor"];
                usuario.situacao = (Boolean)dr["situacao"];

            }
            else
            {

                usuario = null;
            }

            return usuario;

        }

        public List<Usuario> ListarPorNome(string nome) // retorna um lista de usuairo com um filtro 
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM usuario WHERE nome LIKE @nome"; 


            comando.Parameters.AddWithValue("@nome", "%" + nome + "%"); 

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            List<Usuario> usuarios = new List<Usuario>();
            GrupoUsuarioDAO gpUserDao = new GrupoUsuarioDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.nome = (string)dr["nome"];
                    usuario.cargo = (string)dr["cargo"];
                    usuario.usuarioLogin = (string)dr["titulo"];
                    usuario.senha = (string)dr["etiqueta"];
                    usuario.centroCusto = (string)dr["publicada"];
                    usuario.email = (string)dr["email"];
                    usuario.bU = (string)dr["bU"];
                    usuario.setor = (string)dr["setor"];
                    usuario.situacao = (Boolean)dr["situacao"];

                    usuarios.Add(usuario);
                }

            }
            else
            {

                usuarios = null;
            }

            return usuarios;

        }


        public List<Usuario> ListarTodosUsuarios() // retorna toods usuario em uma lista
        {
            List<Usuario> lista = null;
            Usuario usr = null;

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM usuario order by nome asc";
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                if (dr.HasRows) {

                    lista = new List<Usuario>();

                   usr  = new Usuario();

                    while (dr.Read()) {

                        usr.id = (int)dr["id"];
                        usr.nome = (string)dr["nome"];
                        usr.cargo = (string)dr["cargo"];
                        usr.usuarioLogin = (string)dr["usuarioLogin"];
                        usr.senha = (string)dr["senha"];
                        usr.centroCusto = (string)dr["centroCusto"];
                        usr.email = (string)dr["email"];
                        usr.bU = (string)dr["bU"];
                        usr.setor = (string)dr["setor"];
                        usr.situacao = (Boolean)dr["situacao"];

                        lista.Add(usr);

                        usr = new Usuario();

                    }


                }
                else
                {

                    lista = null;

                }


            }
            catch (Exception e) {

                lista = null;

                throw e;

            }

          

            return lista;
        }


        public Usuario PesquisarUsuario(string usuarioLogin, string senha) //realiza uma pesquisa de usuario se esta cadastrado e com a scredenciais correta para libera acesso
        {
            Usuario usuario = new Usuario();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM [ExtranetFenix].[dbo].[usuario]  Where usuario.usuarioLogin like @usuarioLogin and usuario.senha = @senha and usuario.situacao=1"; //and ativo =1

            comando.Parameters.AddWithValue("@usuarioLogin",  usuarioLogin + "%");
            comando.Parameters.AddWithValue("@senha", senha);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                dr.Read();
                usuario.usuarioLogin = (string)dr["usuarioLogin"];
                usuario.senha = (string)dr["senha"];
                usuario.id = Convert.ToInt32(dr["id"]);
                usuario.nome = Convert.ToString(dr["nome"]);
            }
            else
            {
                usuario = null;
            }
            return usuario;
        }


    }
}
