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
		//Atributo listado de partidas(clsgrupo) que se van creando que contiene tambien el listado de jugadores de esa partida
        public static ObservableCollection<clsGrupo> listadoDeGrupos = new ObservableCollection<clsGrupo>();      
        //public static int jugadoresUnidosAUnGrupo = 1;

        /// <summary>
        /// add connection to group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        //public Task JoinGroup(string groupName,clsUsuario usuario)
        //{
        //    agregarUsuario(groupName,usuario);           
        //    return Groups.Add(Context.ConnectionId, groupName);
       // }
	   
	   //metodos para VM crear partida

       /// <summary>
        /// Envia un broadCast con el nombre d grupo,
        /// crea un nuevo grupo en el servidor y añade el
        /// nombre del nuevo grupo al array de nombre de partidas 
        /// </summary>
        /// <param name="grupo"></param>
        public void EnviarPartida(clsGrupo grupo, clsUsuario admin)
        {
            listadoDeGrupos.Add(grupo);
            agregarUsuario(grupo.nombrePartida,admin);
            Groups.Add(Context.ConnectionId, grupo.nombrePartida);
            Clients.All.MandaPartida(grupo);
        }
		
		//metodos para VM unirse a partida
		
		 
		
		   /// <summary>
        /// Este metodo nos permite enviar el listado
        /// de los nombre de los grupos
        /// </summary>
        public void EnviarListadoCompleto()
        {
            Clients.Caller.CargarListasDeGrupos(listadoDeGrupos);
        }
		
		  /// <summary>
        /// Este metodo es para agregar a un usuario a la lista de usuario de un grupo y lo envia a todos los usuarios de ese grupo
        /// </summary>
        /// <param name="nombreGrupo"></param>
        /// <param name="usuario"></param>
        public void agregarUsuario(String nombreGrupo,clsUsuario usuario)
        {
            bool encontrado=false;
            for (int i = 0; i < listadoDeGrupos.Count && encontrado == false; i++)
            {
                if (listadoDeGrupos.ElementAt(i).nombrePartida.Equals(nombreGrupo))
                {
                    listadoDeGrupos.ElementAt(i).listadoDeUsuario.Add(usuario);
                    encontrado = true;
                }
            }
			Groups.Add(Context.ConnectionId, nombreGrupo);
			Clients.Group(nombreGrupo).MandaUsuario(usuario);
        }
		
		//metodos para VM  lista de jugadores

        /// <summary>
        /// Va a mandar la lista de jugadores que pertenencen a un grupo 
        /// </summary>
        /// <param name="nombreGrupo"></param>
        public void MandarListadoJugadores(String nombreGrupo)
        {
            ObservableCollection<clsUsuario> listadoJugadores = new ObservableCollection<clsUsuario>();
            bool encontrado = false;
            for (int i = 0; i < listadoDeGrupos.Count && encontrado == false; i++)
            {
                if (listadoDeGrupos.ElementAt(i).nombrePartida.Equals(nombreGrupo))
                {
                    listadoJugadores = listadoDeGrupos.ElementAt(i).listadoDeUsuario;
                    encontrado = true;
                }
            }
            Clients.Caller.RellenaListadoJugadores(listadoJugadores);
        }


       

        //crear un metodo para eliminar de la lista los grupos que esten completos
        //public void EliminarGrupos(clsGrupo grupo)
        //{
        //    if (grupo.estadoAbierto == false)
        //    {
        //        Groups.Remove(Context.ConnectionId,grupo.nombrePartida);
        //        Clients.All.MandaPartida(grupo);
        //    }          
        //}






        //public void Send(ChatMessage message)
        //{
        //    Clients.All.broadcastMessage(message);
        //}

    }
}