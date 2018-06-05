using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltoLapizService.DataObjects
{
    public class clsPartida
    {
   
        public int numeroRondas { get; set; }
        public String idioma { get; set; }
        public String nombrePartida { get; set; }

        public clsPartida()
        {
            this.numeroRondas = 0;
            this.idioma = " ";
            this.nombrePartida = " ";
        }


        public clsPartida(int numeroRondas, string idioma, string nombrePartida)
        {
            this.numeroRondas = numeroRondas;
            this.idioma = idioma;
            this.nombrePartida = nombrePartida;
        }


    }
}