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
///Librerias para multiprocesamientos
using System.Threading; //Se agrego esta libreria para Threading
using System.Diagnostics; // Para el dispatcher y timers 


namespace Semestral
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch; //Toma el tiempo de ejecución del programa
        TimeSpan tiempoAnterior; //Timespan guarda rangos de tiempo

        enum EstadoJuego { GamePlay, GameOver, GameStart, GameRules, GameEnemies };
        EstadoJuego estadoActual = EstadoJuego.GamePlay;

        enum Direccion { Arriba, Abajo, Derecha, Izquierda, Ninguna };  //Para aclarar la direccion del jugador
        Direccion direccionJugador = Direccion.Ninguna; //Inicializar

        double velocidadEnem1 = 80;
        double velocidadEnem2 = 80;
        double velocidadMeteorito = 80;

        public MainWindow()
        {
            InitializeComponent();
            canvasGamePlay.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

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

        private void canvasGamePlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (estadoActual == EstadoJuego.GamePlay)
            {
                if (e.Key == Key.Up)
                {
                    direccionJugador = Direccion.Arriba;
                }
                if (e.Key == Key.Down)
                {
                    direccionJugador = Direccion.Abajo;
                }

                if (e.Key == Key.Left)
                {
                    direccionJugador = Direccion.Izquierda;
                }

                if (e.Key == Key.Right)
                {
                    direccionJugador = Direccion.Derecha;
                }
                ///Con las teclas WASD{}
                //                if (e.Key == Key.W)
                {
                    direccionJugador = Direccion.Arriba;
                }

                if (e.Key == Key.S)
                {
                    direccionJugador = Direccion.Abajo;
                }

                if (e.Key == Key.A)
                {
                    direccionJugador = Direccion.Izquierda;
                }

                if(e.Key == Key.D)
                {
                    direccionJugador = Direccion.Derecha;
                }
            }
        }

        private void canvasGamePlay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && direccionJugador == Direccion.Arriba)
            {
                direccionJugador = Direccion.Ninguna;
            }

            if (e.Key == Key.Down && direccionJugador == Direccion.Abajo)
            {
                direccionJugador = Direccion.Ninguna;
            }

            if (e.Key == Key.Left && direccionJugador == Direccion.Izquierda)
            {
                direccionJugador = Direccion.Ninguna;
            }

            if (e.Key == Key.Right && direccionJugador == Direccion.Derecha)
            {
                direccionJugador = Direccion.Ninguna;
            }

            ///Con las teclas WASD
            if(e.Key == Key.W && direccionJugador == Direccion.Arriba)
            {
                direccionJugador = Direccion.Ninguna; 
            }
            if (e.Key == Key.S && direccionJugador == Direccion.Abajo)
            {
                direccionJugador = Direccion.Ninguna;
            }

            if (e.Key == Key.A && direccionJugador == Direccion.Izquierda)
            {
                direccionJugador = Direccion.Ninguna;
            }

            if (e.Key == Key.D && direccionJugador == Direccion.Derecha)
            {
                direccionJugador = Direccion.Ninguna;
            }

        }
    }
}
