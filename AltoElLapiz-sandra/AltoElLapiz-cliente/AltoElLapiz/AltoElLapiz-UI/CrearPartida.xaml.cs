using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AltoElLapiz_UI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CrearPartida : Page
    {
        public String nombrePartida;
        public CrearPartida()
        {
            this.InitializeComponent();
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            
                //tblError.Text = string.Empty;
                App myApp = (Application.Current as App);
                if (myApp.conn.State != ConnectionState.Connected)
                {
                    //tblError.Text = "Disconnected!";
                    return;
                }

                nombrePartida = this.txtboxNombre.Text.Trim();
                if (nombrePartida.Length > 0)
                {
                    myApp.proxyPartida.Invoke("EnviarPartida", nombrePartida);
                }
                this.txtboxNombre.Text = string.Empty;
            
            // boton.IsEnabled = false;
            //// tbxError.Text = string.Empty;
            // string groupName = txtboxNombre.Text.Trim();
            //// string userName = tbxName.Text.Trim();
            // if (groupName.Length == 0)
            // {
            //    // tbxError.Text = "Group & user name can't be empty.";
            //     boton.IsEnabled = true;
            //     return;
            // }
            // //Connect to hub
            // App myApp = (Application.Current as App);
            // if (myApp.conn.State != ConnectionState.Connected)
            // {
            //     try
            //     {
            //         await myApp.conn.Start();
            //     }
            //     catch
            //     {
            //        // tbxError.Text = $"Can't connect to server {myApp.conn.Url}";
            //         boton.IsEnabled = true;
            //         return;
            //     }
            // }
            // //join to group
            // if (myApp.conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            // {
            //     await myApp.proxy.Invoke("JoinGroup", groupName);
            //     await myApp.proxy.Invoke("ListarPartidas");
            //     //Group info = new Group();
            //     //info.nombreGrupo = groupName;
            //     //info.Nick = userName;
            //     //Frame.Navigate(typeof(ChatRoom), info);
            // }
            // else
            // {
            //    // tbxError.Text = $"Can't connect to server {myApp.conn.Url}";
            // }
            // boton.IsEnabled = true;
        }

        private void atras_Click(object sender, RoutedEventArgs e)
        {
            Frame frameActual = (Frame)Window.Current.Content;
            frameActual.Navigate(typeof(AltoElLapiz_UI.UnirsePage));

        }
    }
}
