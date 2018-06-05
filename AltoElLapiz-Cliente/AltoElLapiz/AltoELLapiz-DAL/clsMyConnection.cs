using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoElLapiz_DAL
{
    public class clsMyConnection
    {
        //Atributos
        public String miUrl { get; set; }

        //Constructores

        public clsMyConnection()
        {

            
            this.miUrl = "http://altolapizservice.azurewebsites.net";
            //this.miUrl = "http://localhost:59991/";
        }
        //Con parámetros por si quisiera cambiar las conexiones
        public clsMyConnection(String miUrl)
        {
            this.miUrl = miUrl;
        }

    }
}
