using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;


namespace ProjetoIntranet.Models.BO
{
    public class ImagemBO
    {
        public void Gravar(List<Imagem> lista, Imagem arq, int id)
        {

            ImagemDAO imagemDAO = new ImagemDAO();
            //insere
            imagemDAO.Insert(lista, arq, id);
        }

        public void Alterar(List<Imagem> lista, Imagem arq, int id)
        {
            ImagemDAO imagemDAO = new ImagemDAO();
            imagemDAO.Alterar(lista, arq, id);
        }

        public void Delete(int id)
        {
            ImagemDAO imagemDAO = new ImagemDAO();

            imagemDAO.Delete(id);


        }


        public List<Imagem> ListarImagens(int postagemId)
        {
            ImagemDAO imagemDAO = new ImagemDAO();

            List<Imagem> imagensPostagens = new List<Imagem>();

            imagensPostagens = imagemDAO.ListarImagens(postagemId);

            return imagensPostagens;
        }


    }
}
