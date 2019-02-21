using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.BO;


namespace ProjetoIntranet.Models.DAO
{
    public class CurriculoDAO
    {
        public void Insert(Curriculo curriculo , DateTime dataEnvio) //insere um curriculo na tabela curriculo
        {

            try 
            {
                

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO[ExtranetFenix].[dbo].[curriculo] ([nome],[cpf],[dataNascimento],[uf],[cep],[cidade],[bairro],[rua],[numero],[descricao],[telefoneFixo]" +
                                       ",[telefoneCelular],[email],[siteBlog],[skype],[remuneracao],[genero],[areaPretendida_fk],[estadoCivil_fk],[anexo_fk],[dataEnvio]) VALUES" +
                                        "(@nome,@cpf,@dataNascimento,@uf,@cep,@cidade,@bairro,@rua,@numero,@descricao,@telefoneFixo,@telefoneCelular,@email,@siteBlog,@skype,@remuneracao,@genero,@areaPretendida_fk,@estadoCivil_fk,@anexo_fk,@dataEnvio)";

                comando.Parameters.AddWithValue("@nome", curriculo.nome);
                comando.Parameters.AddWithValue("@cpf", curriculo.cpf);
                comando.Parameters.AddWithValue("@dataNascimento", curriculo.dataNascimento);
                comando.Parameters.AddWithValue("@uf", curriculo.uf);
                comando.Parameters.AddWithValue("@cep", curriculo.cep);
                comando.Parameters.AddWithValue("@cidade", curriculo.cidade);
                comando.Parameters.AddWithValue("@bairro", curriculo.bairro);
                comando.Parameters.AddWithValue("@rua", curriculo.rua);
                comando.Parameters.AddWithValue("@numero", curriculo.numero);
                comando.Parameters.AddWithValue("@descricao", curriculo.descricao);
                comando.Parameters.AddWithValue("@telefoneFixo", curriculo.telefoneFixo);
                comando.Parameters.AddWithValue("@telefoneCelular", curriculo.telefoneCelular);
                comando.Parameters.AddWithValue("@email", curriculo.email);
                comando.Parameters.AddWithValue("@siteBlog", curriculo.siteBlog);
                comando.Parameters.AddWithValue("@skype", curriculo.skype);
                comando.Parameters.AddWithValue("@remuneracao", curriculo.remuneracao);
                comando.Parameters.AddWithValue("@genero", curriculo.genero);
                comando.Parameters.AddWithValue("@areaPretendida_fk", curriculo.area.id);
                comando.Parameters.AddWithValue("@estadoCivil_fk", curriculo.estado.id);
                
                comando.Parameters.AddWithValue("@anexo_fk", curriculo.anexo.id);
                comando.Parameters.AddWithValue("@dataEnvio", dataEnvio);

                ConexaoBanco.CRUD(comando);
            }
            catch 
            {

                throw ;
            }

        }


        public void InsertCvAnexo(Curriculo curriculo, Anexo anexo, DateTime dataEnvio) // insere um anexo que esta vinculado com um curriculo
        {
            try
            {
                int idAnexo = 0;
                AnexoBO b = new AnexoBO();
              idAnexo = b.Gravar(anexo);

                SqlConnection conn = new SqlConnection();

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                //comando.CommandText = "select anexo.id from anexo; Select scope_identity()";
                conn = ConexaoBanco.Conectar();
                comando.Connection = conn;
                //int id = (Int32)comando.ExecuteScalar();

                comando.CommandText = "INSERT INTO curriculo (nome,cpf,dataNascimento,uf,cep,cidade,bairro,rua,numero," +
                        "descricao,telefoneFixo,telefoneCelular,email,siteBlog,skype,remuneracao,genero,estadoCivil_fk, " +
                        "areaPretendida_fk,anexo_fk, dataEnvio) VALUES (@nome,@cpf,@dataNascimento,@uf,@cep,@cidade,@bairro,@rua,@numero,@descricao,@telefoneFixo," +
                        "@telefoneCelular,@email,@siteBlog,@skype,@remuneracao,@genero,@estadoCivil_fk,@areaPretendida_fk,@anexo_fk,@dataEnvio)";

                comando.Parameters.AddWithValue("@nome", curriculo.nome);
                comando.Parameters.AddWithValue("@cpf", curriculo.cpf);
                comando.Parameters.AddWithValue("@dataNascimento", curriculo.dataNascimento);
                comando.Parameters.AddWithValue("@uf", curriculo.uf);
                comando.Parameters.AddWithValue("@cep", curriculo.cep);
                comando.Parameters.AddWithValue("@cidade", curriculo.cidade);
                comando.Parameters.AddWithValue("@bairro", curriculo.bairro);
                comando.Parameters.AddWithValue("@rua", curriculo.rua);
                comando.Parameters.AddWithValue("@numero", curriculo.numero);
                comando.Parameters.AddWithValue("@descricao", curriculo.descricao);
                comando.Parameters.AddWithValue("@telefoneFixo", curriculo.telefoneFixo);
                comando.Parameters.AddWithValue("@telefoneCelular", curriculo.telefoneCelular);
                comando.Parameters.AddWithValue("@email", curriculo.email);
                comando.Parameters.AddWithValue("@siteBlog", curriculo.siteBlog);
                comando.Parameters.AddWithValue("@skype", curriculo.skype);
                comando.Parameters.AddWithValue("@remuneracao", curriculo.remuneracao); //não pegou a remner~ção verrificar a view
                comando.Parameters.AddWithValue("@genero", curriculo.genero);
                comando.Parameters.AddWithValue("@estadoCivil_fk", curriculo.estado.id);
                comando.Parameters.AddWithValue("@areaPretendida_fk", curriculo.area.id);
                comando.Parameters.AddWithValue("@anexo_fk", idAnexo);
                comando.Parameters.AddWithValue("@dataEnvio", dataEnvio);

                ConexaoBanco.CRUD(comando);
            }
            catch 
            {

                throw ;
            }



        }


        public void Update(Curriculo curriculo)
        {

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE curriculo SET(nome=@nome, cpf=@cpf, dataNascimento=@dataNascimento, uf=@uf, cep=@cep, " +
                    "cidade=@cidade,bairro=@bairro,rua=@rua,numero=@numero,descricao=@descricao,telefoneFixo=@telefoneFixo," +
                    "telefoneCelular=@telefoneCelular,email=@email,siteBlog=@siteBlog,skype=@skype,remuneracao=@remuneracao," +
                    "genero=@genero,estadoCivil_fk=@estado,areaPretendida_fk=@area,anexo_fk=@anexo WHERE id=@curriculoId ";

            comando.Parameters.AddWithValue("@nome", curriculo.nome);
            comando.Parameters.AddWithValue("@cpf", curriculo.cpf);
            comando.Parameters.AddWithValue("@dataNascimento", curriculo.dataNascimento);
            comando.Parameters.AddWithValue("@uf", curriculo.uf);
            comando.Parameters.AddWithValue("@cep", curriculo.cep);
            comando.Parameters.AddWithValue("@cidade", curriculo.cidade);
            comando.Parameters.AddWithValue("@bairro", curriculo.bairro);
            comando.Parameters.AddWithValue("@rua", curriculo.rua);
            comando.Parameters.AddWithValue("@numero", curriculo.numero);
            comando.Parameters.AddWithValue("@descricao", curriculo.descricao);
            comando.Parameters.AddWithValue("@telefoneFixo", curriculo.telefoneFixo);
            comando.Parameters.AddWithValue("@telefoneCelular", curriculo.telefoneCelular);
            comando.Parameters.AddWithValue("@email", curriculo.email);
            comando.Parameters.AddWithValue("@siteBlog", curriculo.siteBlog);
            comando.Parameters.AddWithValue("@skype", curriculo.skype);
            comando.Parameters.AddWithValue("@remuneracao", curriculo.remuneracao);
            comando.Parameters.AddWithValue("@genero", curriculo.genero);
            comando.Parameters.AddWithValue("@estadoCivil_fk", curriculo.estado.id);
            comando.Parameters.AddWithValue("@areaPretendida_fk", curriculo.area.id);
            comando.Parameters.AddWithValue("@anexo_fk", curriculo.anexo.id);

            ConexaoBanco.CRUD(comando);

        }

        public void Delete(int id)
        {

            try {


                Curriculo curriculo = new Curriculo();
                Anexo anexo = new Anexo();
                AnexoBO anx = new AnexoBO();

                CurriculoBO curriculoBO = new CurriculoBO();
                curriculo = curriculoBO.ReadById(id);

                SqlConnection conn = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE  FROM curriculo WHERE id=@curriculoId ";
                comando.Parameters.AddWithValue("@curriculoId", id);

                ConexaoBanco.CRUD(comando);

                conn = ConexaoBanco.Conectar();
                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                if (curriculo.anexo.id != 0)
                {
                    int anexoId = curriculo.anexo.id;
                    comando.CommandText = "DELETE  from anexo where id=@id ";
                    comando.Parameters.AddWithValue("@id", anexoId);

                    ConexaoBanco.CRUD(comando);
                }





            }
            catch {

                throw ;
            }

            

        }

        public Curriculo ReadById(int id) //retorna um curriculo por ID
        {

            Curriculo curriculo = new Curriculo();

            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT curriculo.* from curriculo where id=@curriculoId ";

                comando.Parameters.AddWithValue("@curriculoId", id);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

               

                if (dr.HasRows) //verifica se o dr tem alguma coisa
                {
                    dr.Read();

                    curriculo.nome = (string)dr["nome"];
                    curriculo.cpf = (string)dr["cpf"];
                    curriculo.uf = (string)dr["uf"];
                    curriculo.cep = (string)dr["cep"];
                    curriculo.cidade = (string)dr["cidade"];
                    curriculo.bairro = (string)dr["bairro"];
                    curriculo.rua = (string)dr["rua"];
                    curriculo.numero = Convert.ToString(dr["numero"]);
                    curriculo.descricao = (string)dr["descricao"];
                    curriculo.telefoneFixo = (string)dr["telefoneFixo"];
                    curriculo.telefoneCelular = (string)dr["telefoneCelular"];
                    curriculo.email = (string)dr["email"];
                    curriculo.siteBlog = (string)dr["siteBlog"];
                    curriculo.skype = (string)dr["skype"];
                    curriculo.remuneracao = (double)dr["remuneracao"];
                    curriculo.genero = (string)dr["genero"];
                    curriculo.estado.id = Convert.ToInt32(dr["estadoCivil_fk"]);
                    curriculo.area.id = Convert.ToInt32(dr["areaPretendida_fk"]);
                    curriculo.anexo.id = Convert.ToInt32(dr["anexo_fk"]);
                    curriculo.id = Convert.ToInt32(dr["id"]);

                }
               


            } catch  {

                curriculo = null;
                throw ;

            }

           

            return curriculo;

        }

       


        public List<Curriculo> ListarCurriculos() // lista todos os curriculos
        {

            Curriculo curriculo = new Curriculo();
            List<Curriculo> curriculos = new List<Curriculo>();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * FROM curriculo LEFT JOIN estadoCivil on estadoCivil.id=curriculo.estadoCivil_fk " +
                        "LEFT JOIN areaPretendida on areaPretendida.id=curriculo.areaPretendida_fk " +
                        "LEFT JOIN anexo on curriculo.anexo_fk=anexo.id where 1=1 order by [ExtranetFenix].[dbo].curriculo.nome asc";

                // a query seguinte vai lista por quantidade de tuplas por pagina...ain não está pronta...
                //SELECT * ,ROW_NUMBER()OVER(ORDER BY curriculo.nome) as rowNumber FROM ExtranetFenix.dbo.curriculo LEFT JOIN ExtranetFenix.dbo.estadoCivil on estadoCivil.id = curriculo.estadoCivil_fk
                //        LEFT JOIN ExtranetFenix.dbo.areaPretendida on areaPretendida.id = curriculo.areaPretendida_fk
                //        LEFT JOIN ExtranetFenix.dbo.anexo on curriculo.anexo_fk = anexo.id where 1 = 1



                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                
              

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        

                        curriculo.nome = Convert.ToString(dr["nome"]);
                        curriculo.cpf = Convert.ToString(dr["cpf"]);
                        curriculo.uf = Convert.ToString(dr["uf"]);
                        curriculo.cep = Convert.ToString(dr["cep"]);
                        curriculo.cidade = Convert.ToString(dr["cidade"]);
                        curriculo.bairro = Convert.ToString(dr["bairro"]);
                        curriculo.rua = Convert.ToString(dr["rua"]);
                        curriculo.numero = Convert.ToString(dr["numero"]);
                        curriculo.descricao = Convert.ToString(dr["descricao"]);
                        curriculo.telefoneFixo = Convert.ToString(dr["telefoneFixo"]);
                        curriculo.telefoneCelular = Convert.ToString(dr["telefoneCelular"]);
                        curriculo.email = Convert.ToString(dr["email"]);
                        curriculo.siteBlog = Convert.ToString(dr["siteBlog"]);
                        curriculo.skype = Convert.ToString(dr["skype"]);
                        curriculo.remuneracao = Convert.ToDouble(dr["remuneracao"]);
                        curriculo.genero = Convert.ToString(dr["genero"]);
                        curriculo.estado.id = Convert.ToInt32(dr["estadoCivil_fk"]);
                       
                        curriculo.area.id = Convert.ToInt32(dr["areaPretendida_fk"]);
                        curriculo.area.cargo = Convert.ToString(dr["cargo"]);
                        curriculo.estado.estado = Convert.ToString(dr["estado"]);
                        curriculo.anexo.id = Convert.ToInt32(dr["anexo_fk"]);
                        curriculo.id = Convert.ToInt32(dr["id"]);

                        curriculos.Add(curriculo);

                        curriculo = new Curriculo();

                    }

                }
                



            } catch  {
                curriculos = null;
                throw ;
            }

            

