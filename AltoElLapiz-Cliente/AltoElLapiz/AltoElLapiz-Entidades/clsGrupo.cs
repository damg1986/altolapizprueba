using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoElLapiz_Entidades
{
    public class clsGrupo : clsPartida
    {
        //Atributos
        public int numeroParticipantes { get; set; }
        public bool estadoAbierto { get; set; }
        public ObservableCollection<clsUsuario> listadoDeUsuario { get; set; }

        //Constructor por parametros
        public clsGrupo(int numeroRondas, string pais, string nombrePartida, int numeroParticipantes, bool estadoAbierto, ObservableCollection<clsUsuario> listadoDeUsuario)
        : base(numeroRondas, pais, nombrePartida)
        {
            this.numeroParticipantes = numeroParticipantes;
            this.estadoAbierto = estadoAbierto;
            this.listadoDeUsuario = listadoDeUsuario;
        }


        //Constructor por defecto
        public clsGrupo()
        : base()
        {
            this.numeroParticipantes = 0;
            this.estadoAbierto = true;
            this.listadoDeUsuario = new ObservableCollection<clsUsuario>();

        }

    }
}
