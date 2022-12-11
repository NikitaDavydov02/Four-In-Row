using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Linq;
using System.Windows.Media.Animation;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Navigation;
//using Windows.UI.Xaml.Media.Animation;
//using Windows.UI.Xaml.Media.Imaging;
//using Windows.UI;

namespace FourInRow2.View
{
    /// <summary>
    /// Логика взаимодействия для Chip.xaml
    /// </summary>
    public partial class Chip : UserControl
    {
        public Chip()
        {
            InitializeComponent();
        }
        public Chip(double X, double toY, SolidColorBrush color) : this()
        {
            ellipse.Fill = color;
            Canvas.SetLeft(this, X);
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation,new PropertyPath("(Canvas.Top)"));
            animation.From = 0;
            animation.To = toY;
            animation.Duration = TimeSpan.FromMilliseconds(200);
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
