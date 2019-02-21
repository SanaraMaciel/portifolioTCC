using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.Models.Entity
{
    public class ItemDeMenu
    {

        public ItemDeMenu()
        {

            filhosList = new List<ItemDeMenu>();

        }

        public int id { get; set; }

        [Display(Name = "Nome", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "NomeObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "Nome")]
        public String nome { get; set; }

        [Display(Name = "Url", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "URLObrigatória")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "Url")]
        public String url { get; set; }

        [Display(Name = "pai", ResourceType = typeof(Recursos.Resources))]
        public ItemDeMenu pai { get; set; }

        [Display(Name = "ordem", ResourceType = typeof(Recursos.Resources))]
        public int ordem { get; set; }

        public List<ItemDeMenu> filhosList { get; set; }

    }
}