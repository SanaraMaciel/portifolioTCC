using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntranet.Models.Entity
{
    public class CadastroMenu
    {
        public int id { get; set; }

        [Display(Name = "Url", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
      ErrorMessageResourceName = "URLObrigatória")]
        [StringLength(50, ErrorMessageResourceType = typeof(Recursos.Resources),
              ErrorMessageResourceName = "Url")]
        public string url { get; set; }

        [Display(Name = "Nome", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
       ErrorMessageResourceName = "NomeObrigatório")]
        [StringLength(50, ErrorMessageResourceType = typeof(Recursos.Resources),
               ErrorMessageResourceName = "Nome")]
        public string nome { get; set; }

        [Display(Name = "ordem", ResourceType = typeof(Recursos.Resources))]      
        public int ordem { get; set; }

        [Display(Name = "codigoPai", ResourceType = typeof(Recursos.Resources))]
        public int codigoPai { get; set; }

       

    }
}
