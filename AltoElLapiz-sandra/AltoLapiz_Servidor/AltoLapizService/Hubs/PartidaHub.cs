using AltoLapizService.DataObjects;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AltoLapizService.Hubs
{
    [HubName("PartidaHub")]
    public class PartidaHub:Hub
    {
        public static ObservableCollection<String> grupos = new ObservableCollection<String>();

        
        /// <summary>
        /// add connection to group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Task JoinGroup(string groupName)
        {                      
            grupos.Add(groupName);
            return Groups.Add(Context.ConnectionId, groupName);
        }
        
        public void ListarPartidas()
        {
            Clients.All.ObtenerListaPartidas(grupos);
        }

        public void EnviarPartida(String nombrePartida)
        {
            Clients.All.RecibirPartida(nombrePartida);
        }


    }
}