using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.Models.BO
{
    public class AutenticadorLDAP
    {

        public AutenticadorLDAP( String urlDominio,String logon,String senhaDeUsuario)
        {
           
            this.dominio = urlDominio;
            
           

            this.logonDeUsuario = logon;

            this.senha = senhaDeUsuario;

        }

        private String dominio { get; set; }

        private String logonDeUsuario { get; set; }

        private String senha { get; set; }



        public bool autenticar()
        {

            try
            {

                DirectoryEntry entry = new DirectoryEntry(dominio,logonDeUsuario,senha );
                DirectorySearcher ds = new DirectorySearcher(entry);
                ds.Filter = "(SAMAccountName=" + logonDeUsuario + ")";
               
                SearchResult sr = ds.FindOne();

                var flag = Convert.ToInt32(sr.Properties["userAccountControl"][0]); 
                if (flag == 512 || flag == 66048)
                {


                    return true;

                }
                else
                {

                    return false;
                }
               

            }
            catch
          (Exception ex)
            {

               

                return false;
            }


        }

    }
    

}