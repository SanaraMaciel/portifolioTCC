

using ProjetoIntranet.Models.DAO;
using ProjetoIntranet.Models.Entity;
using System;

namespace ProjetoIntranet.Models.BO
{
    public class HistoricoBO
    {
        public void Gravar(Historico historico, String mensagem)
        {

            HistoricoDAO historicoDAO = new HistoricoDAO();

            historicoDAO.Insert(historico,mensagem);

            //if (historico.id != 0)
            //{
            //    //altera
            //    historicoDAO.Update(historico);
            //}
            //else
            //{
            //    //insere
               
            //}

        }
        
        public void Delete(Historico historico)
        {
            HistoricoDAO historicoDAO = new HistoricoDAO();

            historicoDAO.Delete(historico);


        }


    }
}
