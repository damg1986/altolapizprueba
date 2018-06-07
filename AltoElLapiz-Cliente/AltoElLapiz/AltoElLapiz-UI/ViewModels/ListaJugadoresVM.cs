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

        private int _opacidadBtnJugar;
        private int _opacidadTextoEspera;
        private clsUsuario _usuario;

        private DelegateCommand _jugar;
        #endregion

        #region hub
        public HubConnection conn { get; set; }
        public IHubProxy proxyPartidaListaJugadores { get; set; }
        public IHubProxy proxyPartidaListaJugadores2 { get; set; }

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

        
        public int opacidadBtnJugar
        {
            get
            {
                return _opacidadBtnJugar;
            }

            set
            {
                _opacidadBtnJugar = value;
                NotifyPropertyChanged("opacidadBtnJugar");
            }
        }
        
        public int opacidadTextoEspera
        {
            get
            {
                return _opacidadTextoEspera;
            }

            set
            {
                _opacidadTextoEspera = value;
                NotifyPropertyChanged("opacidadTextoEspera");
            }
        }

       
        public clsUsuario usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
                if (_usuario.admin)
                {
                    opacidadBtnJugar = 1;
                    opacidadTextoEspera = 0;
                }
                else
                {
                    opacidadBtnJugar = 0;
                    opacidadTextoEspera = 1;
                }
            }
        }

        public DelegateCommand jugar {
            get { _jugar = new DelegateCommand(pulsarJugar);
                return _jugar;
            }
        }



        #endregion

        #region metodos
        private void pulsarJugar()
        {
            CerrarPartida();
        }

        
        #endregion

        #region signal
        public void SignalRListaJugadoresEspera()
        {
            conn = new HubConnection(conexion.miUrl);
            proxyPartidaListaJugadores = conn.CreateHubProxy("PartidaHub");
            proxyPartidaListaJugadores2 = conn.CreateHubProxy("PartidaHub");

            conn.Start();
            proxyPartidaListaJugadores.On<ObservableCollection<clsUsuario>>("RellenaListadoJugadores", OnMandaListadoJugadores);
            proxyPartidaListaJugadores.On<String>("EliminaPartida", OnEliminaPartida);
        }

        private async void OnMandaListadoJugadores( ObservableCollection<clsUsuario> listaUsuarios)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                listadoUsuariosPartida = listaUsuarios;

            });
        }

        /// <summary>
        /// invoca en el servidor que mande el listado al cliente de jugadores que ya estan en esta partida
        /// </summary>
        public void RellenarJugadoresDeInicio()
        {
            proxyPartidaListaJugadores.Invoke("MandarListadoJugadores", nombrePartida);

        }

        /// <summary>
        /// va a invocar a QuitarPartida del servidor que eliminara la aprtida del observablecollections de grupos
        /// </summary>
        public void CerrarPartida()
        {
            proxyPartidaListaJugadores2.Invoke("QuitarPartida", nombrePartida);

        }

        private async void OnEliminaPartida(String nombrePartida)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                bool encontrado = false;
                for (int i = 0; i < App.unirseVM.listadoCompletoPartidas.Count && encontrado == false; i++)
                {
                    if (App.unirseVM.listadoCompletoPartidas.ElementAt(i).nombrePartida.Equals(nombrePartida))
                    {
                        App.unirseVM.listadoCompletoPartidas.RemoveAt(i);
                        encontrado = true;
                    }
                }
               

            });
        }

        #endregion
    }
}
