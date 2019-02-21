using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.Models.Entity
{
    public class MensagemEmail
    {
        [Display(Name = "Nome", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "NomeObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "Nome")]
        public String Nome { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "EmailObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "Email")]

        public String Email { get; set; }

        [Display(Name = "Fone", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "FoneObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
        ErrorMessageResourceName = "Fone")]
        public String Fone { get; set; }

        [Display(Name = "Msg", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
ErrorMessageResourceName = "MsgObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
       ErrorMessageResourceName = "Msg")]
        public String Msg { get; set; }

        

    }
}