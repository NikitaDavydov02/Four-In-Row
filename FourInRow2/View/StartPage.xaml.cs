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

namespace FourInRow2.View
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void twoPlayers_Click(object sender, RoutedEventArgs e)
        {
            OnGoToTwoPlayers(new EventArgs());
        }

        private void playWithComputerButtom_Click(object sender, RoutedEventArgs e)
        {
            OnGoToPlayWithComputer(new EventArgs());
        }

        public event EventHandler GoToTwoPlayers;
        public event EventHandler GoToPlayWithComputer;
        public void OnGoToTwoPlayers(EventArgs args)
        {
            EventHandler handler = GoToTwoPlayers;
            if (handler != null)
                handler(this, args);
        }
        public void OnGoToPlayWithComputer(EventArgs args)
        {
            EventHandler handler = GoToPlayWithComputer;
            if (handler != null)
                handler(this, args);
        }
    }
}
