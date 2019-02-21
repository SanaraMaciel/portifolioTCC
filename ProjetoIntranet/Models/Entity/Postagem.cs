using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntranet.Models.Entity
{
    public class Postagem
    {

        public Postagem()
        {
            autor = new Usuario();
        }

        public int id { get; set; }

        [Display(Name = "Corpo", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "CorpoObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
       ErrorMessageResourceName = "Corpo")]
        public string corpo { get; set; }

        [Display(Name = "Titulo", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "TituloObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
       ErrorMessageResourceName = "Titulo")]
        public string titulo { get; set; }

        [Display(Name = "Etiqueta", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "EtiquetaObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
       ErrorMessageResourceName = "Etiqueta")]
        public string etiqueta { get; set; }

        public Usuario autor { get; set; }

        public int rascunho { get; set; }

        public IEnumerable<Imagem> imagens { get; set; }

        public int vencimento { get; set; }

        public List<Imagem> imagensPostagens { get; set; }

    }
}
