using AltoElLapiz_DAL;
using AltoElLapiz_Entidades;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AltoElLapiz_UI.ViewModels
{
    

    public class ListaJugadoresVM : clsVMBase
    {
        #region Atributos
        private String _nombrePartida;
        private ObservableCollection<clsUsuario> _listadoUsuariosPartida;

        #endregion

        #region hub
        public HubConnection conn { get; set; }
        public IHubProxy proxyPartidaListaJugadores { get; set; }

        clsMyConnection conexion = new clsMyConnection();
        #endregion

        public ListaJugadoresVM()
        {
            listadoUsuariosPartida = new ObservableCollection<clsUsuario>();
            //clsUsuario us1 = new clsUsuario("Dieguito", true, new ObservableCollection<clsPalabra>());
            //clsUsuario us2 = new clsUsuario("Damg", false, new ObservableCollection<clsPalabra>());
            //listadoUsuariosPartida.Add(us1);
            //listadoUsuariosPartida.Add(us2);


        }

        #region propiedades publicas
        public ObservableCollection<clsUsuario> listadoUsuariosPartida {
            get { return _listadoUsuariosPartida; }
            set { _listadoUsuariosPartida = value;
                NotifyPropertyChanged("listadoUsuariosPartida");
            }
        }

        public String nombrePartida {
            get { return _nombrePartida; }
            set { _nombrePartida = value; }
        }

        #endregion

        public void SignalRListaJugadoresEspera()
        {
            conn = new HubConnection(conexion.miUrl);
            proxyPartidaListaJugadores = conn.CreateHubProxy("PartidaHub");
            conn.Start();
            proxyPartidaListaJugadores.On<ObservableCollection<clsUsuario>>("RellenaListadoJugadores", OnMandaListadoJugadores);
        }

        private async void OnMandaListadoJugadores( ObservableCollection<clsUsuario> listaUsuarios)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                listadoUsuariosPartida = listaUsuarios;

            });
            //Frame frameActual = (Frame)Window.Current.Content;
            //frameActual.Navigate(typeof(AltoElLapizUI.ListaJugadores), nombrePartida);
        }

        public void RellenarJugadoresDeInicio()
        {
            proxyPartidaListaJugadores.Invoke("MandarListadoJugadores", nombrePartida);

        }
    }
}
