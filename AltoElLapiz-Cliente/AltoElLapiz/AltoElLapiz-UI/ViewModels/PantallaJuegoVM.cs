﻿using AltoElLapiz_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AltoElLapiz_UI.ViewModels
{
    public class PantallaJuegoVM : clsVMBase
    {
        private DelegateCommand _altoCommand;
        private DispatcherTimer _timer;
        private clsGrupo _partida;
        public PantallaJuegoVM()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += dispatcherTimer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 59);
            _timer.Start();
        }

        public clsGrupo partida{
            get{ return _partida; }
            set { _partida = value; }
        }

        public DelegateCommand altoCommand
        {
            get
            {
                _altoCommand = new DelegateCommand(AltoCommand);
                return _altoCommand;
            }
        }

        private void AltoCommand()
        {
            Frame frameActual = (Frame)Window.Current.Content;
            frameActual.Navigate(typeof(AltoElLapiz_UI.ValidarRespuestas));
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            //UpdateCommand();
            //AltoCommand(); lo comento para q no cambie de vista mientras estamos tocando la vista de partida
        }
    }
}
