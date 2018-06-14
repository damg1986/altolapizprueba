
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
    public class UnirseVM : clsVMBase
    {
        #region Atributos
        private clsGrupo _partidaSeleccionada;
        private bool _visibilidadPopUp = false;
        private ObservableCollection<clsGrupo> _listadoCompletoPartidas;
        private ObservableCollection<clsGrupo> _listViewPartidas;
        private string _busquedaPartidas;
        private bool _bloqueafondo;
        private DelegateCommand _unirsePartidaCommand;
        private DelegateCommand _cerrarCommand;

        private String _nombre;
        
        #endregion

        #region hub
        public HubConnection conn { get; set; }
        public IHubProxy proxyPartidaUnirse1 { get; set; }
        public IHubProxy proxyPartidaUnirse2{ get; set; }
        public IHubProxy proxyPartidaUnirse3 { get; set; }

        clsMyConnection conexion = new clsMyConnection();
        #endregion

        public UnirseVM()
        {
            listadoCompletoPartidas = new ObservableCollection<clsGrupo>();

            //listViewPartidas = rellenaListaPartidas();
            //NotifyPropertyChanged("listViewPartidas");
        }

        public clsGrupo partidaSeleccionada
        {
            get
            {
                return _partidaSeleccionada;
            }

            set
            {
                _partidaSeleccionada = value;
                NotifyPropertyChanged("partidaSeleccionada");

                //Abrir popup
                bloqueaFondo = true;
                visibilidadPopUp = true;

            }
        }

        public ObservableCollection<clsGrupo> listadoCompletoPartidas
        {
            get
            {
                return _listadoCompletoPartidas;
            }

            set
            {
                _listadoCompletoPartidas = value;
                NotifyPropertyChanged("listadoCompletoPartidas");
                listViewPartidas = rellenaListaPartidas();
                NotifyPropertyChanged("listViewPartidas");

            }
        }

        public String nombre {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public ObservableCollection<clsGrupo> listViewPartidas
        {
            get
            {
                return _listViewPartidas;
            }

            set
            {
                _listViewPartidas = value;
                NotifyPropertyChanged("listViewPartidas");
            }
        }

      

        public string busquedaPartidas
        {
            get
            {
                return _busquedaPartidas;
            }
            set
            {
                _busquedaPartidas = value;

                listViewPartidas = rellenaListaPartidas(_busquedaPartidas);
                NotifyPropertyChanged("listViewPartidas");
                //Obtener partidas que coinciden
                //for (int i = 0; i < listadoCompletoPartidas.Count; i++) {
                //    if (listadoCompletoPartidas.ElementAt(i).nombrePartida.Contains(_busquedaPartidas)) {
                //        listViewPartidas = new ObservableCollection<clsPartida>();
                //        listViewPartidas.Add(listadoCompletoPartidas.ElementAt(i));
                //    }
                //}
            }
        }

        public DelegateCommand cerrarCommand
        {
            get
            {
                _cerrarCommand = new DelegateCommand(CerrarCommand);
                return _cerrarCommand;
            }
        }

        public DelegateCommand unirsePartidaCommand
        {
            get
            {
                _unirsePartidaCommand = new DelegateCommand(UnirsePartidaCommand);
                return _unirsePartidaCommand;
            }
        }

        public bool visibilidadPopUp
        {
            get
            {
                return _visibilidadPopUp;
            }

            set
            {
                _visibilidadPopUp = value;

                NotifyPropertyChanged("visibilidadPopUp");
            }
        }

        public bool bloqueaFondo
        {
            get
            {
                return _bloqueafondo;
            }

            set
            {
                _bloqueafondo = value;
                NotifyPropertyChanged("bloqueaFondo");
            }
        }
        public async Task MostrarPantallaEsperaUnirsePartidaAsync()
        {
            ContentDialog mostrarPantallaEsperaUnirsePartida = new ContentDialog();
            StackPanel stackPanel = new StackPanel();
            TextBlock nombre = new TextBlock();

            //stackPanel.Children.Add
            mostrarPantallaEsperaUnirsePartida.Title = "Sala de espera";
            mostrarPantallaEsperaUnirsePartida.Content = "";
            mostrarPantallaEsperaUnirsePartida.PrimaryButtonText = "Cerrar";

            ContentDialogResult resultado = await mostrarPantallaEsperaUnirsePartida.ShowAsync();
        }

        /// <summary>
        /// Este método filtra la lista completa para obtener las partidas cuyo nombre
        /// contiene la palabra clave que le indicamos
        /// </summary>
        /// <param name="busqueda">La palabra clave</param>
        /// <returns>Lista filtrada</returns>
        public ObservableCollection<clsGrupo> rellenaListaPartidas(string busqueda = "")
        {
            //listadoCompletoPartidas.Add(new clsGrupo(1, "..\\Assets\\Images\\ukflag.png", "Aguachinei", 2, true));
            ObservableCollection<clsGrupo> listaFiltrada = null;
            if (busqueda.Length == 0)
            {
                listaFiltrada = new ObservableCollection<clsGrupo>(_listadoCompletoPartidas);
            }
            else
            {
                for (int i = 0; i < listadoCompletoPartidas.Count; i++)
                {
                    if (listadoCompletoPartidas.ElementAt(i).nombrePartida.Contains(_busquedaPartidas))
                    {
                        listaFiltrada = new ObservableCollection<clsGrupo>();
                        listaFiltrada.Add(listadoCompletoPartidas.ElementAt(i));
                    }
                }
            }
            return listaFiltrada;

        }

        private void UnirsePartidaCommand()
        {
            visibilidadPopUp = false;
            bloqueaFondo = false;
            clsUsuario usuario = new clsUsuario();
            usuario.nick = nombre;

            //unirJugadorAPartida();
            BroadcastListaUsuarios(usuario);

            App.listaJugadoresVM.partida = partidaSeleccionada;

            if (App.listaJugadoresVM.listadoUsuariosPartida.Count <= 6)
            {
                App.listaJugadoresVM.usuario = usuario;


                App.chatVM.mensaje.nombreGrupo = partidaSeleccionada.nombrePartida;
                App.chatVM.mensaje.nick = usuario.nick;
                App.chatVM.Conectar();
                App.chatVM.Messages.Clear();

                Frame frameActual = (Frame)Window.Current.Content;
                frameActual.Navigate(typeof(AltoElLapizUI.ListaJugadores));

                if (App.listaJugadoresVM.listadoUsuariosPartida.Count == 6) {
                    CerrarPartida();
                }
            }
            else { visibilidadPopUp = false; }
            

            //Frame frameActual = (Frame)Window.Current.Content;
            //frameActual.Navigate(typeof(AltoElLapizUI.ListaJugadores));


        }

        private void CerrarCommand()
        {
            //Frame frameActual = (Frame)Window.Current.Content;
            //frameActual.Navigate(typeof(AltoElLapiz_UI.UnirsePage)); //Este MainPage es el CrearPartida, da problemas al cambiar los nombres
            visibilidadPopUp = false;
            bloqueaFondo = false;
        }

        public void SignalRUnirse()
        {
            conn = new HubConnection(conexion.miUrl);
            proxyPartidaUnirse1 = conn.CreateHubProxy("PartidaHub");
            proxyPartidaUnirse2 = conn.CreateHubProxy("PartidaHub");
            proxyPartidaUnirse3 = conn.CreateHubProxy("PartidaHub");
            conn.Start();
            proxyPartidaUnirse1.On<ObservableCollection<clsGrupo>>("CargarListasDeGrupos", OnRellenarPartidasDeInicio);
            proxyPartidaUnirse2.On<clsUsuario>("MandaUsuario", OnNombreUsuario);
            proxyPartidaUnirse3.On<String>("EliminaPartida", OnEliminaPartida);
        }

        public void BroadcastRellenarPartidasDeInicio()
        {
            if (conn.State == ConnectionState.Connected)
            {
                proxyPartidaUnirse1.Invoke("EnviarListadoCompleto");
            }
        }

        /// <summary>
        /// Metodo que recibe el listado de nombre de las partidas creadas
        /// </summary>
        /// <param name="listado"></param>
        private async void OnRellenarPartidasDeInicio(ObservableCollection<clsGrupo> listado)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                listadoCompletoPartidas = listado;
        });
        }

        /// <summary>
        /// metodo que hace broadcast añadiendo el usuario a la lsita de los demas usuarios de ese grupo
        /// </summary>
        /// <param name="nuevaPartida"></param>
        public void BroadcastListaUsuarios(clsUsuario usuario)
        {
            if (conn.State == ConnectionState.Connected)
            {
                proxyPartidaUnirse2.Invoke("agregarUsuario", partidaSeleccionada.nombrePartida, usuario);

            }
        }


        /// <summary>
        /// Aqui llamamos al añadir el nombre del usuario al observable del VM
        /// </summary>
        /// <param name="nombrePartida"></param>
        private async void OnNombreUsuario(clsUsuario user)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                App.listaJugadoresVM.listadoUsuariosPartida.Add(user);

            });
        }

        /// <summary>
        /// va a invocar a QuitarPartida del servidor que eliminara la aprtida del observablecollections de grupos
        /// </summary>
        public void CerrarPartida()
        {
            if (conn.State == ConnectionState.Connected)
            {
                proxyPartidaUnirse3.Invoke("QuitarPartida", partidaSeleccionada.nombrePartida);
            }
        }

        private async void OnEliminaPartida(String nombrePartida)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                bool encontrado = false;
                for (int i = 0; i < listadoCompletoPartidas.Count && encontrado == false; i++)
                {
                    if (listadoCompletoPartidas.ElementAt(i).nombrePartida.Equals(nombrePartida))
                    {
                        listadoCompletoPartidas.RemoveAt(i);
                        encontrado = true;
                    }
                }


            });
        }




    }
}
