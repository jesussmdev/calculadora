using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraAPP
{
    /// <summary>
    /// Lógica de interacción para CalculadoraBasica.xaml
    /// </summary>
    /// 
    /**
  * Enumerador para seleccionar el tipo de operacion
  */
   
    public enum OperacionSeleccionada
    {
        Suma,
        Resta,
        Multiplicacion,
        Division
    };

    /**
     * Clase para realizar las distintas acciones de calculo
     */
    public class Calcular
    {
        public static double Suma(double n1, double n2)
        {
            return n1 + n2;
        }
        public static double Resta(double n1, double n2)
        {
            return n1 - n2;
        }
        public static double Multiplicacion(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Division(double n1, double n2)
        {
            //No se puede dividir entre 0 porque genera un limite que tiende a infinito por lo que tenemos que tener en cuenta que el n2
            //no sea un 0
            if (n2 == 0)
            {
                MessageBox.Show("No se puede dividir entre 0", "Operación incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;

                
            }

            return n1 / n2;
        }

    }
    /**
     * Clase principal de la ventana
     */
    public partial class CalculadoraBasica : Page
    {
        double numeroAnterior, resultado;
        OperacionSeleccionada operacionSeleccionada;
  
        public CalculadoraBasica()
        {
            InitializeComponent();
        }
        /**
                 * Metodo para generar las diferentes acciones de calculo
                 */
        private void operacionBoton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultadoLabel.Content.ToString(), out numeroAnterior))
            {

                resultadoLabel.Content = "0";

            }
            //Con esto selecciono la accion a realizar cuando pulse el igual, guardando el valor en la constante numero anterior
            //El funcionamiento son con operadores ternarios valora que tipo de btn ha sido clickado y comprueba que accion realizar
            operacionSeleccionada = sender == btnMultiplicar ? OperacionSeleccionada.Multiplicacion : operacionSeleccionada;
            operacionSeleccionada = sender == btnDividir ? OperacionSeleccionada.Division : operacionSeleccionada;
            operacionSeleccionada = sender == btnSumar ? OperacionSeleccionada.Suma : operacionSeleccionada;
            operacionSeleccionada = sender == btnRestar ? OperacionSeleccionada.Resta : operacionSeleccionada;



        }



        /**
         * Metodo onclick que se le asocia a cada boton numerico 0-9
         */
        private void numeroBoton_Click(object sender, RoutedEventArgs e)
        {
            int valorSeleccionado = int.Parse((sender as Button).Content.ToString());
            /*
            if (resultadoLabel.Content.ToString() == "0")
            {
                resultadoLabel.Content = $"{valorSeleccionado}";
            }
            else
            {
                resultadoLabel.Content = $"{resultadoLabel.Content}{valorSeleccionado}";
            }
            */
            //Inicialmente el label esta a 0 por eso comparamos su valor con el 0 en caso de ser asi el valor seleccionado sera 0 
            //en caso contrario se le asocia el valor que sea seleccionado al contenido del label
            resultadoLabel.Content = resultadoLabel.Content.ToString() == "0" ? $"{valorSeleccionado}" : $"{resultadoLabel.Content}{valorSeleccionado}";
            labelHistorial.Content = resultadoLabel.Content.ToString();



        }

        /**
         * Boton para borrar todos las labels y resetea el valor a 0
         */
        private void btnAC_Click(object sender, RoutedEventArgs e)
        {
            //Borra todo el contenido de la pantalla
            resultadoLabel.Content = "0";
            labelHistorial.Content = "0";
        }

        /*
         * Boton para obtener el numero en version 1-100%
         *                                         x-%
         *                                         0-1 = 0-100%
         *                                         
         */
        private void btnModulo_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultadoLabel.Content.ToString(), out numeroAnterior))
            {
                numeroAnterior = numeroAnterior / 100;
                resultadoLabel.Content = $"{numeroAnterior}";

            }
        }
        /**
         * Metodo para tener en cuenta que solo puede existir una coma a la vez
         */
        private void btnComa_Click(object sender, RoutedEventArgs e)
        {
            /*
             Este codigo tiene el mismo funcionamiento que con operadores ternarios

            if (resultadoLabel.Content.ToString().Contains(","))
            {

            }
            else
            {
                resultadoLabel.Content = $"{resultadoLabel.Content},";
            }
            */
            resultadoLabel.Content = resultadoLabel.Content.ToString().Contains(",") ? resultadoLabel.Content : $"{resultadoLabel.Content},";
            labelHistorial.Content = resultadoLabel.Content.ToString();
        }

        /**
         * Metodo para borrar el ultimo numero de la derecha 
         */
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!resultadoLabel.Content.Equals(""))
            {
                String textoOriginal = resultadoLabel.Content.ToString();
                String textonuevo = textoOriginal.Remove(textoOriginal.Length - 1);

                resultadoLabel.Content = textonuevo;
                labelHistorial.Content = resultadoLabel.Content;
            }
            else
            {
                resultadoLabel.Content = "0";
                labelHistorial.Content = resultadoLabel.Content;
            }

        }

        private void MenuItem_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void hitorial_Click(object sender, RoutedEventArgs e)
        {

        }

        /**
* Metodo para realizar una operacion de cualquier tipo diferencia las acciones con un swith y se realizan
* mediante la clase Calcular
*/
        private void btnIgual_Click(object sender, RoutedEventArgs e)
        {
            double numeroNuevo;
            if (double.TryParse(resultadoLabel.Content.ToString(), out numeroNuevo))
            {
                switch (operacionSeleccionada)
                {
                    case OperacionSeleccionada.Suma:
                        labelHistorial.Content = numeroAnterior + " + " + numeroNuevo;
                        resultado = Calcular.Suma(numeroAnterior, numeroNuevo);
                        //resultado = numeroAnterior + numeroNuevo;
                        break;
                    case OperacionSeleccionada.Resta:
                        labelHistorial.Content = numeroAnterior + " - " + numeroNuevo;
                        resultado = Calcular.Resta(numeroAnterior, numeroNuevo);
                        break;
                    case OperacionSeleccionada.Multiplicacion:
                        labelHistorial.Content = numeroAnterior + " X " + numeroNuevo;
                        resultado = Calcular.Multiplicacion(numeroAnterior, numeroNuevo);
                        break;
                    case OperacionSeleccionada.Division:
                        labelHistorial.Content = numeroAnterior + " / " + numeroNuevo;
                        resultado = Calcular.Division(numeroAnterior, numeroNuevo);
                        break;
                }
            }
            resultadoLabel.Content = $"{resultado}";
            labelHistorial.Content = labelHistorial.Content + "=";

        }
    }
}


