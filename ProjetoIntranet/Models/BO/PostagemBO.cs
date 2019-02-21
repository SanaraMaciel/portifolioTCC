using ProjetoIntranet.Models.DAO;
using ProjetoIntranet.Models.Entity;
using System;
using System.Collections.Generic;



namespace ProjetoIntranet.Models.BO
{
    public class PostagemBO
    {

        public int Gravar(Postagem postagem, DateTime dataHora, int post)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            //insere
            int id = postagemDAO.Insert(postagem, dataHora, post);
            return id;

        }

        public void Alterar(Postagem postagem, DateTime dataHora, List<Imagem> lista, Imagem arq, int id, int post)
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            //altera
            postagemDAO.Update(postagem, dataHora, lista, arq, id, post);

        }

        public void DeleteRascunho(int id)
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            postagemDAO.Delete(id);
        }

        public void DeletePostagem(int id)
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            postagemDAO.Delete(id);
        }

        public List<Postagem> ListarPostagens()
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            return postagemDAO.ListarPostagens();
        }

        public List<Postagem> ListarPostagensPrincipal()
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            return postagemDAO.ListarPostagensPrincipal();
        }
     
        public List<Postagem> ListarRascunhos()
        {
            PostagemDAO postagemDAO = new PostagemDAO();

            return postagemDAO.ListarRascunhos();
        }

        public List<Postagem> ListarPorTituloPostagens(string titulo)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ListarPorTituloPostagens(titulo);
        }

        public List<Postagem> ListarPorTituloRascunhos(string titulo)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ListarPorTituloRascunhos(titulo);
        }

        public List<Postagem> ListarPorDataPostagens(string dataInicial, string dataFim)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ListarPorDataPostagens(dataInicial, dataFim);

        }

        public List<Postagem> ListarPorDataRascunhos(string dataInicial, string dataFim)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ListarPorDataRascunhos(dataInicial, dataFim);

        }

        public Postagem ReadById(int id)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ReadById(id);
        }

        public void PublicarRascunho(int id)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            postagemDAO.PublicarRascunho(id);

        }
        
        public List<Postagem> ListarPorTagPostagens(string tag)
        {
            PostagemDAO postagemDAO = new PostagemDAO();
            return postagemDAO.ListarPorTagPostagens(tag);
        }

    }
}
