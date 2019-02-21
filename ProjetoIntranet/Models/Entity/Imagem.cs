using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoIntranet.Models.Entity;
using System.Web;

namespace ProjetoIntranet.Models.Entity
{
    public class Imagem
    {
        private Postagem Postagem = new Postagem();

        public int id { get; set; }
        public string nome { get; set; }
        public byte[] imagem { get; set; }
        public string tipoArquivo { get; set; }

        public IEnumerable<HttpPostedFileBase> imagens { get; set; }
        public Postagem postagem
        {
            get
            {
                return Postagem;

            }
        }
             
    }
}
