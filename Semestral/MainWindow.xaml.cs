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

        enum EstadoJuego { GamePlay, GameOver, GameStart };
        EstadoJuego estadoActual = EstadoJuego.GameStart;
        enum Direccion { Arriba, Abajo, Derecha, Izquierda, Ninguna };  //Para aclarar la direccion del jugador
        Direccion direccionJugador = Direccion.Ninguna; //Inicializar

        //double velocidadEnem1 = 100;
        //double velocidadEnem2 = 80;
        double velocidadMeteorito = 50;

        public MainWindow()
        {
            InitializeComponent();
            canvasGamePlay.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            ThreadStart threadStart = new ThreadStart(actualizar);
            //2..Inicializar el Thread - Dar valores e instrucciones
            Thread threadMoverEnemigos = new Thread(threadStart);
            //3..Ejecutar el Thread
            threadMoverEnemigos.Start();

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

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            canvasEnemigos.Visibility = Visibility.Collapsed;
            canvasReglas.Visibility = Visibility.Visible;
        }

        private void btnNextEnemigos_Click(object sender, RoutedEventArgs e)
        {
            canvasEnemigos.Visibility = Visibility.Collapsed;
            canvasGamePlay.Visibility = Visibility.Visible;
            this.estadoActual = EstadoJuego.GamePlay;
            canvasGamePlay.Focus();
        }

       
        void moverjugador(TimeSpan deltaTime)
        {
            double LeftNaveActual = Canvas.GetLeft(imgNave);
            double bottomNaveActual = Canvas.GetTop(imgNave);
            switch (direccionJugador)
            {
                case Direccion.Arriba:
                    double topNaveActual = Canvas.GetTop(imgNave);
                    //Primero el elemento a mover, Luego los valores a mover
                    
                    if (bottomNaveActual - (velocidadMeteorito * deltaTime.TotalSeconds) >= 0)
                    {
                        Canvas.SetTop(imgNave, topNaveActual - (velocidadMeteorito * deltaTime.TotalSeconds));
                    }
                    break;
                case Direccion.Abajo:
                   
                    double nuevaPosicion1 = bottomNaveActual + (velocidadMeteorito * deltaTime.TotalSeconds);
                    if (nuevaPosicion1 + imgNave.Width <= 440)
                    {

                        Canvas.SetTop(imgNave, nuevaPosicion1);
                    }
                  

                    break;
                case Direccion.Izquierda: //Para que no salga por la izquierda

                    if (LeftNaveActual - (velocidadMeteorito * deltaTime.TotalSeconds) >= 0)
                    {
                        Canvas.SetLeft(imgNave, LeftNaveActual - (velocidadMeteorito * deltaTime.TotalSeconds));
                    }
                    break;
                case Direccion.Derecha:
                    double nuevaPosicion = LeftNaveActual + (velocidadMeteorito * deltaTime.TotalSeconds);
                    if (nuevaPosicion + imgNave.Width <= 800)
                    {

                        Canvas.SetLeft(imgNave, nuevaPosicion);
                    }

                    break;
                case Direccion.Ninguna:
                    break;
            }
        }
        void actualizar()
        {   //Invoke lleva de parametro una función
            while (true)
            {
                Dispatcher.Invoke(
                 () => //Se creo una función nueva dentro de otra. La => es para indicar que es otra función
                {
                        var tiempoActuali = stopwatch.Elapsed;
                        var deltaTime = tiempoActuali - tiempoAnterior;
                    //Para ir acelerando la velocidad del movimiento de la rana
                    //velocidadRana += 2 * deltaTime.TotalSeconds; 

                    if (estadoActual == EstadoJuego.GamePlay)
                        {
                            double leftMeteoritoActual = Canvas.GetLeft(imgMeteorito);
                        // se mueve 120 pixeles por segundo
                        Canvas.SetLeft(imgMeteorito, leftMeteoritoActual - (velocidadMeteorito * deltaTime.TotalSeconds));
                            if (Canvas.GetLeft(imgMeteorito) <= -100)
                            {
                            Canvas.SetLeft(imgMeteorito, 800);
                            }
                        //moverjugador
                        //Se agrega al parametro utilizado 
                        moverjugador(deltaTime);
                        //Intersección en X
                        double xMeteorito = Canvas.GetLeft(imgMeteorito);
                        double xNave = Canvas.GetLeft(imgNave);

                            if (xNave + imgNave.Width >= xMeteorito && xNave <= xMeteorito + imgMeteorito.Width)
                            {
                               lblinterseccionX.Text = "SI HAY INTERSECCION EN X!!!";
                            }
                            else
                            {
                               lblinterseccionX.Text = "No hay interseccion en X";
                            }
                        //Intersección en Y
                        double yMeteorito = Canvas.GetTop(imgMeteorito);
                        double yNave = Canvas.GetTop(imgNave);

                            if (yNave + imgNave.Height >= yMeteorito && yNave <= yMeteorito + imgMeteorito.Height)
                            {
                            lblinterseccionY.Text = "SI HAY INTERSECCION EN Y!!!";
                               
                            }
                            else
                            {
                            lblinterseccionY.Text = "No hay interseccion en Y";
                            }
                            if (xNave + imgNave.Width >= xMeteorito && xNave <= xMeteorito + imgMeteorito.Width &&
                            yNave + imgNave.Height >= yMeteorito && yNave <= yMeteorito + imgMeteorito.Height
                            )
                            {
                                lblcolision.Text = "HAY COLISIÓN!!";
                                this.estadoActual = EstadoJuego.GameOver;
                                canvasGamePlay.Visibility = Visibility.Collapsed;
                                canvasGameOver.Visibility = Visibility.Visible;
                            }
                            else
                            {
                            lblcolision.Text = "No hay colisión";
                            }
                        }
                        else if (this.estadoActual == EstadoJuego.GameOver)
                        {

                        }
                    tiempoAnterior = tiempoActuali;
                    }
                    );
            }
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

        }
    }
}
