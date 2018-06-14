using AltoElLapiz_DAL;
using AltoElLapiz_Entidades;
using AltoElLapiz_UI.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace AltoElLapiz_UI.ChatNervion_Windows_CS.ViewModels
{
    public class ChatVM : clsVMBase
    {
        #region Atributos
        private clsMensajeChat _mensaje = new clsMensajeChat();
        private ObservableCollection<clsMensajeChat> _Messages = new ObservableCollection<clsMensajeChat>();
        private DelegateCommand _enviarMensaje;
        #endregion

        #region hub
        public HubConnection conn { get; set; }
        public IHubProxy proxyChat { get; set; }
        clsMyConnection conexion = new clsMyConnection();
        private IDisposable receiveMessageHandler { get; set; }
        #endregion

        public ChatVM()
        {
            
        }

        public clsMensajeChat mensaje {
            get { return _mensaje; }
            set { _mensaje = value;
                NotifyPropertyChanged("mensaje");
            }
        }

        public ObservableCollection<clsMensajeChat> Messages {
            get { return _Messages; }
            set { _Messages = value;
                NotifyPropertyChanged("Messages");
            }
        }

        public DelegateCommand enviarMensaje
        {
            get
            {
                _enviarMensaje = new DelegateCommand(Enviar);
                return _enviarMensaje;
            }
        }

       

        public void SignalRChat()
        {
            conn = new HubConnection(conexion.miUrl);
            proxyChat = conn.CreateHubProxy("ChatHub");
            conn.Start();
            receiveMessageHandler = proxyChat.On<string, string>("receivemessage", ReceiveMessage);
        }

        private async void ReceiveMessage(string nick, string message)
        {
            clsMensajeChat msj = new clsMensajeChat();
            msj.nick = nick;
            msj.mensaje = message;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
               Messages.Add(msj);
            });
        }


        /// <summary>
        /// metodo que hace join en el grupo del servidor
        /// </summary>
        public void Conectar()
        {
            proxyChat.Invoke("JoinGroup", mensaje.nombreGrupo);   
        }

        /// <summary>
        /// Invoke SendToGroup method 
        /// </summary>
        private void Enviar()
        {
            if (mensaje.mensaje.Length > 0)
            {
                proxyChat.Invoke("SendToGroup", mensaje);
            }
            mensaje.mensaje = "";
        }
    }
}
