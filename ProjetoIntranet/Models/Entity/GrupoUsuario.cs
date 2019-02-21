using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntranet.Models.Entity
{
    public class GrupoUsuario
    {
        public int id { get; set; }

        [Display(Name = "Nome", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
     ErrorMessageResourceName = "NomeObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
             ErrorMessageResourceName = "Nome")]
        public string nome { get; set; }

        [Display(Name = "Descricao", ResourceType = typeof(Recursos.Resources))]
        public string descricao { get; set; }

        [Display(Name = "grafico", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
     ErrorMessageResourceName = "graficoObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
             ErrorMessageResourceName = "grafico")]
        public string graficoUrl { get; set; }

        [Display(Name = "Privilegios", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
     ErrorMessageResourceName = "PrivilegioObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
             ErrorMessageResourceName = "Privilegios")]
        public String privilegios { get; set; }
    }
}
