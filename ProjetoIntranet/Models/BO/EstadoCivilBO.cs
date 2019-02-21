using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;


namespace ProjetoIntranet.Models.BO
{
    public class EstadoCivilBO
    {
        public void Gravar(EstadoCivil estadoCivil)
        {

            EstadoCivilDAO estadoCivilDAO = new EstadoCivilDAO();

            if (estadoCivil.id != 0)
            {
                //altera
                estadoCivilDAO.Update(estadoCivil);
            }
            else
            {
                //insere
                estadoCivilDAO.Insert(estadoCivil);
            }

        }

       

        public List<EstadoCivil> ListarEstadoCivil()
        {
            List<EstadoCivil> estados = new List<EstadoCivil>();
            EstadoCivilDAO estadoDao = new EstadoCivilDAO();
            estados = estadoDao.ListarEstadoCivil();
            return estados;
        }
    }
}
