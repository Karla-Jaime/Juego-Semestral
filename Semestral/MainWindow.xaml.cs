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

namespace Semestral
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            canvasReglas.Visibility = Visibility.Visible;
        }

        private void btnNextReglas_Click(object sender, RoutedEventArgs e)
        {
            canvasReglas.Visibility = Visibility.Collapsed;
            canvasEnemigos.Visibility = Visibility.Visible;
        }

        private void btnNextEnemigos_Click(object sender, RoutedEventArgs e)
        {
            canvasEnemigos.Visibility = Visibility.Collapsed;
            canvasGamePlay.Visibility = Visibility.Visible;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            canvasEnemigos.Visibility = Visibility.Collapsed;
            canvasReglas.Visibility = Visibility.Visible;
        }
    }
}
