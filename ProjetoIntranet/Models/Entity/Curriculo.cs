using System;

using System.ComponentModel.DataAnnotations;

namespace ProjetoIntranet.Models.Entity
{
    public class Curriculo
    {
        private AreaPretendida Area = new AreaPretendida();
        private EstadoCivil Estado = new EstadoCivil();
        private Anexo Anexo = new Anexo();

        public int id { get; set; }

        [Display(Name = "Nome", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
      ErrorMessageResourceName = "NomeObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
              ErrorMessageResourceName = "Nome")]
        public string nome { get; set; }


        [Display(Name = "CPF", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
      ErrorMessageResourceName = "CPFObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
              ErrorMessageResourceName = "CPF")]
        public string cpf { get; set; }


        [Display(Name = "Data", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "DataObrigatória")]
        
        public DateTime dataNascimento { get; set; }

        [Display(Name = "uf", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "UFObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "uf")]
        public string uf { get; set; }

        [Display(Name = "Cep", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "CepObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Cep")]
        public string cep { get; set; }

        [Display(Name = "Cidade", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "CidadeObrigatória")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Cidade")]
        public string cidade { get; set; }

        [Display(Name = "Bairro", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "BairroObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Bairro")]
        public string bairro { get; set; }

        [Display(Name = "Rua", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "RuaObrigatória")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Rua")]

        public string rua { get; set; }

        [Display(Name = "Numero", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "NumeroObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Numero")]
        public string numero { get; set; }

        [Display(Name = "Descricao", ResourceType = typeof(Recursos.Resources))]
        public string descricao { get; set; }

        [Display(Name = "Telefonefixo", ResourceType = typeof(Recursos.Resources))]
        public string telefoneFixo { get; set; }

        [Display(Name = "Telefonecelular", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
    ErrorMessageResourceName = "CelularObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
            ErrorMessageResourceName = "Telefonecelular")]
        public string telefoneCelular { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
   ErrorMessageResourceName = "EmailObrigatório")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
           ErrorMessageResourceName = "Email")]
        public string email { get; set; }

        public string siteBlog { get; set; }

        public string skype { get; set; }

        [Display(Name = "Remuneracao", ResourceType = typeof(Recursos.Resources))]
        [Required(ErrorMessageResourceType = typeof(Recursos.Resources),
   ErrorMessageResourceName = "remuneracaoObrigatória")]
        [StringLength(100, ErrorMessageResourceType = typeof(Recursos.Resources),
           ErrorMessageResourceName = "Remuneracao")]
        public double remuneracao { get; set; }

        [Display(Name = "genero", ResourceType = typeof(Recursos.Resources))]       
        public string genero { get; set; }

        [Display(Name = "area", ResourceType = typeof(Recursos.Resources))]
        public AreaPretendida area
        {
            get
            {
                return Area;

            }
        }

        [Display(Name = "estado", ResourceType = typeof(Recursos.Resources))]
        public EstadoCivil estado
        {
            get
            {
                return Estado;

            }
        }

        [Display(Name = "anexo", ResourceType = typeof(Recursos.Resources))]
        public Anexo anexo
        {
            get
            {
                return Anexo;

            }
        }

    }
}
