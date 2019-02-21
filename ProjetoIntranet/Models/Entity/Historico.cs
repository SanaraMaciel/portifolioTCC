using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntranet.Models.Entity
{
    public class Historico
    {
        public int id { get; set; }

        public DateTime dataHora { get; set; }

        public Usuario usuario { get; set; }

        public String mensagem { get; set; }

    }
}
