﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoLapizService.DataObjects
{
    public class clsGrupo : clsPartida
    {
        //Atributos
        public int numeroParticipantes { get; set; }
        public bool estadoAbierto { get; set; }
        public ObservableCollection<clsUsuario> listadoDeUsuario { get; set; }

        public string pais { get; set; }

        public ObservableCollection<clsCategoria> listadoDeCategorias { get; set; }

        //Constructor por parametros
        public clsGrupo(int numeroRondas, string pais, string nombrePartida, int numeroParticipantes, bool estadoAbierto, ObservableCollection<clsUsuario> listadoDeUsuario, ObservableCollection<clsCategoria> listadoDeCategoria)
        : base(numeroRondas, pais, nombrePartida)
        {
            this.numeroParticipantes = numeroParticipantes;
            this.estadoAbierto = estadoAbierto;
            this.listadoDeUsuario = listadoDeUsuario;
            this.listadoDeCategorias = listadoDeCategoria;
            this.pais = pais;
        }


        //Constructor por defecto
        public clsGrupo()
        : base()
        {
            this.numeroParticipantes = 0;
            this.estadoAbierto = true;
            this.listadoDeUsuario = new ObservableCollection<clsUsuario>();
            this.listadoDeCategorias = new ObservableCollection<clsCategoria>();
            this.pais = "";

        }

    }
}
