using System.Collections.Generic;
using ProjetoIntranet.Models.DAO;
using ProjetoIntranet.Models.Entity;

namespace ProjetoIntranet.Models.BO
{
    public class AreaPretendidaBO
    {
        public void Gravar(AreaPretendida areaPretendida)
        {

            AreaPretendidaDAO areaPretendidaDAO = new AreaPretendidaDAO();
            
              if (areaPretendida.id != 0)
                {
                    //altera
                    areaPretendidaDAO.Update(areaPretendida);
                }
                else
                {
                    //insere
                    areaPretendidaDAO.Insert(areaPretendida);
                }

            }



        public void Delete(int id)
        {
            AreaPretendidaDAO areaPretendidaDAO = new AreaPretendidaDAO();

            areaPretendidaDAO.Delete(id);


        }

        public List<AreaPretendida> ListarAreas()
        {
            AreaPretendidaDAO areaDao = new AreaPretendidaDAO();
            return areaDao.ListarAreas();

        }






    }


}

