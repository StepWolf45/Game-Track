using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

using System.Windows.Threading;

namespace Пробный
{
    public partial class  Window1 : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        List<Rectangle> itemRemover = new List<Rectangle>();

        Random rand = new Random();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush starImage = new ImageBrush();

        Rect playerHitBox;
        int speed = 15;
        int playerSpeed = 10;
        int carNum;
        int starCounter = 30;
        int powerModeCounter = 200;
        double score;
        bool moveLeft, moveRight, gameOver, powerMode;

        public Window1()
        {
            InitializeComponent();
            myCanvas.Focus();
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
            
            
        }

        private void GameLoop(object sender, EventArgs e)
        {
            score += .05;

            starCounter -= 1;

            scoreText.Content = "  Прошло " + score.ToString("#.#") + " секунд          ";

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);


            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }

            if (starCounter < 1)
            {
                MakeStar();
                starCounter = rand.Next(600, 900);
            }

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {

                if ((string)x.Tag == "roadMarks")
                {


                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);

                    if (Canvas.GetTop(x) > 510)
                    {
                        Canvas.SetTop(x, -152);
                    }

                }


                if ((string)x.Tag == "Car")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);


                    if (Canvas.GetTop(x) > 500)
                    {
                        ChangeCars(x);
                    }

                    Rect carHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(carHitBox) && powerMode == true)
                    {
                        ChangeCars(x);
                    }
                    else if (playerHitBox.IntersectsWith(carHitBox) && powerMode == false)
                    {

                        gameTimer.Stop();
                        opo1.Visibility = Visibility.Visible;
                        opo2.Visibility = Visibility.Visible;
                        gameOver = true;
                    }

                }

                if ((string)x.Tag == "star")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5);

                    Rect starHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


                    if (playerHitBox.IntersectsWith(starHitBox))
                    {
                        itemRemover.Add(x);


                        powerMode = true;

                        powerModeCounter = 200;

                    }

                    if (Canvas.GetTop(x) > 400)
                    {
                        itemRemover.Add(x);
                    }

                }
            }

            if (powerMode == true)
            {
                f1.Visibility = Visibility.Visible;
                powerModeCounter -= 1;
                f1.Text=powerModeCounter.ToString()+" ms";
                PowerUp();
                if (powerModeCounter < 1)
                {
                    f1.Visibility = Visibility.Hidden;
                    player.Height = 140;
                    powerMode = false;
                }
            }
            else
            {
                playerImage.ImageSource = new BitmapImage(new Uri("maincar2.png", UriKind.Relative));
                myCanvas.Background = Brushes.Gray;
            }
            foreach (Rectangle y in itemRemover)
            {
                myCanvas.Children.Remove(y);
            }

            if (score >= 10 && score < 20)
            {
                speed = 12;
            }

            if (score >= 20 && score < 30)
            {
                speed = 14;
            }
            if (score >= 30 && score < 40)
            {
                speed = 16;
            }
            if (score >= 40 && score < 50)
            {
                speed = 18;
            }
            if (score >= 50 && score < 80)
            {
                speed = 22;
            }
        }
        bool a = true;
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {

                if (a == true)
                {

                    r1.Visibility = Visibility.Visible;
                    p1.Visibility = Visibility.Visible;
                    a = false;
                }
                else if (a == false)
                {

                    r1.Visibility = Visibility.Hidden;
                    p1.Visibility = Visibility.Hidden;
                    a = true;

                }
            }

            if (e.Key == Key.A)
            {
                moveLeft = true;
            }
            if (e.Key == Key.D)
            {
                moveRight = true;
            }
        }

        private void OnKeyUP(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.A)
            {
                moveLeft = false;
            }
            if (e.Key == Key.D)
            {
                moveRight = false;
            }

            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void StartGame()
        {

            speed = 8;
            gameTimer.Start();
            moveLeft = false;
            moveRight = false;
            gameOver = false;
            powerMode = false;
            score = 0;
            scoreText.Content = "Выжил: 0 секунд";
            if (p1.Visibility == Visibility.Hidden)
            {
                playerImage.ImageSource = new BitmapImage(new Uri("maincar2.png", UriKind.Relative));
                starImage.ImageSource = new BitmapImage(new Uri("star1.png", UriKind.Relative));
                player.Fill = playerImage;
                myCanvas.Background = Brushes.Gray;


                foreach (var x in myCanvas.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "Car")
                    {
                        Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                        Canvas.SetLeft(x, rand.Next(0, 430));
                        ChangeCars(x);
                    }

                    if ((string)x.Tag == "star")
                    {
                        itemRemover.Add(x);
                    }
                }
                itemRemover.Clear();
                opo1.Visibility = Visibility.Hidden;
                opo2.Visibility = Visibility.Hidden;
            }
            else {
                return;
            }
        }
        private void ChangeCars(Rectangle car)
        {
            carNum = rand.Next(1, 6);

            ImageBrush carImage = new ImageBrush();
            switch (carNum)
            {
                case 1:
                    carImage.ImageSource = new BitmapImage(new Uri("car1.png", UriKind.Relative));
                    break;
                case 2:
                    carImage.ImageSource = new BitmapImage(new Uri("car2.png", UriKind.Relative));
                    break;
                case 3:
                    carImage.ImageSource = new BitmapImage(new Uri("car3.png", UriKind.Relative));
                    break;
                case 4:
                    carImage.ImageSource = new BitmapImage(new Uri("car4.png", UriKind.Relative));
                    break;
                case 5:
                    carImage.ImageSource = new BitmapImage(new Uri("car7.png", UriKind.Relative));
                    break;
            }

            car.Fill = carImage;
            Canvas.SetTop(car, (rand.Next(100, 400) * -1));
            Canvas.SetLeft(car, rand.Next(0, 430));
            
        }

        private void p1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        private void PowerUp()
        {
            player.Height = 200;
            playerImage.ImageSource = new BitmapImage(new Uri("maincar20.png", UriKind.Relative));
            

        }
        private void MakeStar()
        {
            Rectangle newStar = new Rectangle
            {
                Height = 100,
                Width = 50,
                Tag = "star",
                Fill = starImage
            };
            Canvas.SetLeft(newStar, rand.Next(0, 430));
            Canvas.SetTop(newStar, (rand.Next(100, 400) * -1));
            myCanvas.Children.Add(newStar);

        }
    }
}