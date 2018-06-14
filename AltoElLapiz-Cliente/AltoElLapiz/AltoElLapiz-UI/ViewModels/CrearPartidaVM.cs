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

    public class CrearPartidaVM : clsVMBase
    {
        #region Atributos
        private String _nombrePartida;
        private DelegateCommand _aumentarContadorCommand, _crearPartidaCommand;
        private int contadorToggled = 0;
        private int _rondasSeleccionadas;
        public int[] _rondas = { 3, 4, 5, 6, 7, 8, 9, 10 };
        private bool _botonCrearHabilitado = false;
        private bool _toggle1, _toggle2, _toggle3, _toggle4, _toggle5, _toggle6, _toggle7, _toggle8, _toggle9, _toggle10, _toggle11, _toggle12, _toggle13, _toggle14, _toggle15, _toggle16;

        private string[] _idiomas = { "Deutsch", "English", "Español", "Français", "Hindi", "Chinese", "Japanese", "Italian", "Português" };
        private string _idiomaSeleccionado;
        private ObservableCollection<clsCategoria> _categoriasSeleccionadas;
        private ObservableCollection<clsCategoria> _categorias;

        private clsUsuario _admin;
        private bool _visibilidadPopUp = false;
        private bool _bloqueafondo;
        private DelegateCommand _unirsePartidaCommand;
        private DelegateCommand _cerrarCommand;


        #endregion

        #region hub
        public HubConnection conn { get; set; }
        public IHubProxy proxyPartidaCrear { get; set; }
        public IHubProxy proxyPartidaCrear2 { get; set; }

        clsMyConnection conexion = new clsMyConnection();
        #endregion

        #region constructor
        public CrearPartidaVM()
        {
            admin = new clsUsuario();
            admin.admin = true;

            _categorias = new ObservableCollection<clsCategoria>();
            _categoriasSeleccionadas = new ObservableCollection<clsCategoria>();
            _categorias.Add(new clsCategoria("Nombre"));
            _categorias.Add(new clsCategoria("Apellido"));
            _categorias.Add(new clsCategoria("Lugar"));
            _categorias.Add(new clsCategoria("Deporte"));
            _categorias.Add(new clsCategoria("Color"));
            _categorias.Add(new clsCategoria("Comida"));
            _categorias.Add(new clsCategoria("Profesión"));
            _categorias.Add(new clsCategoria("Animal"));
            _categorias.Add(new clsCategoria("Marca"));
            _categorias.Add(new clsCategoria("Parte del cuerpo"));
            _categorias.Add(new clsCategoria("Flor"));
            _categorias.Add(new clsCategoria("Deportista"));
            _categorias.Add(new clsCategoria("Anime"));
            _categorias.Add(new clsCategoria("Actor"));
            _categorias.Add(new clsCategoria("Libro"));
            _categorias.Add(new clsCategoria("Cantante"));
        }
        #endregion

        #region Propiedades públicas
        public int[] rondas
        {
            get
            {
                return _rondas;
            }
            set
            {
                _rondas = value;
            }
        }
        public int rondasSeleccionadas
        {
            get
            {
                return _rondasSeleccionadas;
            }
            set
            {
                _rondasSeleccionadas = value;
                NotifyPropertyChanged("rondasSeleccionadas");
            }
        }

        public string idiomaSeleccionado
        {
            get
            {
                return _idiomaSeleccionado;
            }
            set
            {
                _idiomaSeleccionado = value;
                NotifyPropertyChanged("idiomaSeleccionado");
            }
        }

        public ObservableCollection<clsCategoria> categoriasSeleccionadas
        {
            get
            {
                return _categoriasSeleccionadas;
            }
            set
            {
                _categoriasSeleccionadas = value;
            }
        }

        public string[] idiomas
        {
            get
            {
                return _idiomas;
            }
        }

        public bool botonCrearHabilitado
        {
            get
            {
                return _botonCrearHabilitado;
            }
            set
            {
                _botonCrearHabilitado = value;
                NotifyPropertyChanged("botonCrearHabilitado");
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

        public clsUsuario admin {
            get { return _admin; }
            set { _admin = value; }
        }

        #region TOGGLES
        public bool toggle1
        {
            get
            {
                return _toggle1;
            }
            set
            {
                _toggle1 = value;
                if (_toggle1)
                {
                    _categoriasSeleccionadas.Add(_categorias[0]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[0]);
                }
                comprobarToggledButtons(_toggle1);
                NotifyPropertyChanged("toggle1");
            }
        }
        public bool toggle2
        {
            get
            {
                return _toggle2;
            }
            set
            {
                _toggle2 = value;
                if (_toggle2)
                {
                    _categoriasSeleccionadas.Add(_categorias[1]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[1]);
                }
                comprobarToggledButtons(_toggle2);
                NotifyPropertyChanged("toggle2");
            }
        }
        public bool toggle3
        {
            get
            {
                return _toggle3;
            }
            set
            {
                _toggle3 = value;
                if (_toggle3)
                {
                    _categoriasSeleccionadas.Add(_categorias[2]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[2]);
                }
                comprobarToggledButtons(_toggle3);
                NotifyPropertyChanged("toggle3");
            }
        }
        public bool toggle4
        {
            get
            {
                return _toggle4;
            }
            set
            {
                _toggle4 = value;
                if (_toggle4)
                {
                    _categoriasSeleccionadas.Add(_categorias[3]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[3]);
                }
                comprobarToggledButtons(_toggle4);
                NotifyPropertyChanged("toggle4");
            }
        }
        public bool toggle5
        {
            get
            {
                return _toggle5;
            }
            set
            {
                _toggle5 = value;
                if (_toggle5)
                {
                    _categoriasSeleccionadas.Add(_categorias[4]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[4]);
                }
                comprobarToggledButtons(_toggle5);
                NotifyPropertyChanged("toggle5");
            }
        }
        public bool toggle6
        {
            get
            {
                return _toggle6;
            }
            set
            {
                _toggle6 = value;
                if (_toggle6)
                {
                    _categoriasSeleccionadas.Add(_categorias[5]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[5]);
                }
                comprobarToggledButtons(_toggle6);
                NotifyPropertyChanged("toggle6");
            }
        }
        public bool toggle7
        {
            get
            {
                return _toggle7;
            }
            set
            {
                _toggle7 = value;
                if (_toggle7)
                {
                    _categoriasSeleccionadas.Add(_categorias[6]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[6]);
                }
                comprobarToggledButtons(_toggle7);
                NotifyPropertyChanged("toggle7");
            }
        }
        public bool toggle8
        {
            get
            {
                return _toggle8;
            }
            set
            {
                _toggle8 = value;
                if (_toggle8)
                {
                    _categoriasSeleccionadas.Add(_categorias[7]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[7]);
                }
                comprobarToggledButtons(_toggle8);
                NotifyPropertyChanged("toggle8");
            }
        }
        public bool toggle9
        {
            get
            {
                return _toggle9;
            }
            set
            {
                _toggle9 = value;
                if (_toggle9)
                {
                    _categoriasSeleccionadas.Add(_categorias[8]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[8]);
                }
                comprobarToggledButtons(_toggle9);
                NotifyPropertyChanged("toggle9");
            }
        }
        public bool toggle10
        {
            get
            {
                return _toggle10;
            }
            set
            {
                _toggle10 = value;
                if (_toggle10)
                {
                    _categoriasSeleccionadas.Add(_categorias[9]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[9]);
                }
                comprobarToggledButtons(_toggle10);
                NotifyPropertyChanged("toggle10");
            }
        }
        public bool toggle11
        {
            get
            {
                return _toggle11;
            }
            set
            {
                _toggle11 = value;
                if (_toggle11)
                {
                    _categoriasSeleccionadas.Add(_categorias[10]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[10]);
                }
                comprobarToggledButtons(_toggle11);
                NotifyPropertyChanged("toggle11");
            }
        }
        public bool toggle12
        {
            get
            {
                return _toggle12;
            }
            set
            {
                _toggle12 = value;
                if (_toggle12)
                {
                    _categoriasSeleccionadas.Add(_categorias[11]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[11]);
                }
                comprobarToggledButtons(_toggle12);
                NotifyPropertyChanged("toggle12");
            }
        }
        public bool toggle13
        {
            get
            {
                return _toggle13;
            }
            set
            {
                _toggle13 = value;
                if (_toggle13)
                {
                    _categoriasSeleccionadas.Add(_categorias[12]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[12]);
                }
                comprobarToggledButtons(_toggle13);
                NotifyPropertyChanged("toggle13");
            }
        }
        public bool toggle14
        {
            get
            {
                return _toggle14;
            }
            set
            {
                _toggle14 = value;
                if (_toggle14)
                {
                    _categoriasSeleccionadas.Add(_categorias[13]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[13]);
                }
                comprobarToggledButtons(_toggle14);
                NotifyPropertyChanged("toggle14");
            }
        }
        public bool toggle15
        {
            get
            {
                return _toggle15;
            }
            set
            {
                _toggle15 = value;
                if (_toggle15)
                {
                    _categoriasSeleccionadas.Add(_categorias[14]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[14]);
                }
                comprobarToggledButtons(_toggle15);
                NotifyPropertyChanged("toggle15");
            }
        }
        public bool toggle16
        {
            get
            {
                return _toggle16;
            }
            set
            {
                _toggle16 = value;
                if (_toggle16)
                {
                    _categoriasSeleccionadas.Add(_categorias[15]);
                }
                else
                {
                    _categoriasSeleccionadas.Remove(_categorias[15]);
                }
                comprobarToggledButtons(_toggle16);
                NotifyPropertyChanged("toggle16");
            }
        }
        #endregion
        public String nombrePartida
        {
            get
            {
                return _nombrePartida;
            }
            set
            {
                _nombrePartida = value;
                //NotifyPropertyChanged("nombrePartida");
            }
        }

        public DelegateCommand aumentarContadorCommand
        {
            get
            {
                _aumentarContadorCommand = new DelegateCommand(AumentarContador);
                return _aumentarContadorCommand;
            }
        }

        public DelegateCommand crearPartidaCommand {
            get {
                _crearPartidaCommand = new DelegateCommand(creaPartida);
                return _crearPartidaCommand;
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
        #endregion
        private void AumentarContador()
        {
            contadorToggled++;
        }

        /// <summary>
        /// Incrementa o decrementa el contador de toggled y habilita o deshabilita el botón "crear" cuando sea necesario
        /// </summary>
        /// <param name="tb"></param>
        private void comprobarToggledButtons(bool tb)
        {
            if (tb)
            {
                contadorToggled++;
            }
            else
            {
                contadorToggled--;
            }
            if (contadorToggled >= 1 && contadorToggled <= 7 && botonCrearHabilitado == false)
            {
                botonCrearHabilitado = true;
            }
            else
            {
                if ((contadorToggled == 0 || contadorToggled > 7) && botonCrearHabilitado == true)
                {
                    botonCrearHabilitado = false;
                }
            }
        }

        public void creaPartida() {
           // clsGrupo grupo = new clsGrupo();

           // grupo.nombrePartida = nombrePartida;
           // grupo.estadoAbierto = true;
           // grupo.numeroRondas = rondasSeleccionadas;
           // // grupo.pais = (String)boxIdioma.SelectedValue;


           //Broadcast(grupo);

            bloqueaFondo = true;
            visibilidadPopUp = true;

        }

        public void SignalRCrearPartida() {
            conn = new HubConnection(conexion.miUrl);
            proxyPartidaCrear = conn.CreateHubProxy("PartidaHub");
            proxyPartidaCrear2 = conn.CreateHubProxy("PartidaHub");
            conn.Start();
            proxyPartidaCrear.On<clsGrupo>("MandaPartida", OnNombrePartida);
            proxyPartidaCrear2.On<clsUsuario>("MandaUsuario", OnNombreUsuario);
        }

        /// <summary>
        /// metodo que hace broadcast enviando la nueva partida creada a todos los clientes
        /// </summary>
        /// <param name="nuevaPartida"></param>
        public void Broadcast(clsGrupo nuevaPartida)
        {
            if (conn.State == ConnectionState.Connected)
            {
                proxyPartidaCrear.Invoke("EnviarPartida", nuevaPartida, admin);
            }
        }

        /// <summary>
        /// Aqui llamamos al añadir el nombre de la partida al observable del VM
        /// </summary>
        /// <param name="nombrePartida"></param>
        private async void OnNombrePartida(clsGrupo grupo)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                 App.unirseVM.listadoCompletoPartidas.Add(grupo);

            });
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

        private void UnirsePartidaCommand()
        {

            //para hacer: comprobar nombre partida en el servidor, si esta no crea la partida

            clsGrupo grupo = new clsGrupo();

            grupo.nombrePartida = nombrePartida;
            grupo.estadoAbierto = true;
            grupo.numeroRondas = rondasSeleccionadas;
            grupo.listadoDeCategorias = categoriasSeleccionadas;
            grupo.pais = "../Assets/Images/Banderas/" + idiomaSeleccionado + ".png";


            Broadcast(grupo);
            RellenaJugadores(grupo.nombrePartida);

            visibilidadPopUp = false;
            bloqueaFondo = false;

            //asigmanos el nombre de la partida en el VMListado
            App.listaJugadoresVM.partida = grupo;
            App.listaJugadoresVM.usuario = admin;

            App.chatVM.mensaje.nombreGrupo = nombrePartida;
            App.chatVM.mensaje.nick = admin.nick;
            App.chatVM.Conectar();
            App.chatVM.Messages.Clear();

            Frame frameActual = (Frame)Window.Current.Content;
            frameActual.Navigate(typeof(AltoElLapizUI.ListaJugadores)/*, nombrePartida*/);
        }

        private void CerrarCommand()
        {
            //Frame frameActual = (Frame)Window.Current.Content;
            //frameActual.Navigate(typeof(AltoElLapiz_UI.UnirsePage)); //Este MainPage es el CrearPartida, da problemas al cambiar los nombres
            visibilidadPopUp = false;
            bloqueaFondo = false;
        }

        private void RellenaJugadores(String nombrePartida) {
            if (conn.State == ConnectionState.Connected)
            {
                proxyPartidaCrear.Invoke("MandarListadoJugadores", nombrePartida);
            }
        }
    }
}
