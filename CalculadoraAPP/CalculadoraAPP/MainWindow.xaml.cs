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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame1.Navigate(new CalculadoraBasica());
            tituloLabel.Content = "Estandar";
        }

        private void Estandar_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new CalculadoraBasica());
            tituloLabel.Content = "Estandar";
       
        }

        private void Moneda_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new CalculadoraMonedas());
            tituloLabel.Content = "Moneda";
         

        }

        private void hitorial_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
