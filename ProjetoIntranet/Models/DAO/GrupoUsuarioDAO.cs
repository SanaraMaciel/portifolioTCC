using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class GrupoUsuarioDAO
    {

        
      

        public void Insert(GrupoUsuario perfil) // insere um grupo de usuario na tebela
        {

            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO grupoUsuario (nome, descricao, graficoUrl,privilegios) VALUES(@nome,@descricao,@graficoUrl,@privilegios)";

                comando.Parameters.AddWithValue("@nome", perfil.nome);
                comando.Parameters.AddWithValue("@descricao", perfil.descricao);
                comando.Parameters.AddWithValue("@graficoUrl", perfil.graficoUrl);
                comando.Parameters.AddWithValue("@privilegios", perfil.privilegios);

                ConexaoBanco.CRUD(comando);


            }
            catch{


                throw ;

            }

            

            
            

        }

        
        private void updatelistaUsuarioPerfil(SqlConnection con, GrupoUsuario perfil) {


            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE * FROM usuarioGrupo WHERE Usuario_fk=@usuario_fk";
                comando.Parameters.AddWithValue("@id", perfil.id);

                comando.ExecuteNonQuery();

                comando = new SqlCommand("INSERT INTO usuarioGrupo (usuario_fk,grupoUsuario_fk) VALUES (@usuario_fk,@grupoUsuario_fk)");

                comando.Parameters.AddWithValue("@usuario_fk", perfil.id);
                comando.Parameters.AddWithValue("@grupoUsuario_fk", perfil.id);
                comando.ExecuteNonQuery();

            } catch
            {
                throw ;
                    
            }
           

        }

        public void Update(GrupoUsuario perfil)
        {


            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE grupoUsuario SET nome=@nome,descricao=@descricao,graficoUrl=@graficoUrl,privilegios=@privilegios WHERE Id=@id ";


                comando.Parameters.AddWithValue("@nome", perfil.nome);
                comando.Parameters.AddWithValue("@descricao", perfil.descricao);
                comando.Parameters.AddWithValue("@graficoUrl", perfil.graficoUrl);
                comando.Parameters.AddWithValue("@privilegios", perfil.privilegios);
                comando.Parameters.AddWithValue("@id", perfil.id);
                // this.updatelistaUsuarioPerfil(comando, perfil);

                ConexaoBanco.CRUD(comando);


            }
            catch 
            {
                throw ;

            }

           
            
           

        }

        public void Delete(int id)
        {
           try
            {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM grupoUsuario WHERE id=@grupoId ";

            comando.Parameters.AddWithValue("@grupoId", id);
            ConexaoBanco.CRUD(comando);


            }
            catch 
            {
                throw ;

            }


            




        }

        public GrupoUsuario ReadById(int id)
        {

            GrupoUsuario grupo = new GrupoUsuario();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * grupoUsuario WHERE id=@id ";

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

               

                if (dr.HasRows) //verifica se o dr tem alguma coisa
                {

                    dr.Read();
                    grupo.nome = (string)dr["nome"];
                    grupo.descricao = (string)dr["descricao"];
                    grupo.graficoUrl = (string)dr["graficoUrl"];
                    grupo.id = (int)dr["id"];

                }
                else
                {

                    grupo = null;
                }


            }
            catch 
            {

                grupo = null;
                throw ;

            }

           

            return grupo;

        }

        public IList<GrupoUsuario> ListarPorNome(string nome) // lista grupos de usuario por nome
        {

            IList<GrupoUsuario> grupos = new List<GrupoUsuario>();

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM grupoUsuario WHERE descricao LIKE @nome";


                comando.Parameters.AddWithValue("@nome", "%" + nome + "%");

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);



                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        GrupoUsuario grupo = new GrupoUsuario();

                        grupo.nome = (string)dr["nome"];
                        grupo.descricao = (string)dr["descricao"];
                        grupo.graficoUrl = (string)dr["graficoUrl"];
                        grupo.id = (int)dr["id"];

                        grupos.Add(grupo);
                    }

                }
                else
                {
                    grupos = null;
                }



            }
            catch 
            {
                grupos = null;
                throw ;

            }

            

            return grupos;
        }

        public Boolean VerificaUsuarioDoGrupo(GrupoUsuario grupoUsuario) //procura usuario existene em um grupo
        {
            Boolean retorno = true;

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT usuario_fk FROM [ExtranetFenix].[dbo].[usuarioXGrupo] WHERE grupoUsuario_fk = @grupoUsuario_fk";

                comando.Parameters.AddWithValue("@grupoUsuario_fk", grupoUsuario.id);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                UsuarioDAO usuarioDao = new UsuarioDAO();
                GrupoUsuarioDAO grupoDao = new GrupoUsuarioDAO();

                if (dr.HasRows) //verifica se o dr tem alguma coisa
                {
                    retorno = true;
                }
                else
                {

                    retorno = false;

                }
               

            }
            catch 
            {
                retorno = true;
                throw ;

            }
                       
                        
            return retorno;

        }

   
        public String pegarPrivilegios(int idUsuario) // retona os provilegios de um usuario
        {
            String privilegios = "";


            try {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Select privilegios from [ExtranetFenix].[dbo].usuarioXGrupo join [ExtranetFenix].[dbo].usuario on usuario_fk = usuario.id join [ExtranetFenix].[dbo].grupoUsuario on grupoUsuario_fk = grupoUsuario.id where [ExtranetFenix].[dbo].usuarioXGrupo.usuario_fk = @id";

                comando.Parameters.AddWithValue("@id", idUsuario);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        var temp =  Convert.ToString(dr["privilegios"]);

                        privilegios = privilegios + String.Join(",", temp);
                    }
                   


                }

            }
            catch  {

                throw ;

            }


            return privilegios;
        }

        public IList<GrupoUsuario> ListarGrupos() // retorna uma lista de grupos de usuario
        {

            IList<GrupoUsuario> Lista = new List<GrupoUsuario>();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT GrupoUsuario.id,GrupoUsuario.nome , GrupoUsuario.descricao FROM GrupoUsuario order by nome asc";

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);


                if (dr.HasRows)
                {

                    GrupoUsuario gu = new GrupoUsuario();

                    while (dr.Read())
                    {

                        gu.id = Convert.ToInt32(dr["id"]);
                        gu.nome = (string)dr["nome"];
                        gu.descricao = (string)dr["descricao"];

                        Lista.Add(gu);

                        gu = new GrupoUsuario();

                    }



                }


            }
            catch 
            {

                Lista = null;
                throw ;

            }

            


            return Lista;
        }



       
        

    }
}
