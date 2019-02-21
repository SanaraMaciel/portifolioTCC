using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.Models.Entity
{
    public class Anexo
    {
        public int id { get; set; }
        public string nome { get; set; }
        public byte[] arquivo { get; set; }
        public string tipoArquivo { get; set; }
    }
}