            return curriculos;

        }

        public List<Curriculo> ListarPorArea(string area) // lista curriculos  por área pretendida 
        {

           
            List<Curriculo> curriculos = new List<Curriculo>();

            try {


                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT curriculo.*,estadoCivil.estado,areaPretendida.cargo,anexo.arquivo FROM ExtranetFenix.dbo.curriculo  LEFT JOIN ExtranetFenix.dbo.estadoCivil on estadoCivil.id=curriculo.estadoCivil_fk LEFT JOIN  ExtranetFenix.dbo.areaPretendida on areaPretendida.id=curriculo.areaPretendida_fk LEFT JOIN  ExtranetFenix.dbo.anexo on curriculo.anexo_fk=anexo.id  WHERE cargo like @cargo order by [ExtranetFenix].[dbo].curriculo.nome asc";
                                            
                
                comando.Parameters.AddWithValue("@cargo", "%" + area + "%" );

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Curriculo curriculo = new Curriculo();

                        curriculo.nome = (string)dr["nome"];
                        curriculo.cpf = (string)dr["cpf"];
                        curriculo.uf = (string)dr["uf"];
                        curriculo.cep = (string)dr["cep"];
                        curriculo.cidade = (string)dr["cidade"];
                        curriculo.bairro = (string)dr["bairro"];
                        curriculo.rua = (string)dr["rua"];
                        curriculo.numero = Convert.ToString(dr["numero"]);
                        curriculo.descricao = (string)dr["descricao"];
                        curriculo.telefoneFixo = (string)dr["telefoneFixo"];
                        curriculo.telefoneCelular = (string)dr["telefoneCelular"];
                        curriculo.email = (string)dr["email"];
                        curriculo.siteBlog = (string)dr["siteBlog"];
                        curriculo.skype = (string)dr["skype"];
                        curriculo.remuneracao = (double)dr["remuneracao"];
                        curriculo.genero = (string)dr["genero"];
                        curriculo.estado.id = Convert.ToInt32(dr["estadoCivil_fk"]);
                        curriculo.area.id = Convert.ToInt32(dr["areaPretendida_fk"]);
                        curriculo.id = Convert.ToInt32(dr["id"]);

                        curriculos.Add(curriculo);
                    }

                }
               

            }
            catch{

                curriculos = null;
                throw ;
            }

            

            return curriculos;
        }


        public List<Curriculo> ListarPorData(string dataInicial, string dataFim) // lista curriculos por data
        {

            List<Curriculo> curriculos = new List<Curriculo>();

            try {

               

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select curriculo.* from curriculo where" +
                "(curriculo.dataEnvio >= @dataInicial OR curriculo.dataEnvio <= @dataFim) order by curriculo.dataEnvio desc ";

                comando.Parameters.AddWithValue("@dataInicial", dataInicial);
                comando.Parameters.AddWithValue("@dataFim", dataFim);

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Curriculo curriculo = new Curriculo();

                        curriculo.nome = (string)dr["nome"];
                        curriculo.cpf = (string)dr["cpf"];
                        curriculo.uf = (string)dr["uf"];
                        curriculo.cep = (string)dr["cep"];
                        curriculo.cidade = (string)dr["cidade"];
                        curriculo.bairro = (string)dr["bairro"];
                        curriculo.rua = (string)dr["rua"];
                        curriculo.numero = Convert.ToString(dr["numero"]);
                        curriculo.descricao = (string)dr["descricao"];
                        curriculo.telefoneFixo = (string)dr["telefoneFixo"];
                        curriculo.telefoneCelular = (string)dr["telefoneCelular"];
                        curriculo.email = (string)dr["email"];
                        curriculo.siteBlog = (string)dr["siteBlog"];
                        curriculo.skype = (string)dr["skype"];
                        curriculo.remuneracao = (double)dr["remuneracao"];
                        curriculo.genero = (string)dr["genero"];
                        curriculo.estado.id = Convert.ToInt32(dr["estadoCivil_fk"]);
                        curriculo.area.id = Convert.ToInt32(dr["areaPretendida_fk"]);
                        curriculo.id = Convert.ToInt32(dr["id"]);

                        curriculos.Add(curriculo);
                    }

                }
               


            } catch
            {

                curriculos = null;
                throw ;

            }
           

            return curriculos;

        }

    }


    


}
