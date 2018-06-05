using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AltoElLapiz_UI.ViewModels
{
    public class MainPageVM
    {
        private DelegateCommand _unirseCommand;
        private DelegateCommand _crearCommand;

        public MainPageVM()
        {

        }

        public DelegateCommand unirseCommand
        {
            get
            {
                _unirseCommand = new DelegateCommand(UnirseCommand);
                return _unirseCommand;
            }
        }

        public DelegateCommand crearCommand
        {
            get
            {
                _crearCommand = new DelegateCommand(CrearCommand);
                return _crearCommand;
            }
        }

        private void UnirseCommand()
        {
            Frame frameActual = (Frame) Window.Current.Content;
            frameActual.Navigate(typeof(AltoElLapiz_UI.UnirsePage));
        }

        private void CrearCommand()
        {
            Frame frameActual = (Frame)Window.Current.Content;
            frameActual.Navigate(typeof(AltoElLapizUI.MainPage)); //Este MainPage es el CrearPartida, da problemas al cambiar los nombres
        }
    }
}
