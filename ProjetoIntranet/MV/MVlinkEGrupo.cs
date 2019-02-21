
using ProjetoIntranet.Models.Entity;
using System.Collections.Generic;

using System.Web.Mvc;

namespace ProjetoIntranet.MV
{
    public class MVlinkEGrupo
    {

        public List<CadastroMenu>ListaDeLinks { get; set; }

        public List<SelectListItem> ListaDeGrupos { get; set; }

        public List<int>LinksIds { get; set; }

        public int GrupoId { get; set; }

    }


}