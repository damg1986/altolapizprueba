using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoElLapiz_Entidades
{
    public class clsUsuario
    {
        //Atributos

        public String nick { get; set; }
        // public int idGrupo { get; set; }
        public bool admin { get; set; }
        public ObservableCollection<clsPalabra> arrayPalabras { get; set; }


        //Constructor por parámetros

        public clsUsuario(string nick, bool admin /*, int idGrupo*/, ObservableCollection<clsPalabra> arrayPalabras)
        {
            this.nick = nick;
            this.admin = admin;
            //   this.idGrupo = idGrupo;
            this.arrayPalabras = arrayPalabras;
        }

        //Constructor por defecto
        public clsUsuario()
        {
            this.nick = "";
            this.admin = false;
            // this.idGrupo = 0;
            this.arrayPalabras = new ObservableCollection<clsPalabra>();
        }

    }
}
