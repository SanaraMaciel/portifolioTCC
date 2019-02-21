
using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;
using System;

namespace ProjetoIntranet.Models.BO
{
    public class CurriculoBO
    {
        public void Gravar(Curriculo curriculo, Anexo anexo, DateTime dataEnvio)
        {
            CurriculoDAO curriculoDAO = new CurriculoDAO();
            AnexoDAO anexoDAO = new AnexoDAO();
            

                //insere curriculo com anexo
                curriculoDAO.InsertCvAnexo(curriculo,anexo,dataEnvio);
            

        }

        public void Gravar(Curriculo curriculo, DateTime dataEnvio)
        {
            CurriculoDAO curriculoDAO = new CurriculoDAO();
          

            if (curriculo.nome != String.Empty && curriculo.email != String.Empty && curriculo.telefoneCelular != String.Empty)
            {
                //insere curriculo sem anexo
                curriculoDAO.Insert(curriculo, dataEnvio);
            }
            

        }


        public void Delete(int id)
        {
            CurriculoDAO curriculoDAO = new CurriculoDAO();

            curriculoDAO.Delete(id);

        }

        public List<Curriculo> ListarCurriculos()
        {
            CurriculoDAO curriculoDao = new CurriculoDAO();
            return curriculoDao.ListarCurriculos();

        }

        public List<Curriculo> ListarPorArea(string area)
        {
            CurriculoDAO curriculoDao = new CurriculoDAO();
            return curriculoDao.ListarPorArea(area);

        }

        public Curriculo ReadById(int id)
        {
            CurriculoDAO curriculoDao = new CurriculoDAO();
            return curriculoDao.ReadById(id);
        }


        public List<Curriculo> ListarPorData(string dataInicial, string dataFim)
        {
            CurriculoDAO curriculoDao = new CurriculoDAO();
            return curriculoDao.ListarPorData(dataInicial, dataFim);

        }

    }
}
