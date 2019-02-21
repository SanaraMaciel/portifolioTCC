using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;


namespace ProjetoIntranet.Models.BO
{
    public class AnexoBO
    {
        public int Gravar(Anexo anexo)
        {
            int idAnexo = 0;
            AnexoDAO anexoDAO = new AnexoDAO();
            Curriculo curriculo = new Curriculo();



            if (anexo.id >0 )
            {
               
                    //altera
                   anexoDAO.Update(anexo);
                
                

            }
            else
            {
                //insere
               idAnexo = anexoDAO.Insert(anexo);
            }

            return idAnexo;
        }

        public void Delete(int id)
        {
            AnexoDAO anexoDAO = new AnexoDAO();

            anexoDAO.Delete(id);

        }

        public Anexo ReadById(int id)
        {
            AnexoDAO anexoDAO = new AnexoDAO();
            return anexoDAO.ReadById(id);
        }

    }


}

