using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntranet.Models.Entity
{
    public class Usuario
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Nome obrigatório")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Cargo obrigatório")]
        public string cargo { get; set; }
        [Required(ErrorMessage = "Login obrigatório")]
        public string usuarioLogin { get; set; }
        [Required(ErrorMessage = "Senha obrigatória")]
        public string senha { get; set; }
        
        public string centroCusto { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        public string email { get; set; }

        public string bU { get; set; }
        [Required(ErrorMessage = "Setor obrigatório")]
        public string setor { get; set; }

        public Boolean situacao { get; set; }

        public String privilegios { get; set; }

        private List<GrupoUsuario> grupos = new List<GrupoUsuario>();

        public List<GrupoUsuario> getGrupos()
        {
            
            return grupos;
        }
        
        public void setGrupos(GrupoUsuario gu)
        {

            grupos.Add(gu);

        }

    }
}
