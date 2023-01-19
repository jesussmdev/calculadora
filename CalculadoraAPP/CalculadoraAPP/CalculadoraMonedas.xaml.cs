using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using CalculadoraAPP.Model;
using Newtonsoft.Json;


namespace CalculadoraAPP
{
    /// <summary>
    /// Lógica de interacción para CalculadoraMonedas.xaml
    /// </summary>
    public partial class CalculadoraMonedas : Page
    {
        Double rateActual,unrateActual,rateSegundo,unrateSegundo,numeroValor, valorCambioMoneda;
        String nameCode,nameCodeSegundo;
        public CalculadoraMonedas()
        {
            InitializeComponent();
            OutputJson();


        }
        public void OutputJson()
        {
            //Se puede recoger tambien por web
            //WebClient client = new WebClient();
            //string rawResponseString = client.DownloadString("http://www.floatrates.com/daily/eur.json Jump ");

            //Accedo como fichero local
            string json = System.IO.File.ReadAllText(@"usd.json");
           
            Dictionary<string, Moneda> jsonResulttodict = JsonConvert.DeserializeObject<Dictionary<string, Moneda>>(json);
            

            

            List<Moneda> monedas = new List<Moneda>();
            Moneda moneda = new Moneda("USD", "Dolar", "1", jsonResulttodict["gbp"].date, "1");
            monedas.Add(moneda);
            
            
            
            foreach(Moneda m in jsonResulttodict.Values){
                monedas.Add(m);

            }



            monedas = monedas.OrderBy(x => x.name).ToList();
            comboPrimeraMoneda.DisplayMemberPath = "name";
            comboSegundaMoneda.DisplayMemberPath = "name";
            foreach (Moneda m in monedas)
            {
                
                comboPrimeraMoneda.Items.Add(m);
                comboSegundaMoneda.Items.Add(m);

            }

            comboPrimeraMoneda.SelectedItem = monedas[38];
            comboSegundaMoneda.SelectedItem = monedas[44];
        }

        private void SegundoDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
   
            String aux = SegundoDisplay.Text;
            numeroValor = Convert.ToDouble(aux);
            double resultado = valorCambioMoneda * numeroValor;
            PrimerDisplay.Text = ""+resultado;
            //prueba.Content = "" + resultado;
        }

        private void btnUno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void btnReverse_Click(object sender, RoutedEventArgs e)
        {
            int prim = comboPrimeraMoneda.SelectedIndex;
            int seg = comboSegundaMoneda.SelectedIndex;
            comboPrimeraMoneda.SelectedIndex = seg;
            comboSegundaMoneda.SelectedIndex = prim;
        }

        private void btnAC_Click(object sender, RoutedEventArgs e)
        {
            //Borra todo el contenido de la pantalla
            SegundoDisplay.Text = "0";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SegundoDisplay.Text.Length > 1)
            {
                if (!SegundoDisplay.Text.Equals(""))
                {
                    String textoOriginal = SegundoDisplay.Text.ToString();
                    String textonuevo = textoOriginal.Remove(textoOriginal.Length - 1);

                    SegundoDisplay.Text = textonuevo;
                }
                else
                {
                    SegundoDisplay.Text = "0";
                }

            }
            else
            {
                SegundoDisplay.Text = "0";
            }
            
            
        }


        private void btnComa_Click(object sender, RoutedEventArgs e)
        {
            
            SegundoDisplay.Text = SegundoDisplay.Text.ToString().Contains(",") ? SegundoDisplay.Text : $"{SegundoDisplay.Text},";
            
        }

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
            SegundoDisplay.Text = SegundoDisplay.Text.ToString() == "0" ? $"{valorSeleccionado}" : $"{SegundoDisplay.Text}{valorSeleccionado}";
            
        }

        private void comboSegundaMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Moneda m = (Moneda)comboSegundaMoneda.SelectedItem;
            //rateSegundo = double.Parse(m.rate);
            //unrateSegundo = double.Parse(m.inverseRate);
            nameCodeSegundo = m.code;

            String s = m.rate.Replace('.', ',');
            rateSegundo = Convert.ToDouble(s);
            String a = m.inverseRate.Replace('.', ',');
            unrateSegundo = Convert.ToDouble(a);
            nameCode = m.code;


            //SegundoDisplay.Text = m.rate;
            SegundoDisplay.Text = "" + 1;
            label2.Content = "" + nameCodeSegundo;

             valorCambioMoneda = rateActual * unrateSegundo;
            PrimerDisplay.Text = "" + valorCambioMoneda;
        }

        private void comboPrimeraMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Moneda m = (Moneda)comboPrimeraMoneda.SelectedItem;
            //rateActual = double.Parse(m.rate);
            //unrateActual = double.Parse(m.inverseRate);
            //rateActual = double.Parse(m.rate);

            String s = m.rate.Replace('.', ',');
            rateActual = Convert.ToDouble(s);
            String a = m.inverseRate.Replace('.', ',');
            unrateActual = Convert.ToDouble(a);
            nameCode = m.code;

            valorCambioMoneda = rateActual * unrateSegundo;
            //PrimerDisplay.Text = m.rate;
            //label1.Content = ""+ valorCambioMoneda;
            label1.Content = nameCode;
            PrimerDisplay.Text =""+ valorCambioMoneda;
        }







        


    }
}
