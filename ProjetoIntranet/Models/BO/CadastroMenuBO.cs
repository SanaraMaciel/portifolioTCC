using System.Collections.Generic;
using ProjetoIntranet.Models.Entity;
using ProjetoIntranet.Models.DAO;


namespace ProjetoIntranet.Models.BO
{
    public class CadastroMenuBO
    {

        public void Gravar(CadastroMenu cadastroMenu)
        {

            CadastroMenuDAO cadastroMenuDAO = new CadastroMenuDAO();

            if (cadastroMenu.id != 0)
            {
                //altera
                cadastroMenuDAO.Update(cadastroMenu);
            }
            else
            {
                //insere
                cadastroMenuDAO.Insert(cadastroMenu);
            }

        }

        public void Delete(int id)
        {
            CadastroMenuDAO cadastroMenuDAO = new CadastroMenuDAO();

            cadastroMenuDAO.Delete(id);


        }


        public IList<CadastroMenu> listaDeLinks()
        {

            CadastroMenuDAO cmDAO = new CadastroMenuDAO();


            return cmDAO.ListarTodosLinks();
        }

        

    }
}
