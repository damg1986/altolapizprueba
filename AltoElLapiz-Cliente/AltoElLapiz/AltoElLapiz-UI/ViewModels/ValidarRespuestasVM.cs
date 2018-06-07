using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AltoElLapiz_UI.ViewModels
{
    public class ValidarRespuestasVM : clsVMBase
    {
        public int rondasJugadas;
        public ValidarRespuestasVM()
        {
            //Aquí un contador, cada vez que se navegue a esta vista,
            //se incrementa el número de rondas jugadas. Cuando coincida
            //con rondas jugadas, en el método donde se valide, al final se hará
            //un navigated a marcadores en vez de a pantalla de juego

        }

        /// <summary>
        /// Este método valida las respuestas del juego y al final, comprueba
        /// si el juego terminó o hay que seguir. En el primer caso, manda a
        /// los jugadores a la pantalla de marcadores, en caso contrario, nos
        /// devuelve a la pantalla de juego.
        /// </summary>
        /// //Álvaro
        public void validarRespuestas()
        {
            rondasJugadas++;

            //Hay que hacer la parte de validar

            //Comprobación final
            if (rondasJugadas != App.unirseVM.partidaSeleccionada.numeroRondas)
            {
                Frame frameActual = (Frame)Window.Current.Content;
                frameActual.GoBack();
            }
            else
            {
                Frame frameActual = (Frame)Window.Current.Content;
                frameActual.Navigate(typeof(AltoElLapizUI.Marcador));
            }
        }
    }
}
