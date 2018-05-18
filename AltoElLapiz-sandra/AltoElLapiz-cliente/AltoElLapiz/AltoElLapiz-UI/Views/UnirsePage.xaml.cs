using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace AltoElLapiz_UI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UnirsePage : Page
    {
        private IDisposable receiveMessageHandler { get; set; }

        public UnirsePage()
        {
            this.InitializeComponent();
           // this.listViewPartidas.Items.Add($"{listaNonbreGrupos}");
        }



        /// <summary>
        /// get user name and group name from the mainpage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App myApp = (Application.Current as App);
            receiveMessageHandler = myApp.proxyPartida.On<string>("recibirPartida", RecibirPartida);

        }

        private async void RecibirPartida(string nombrePartida)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.listViewPartidas.Items.Add($"{nombrePartida}");
            });
        }

        //private async void ObtenerListaPartidas(ObservableCollection<String> listaNonbreGrupos)
        //{
        //   // HubConnection c = new HubConnection("http://localhost:59991/Get");
        //    await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        this.listViewPartidas.Items.Add($"{listaNonbreGrupos}");
        //    });
        //}

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    App myApp = (Application.Current as App);
        //    receiveMessageHandler = myApp.proxy.On<ObservableCollection<String>>("obtenerListaPartidas", ObtenerListaPartidas);

        //}

        //private async void ObtenerListaPartidas(ObservableCollection<String> listaNonbreGrupos)
        //{
        //    // HubConnection c = new HubConnection("http://localhost:59991/Get");
        //    await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        this.listViewPartidas.Items.Add($"{listaNonbreGrupos}");
        //    });
        //}
    }
}
