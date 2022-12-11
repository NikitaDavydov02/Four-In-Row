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
using FourInRow2.ViewModel;

namespace FourInRow2.View
{
    /// <summary>
    /// Логика взаимодействия для PlayWithComputerPage.xaml
    /// </summary>
    public partial class PlayWithComputerPage : Page
    {
        private PlayWithComputerViewModel viewModel;
        public PlayWithComputerPage()
        {
            InitializeComponent();
        }
        public PlayWithComputerPage(int deep = 4, bool humanIsPlayingAsRed = true)
        {
            InitializeComponent();
            viewModel = new PlayWithComputerViewModel(humanIsPlayingAsRed, deep);
            DataContext = viewModel;
            viewModel.ComeBack += ViewModel_ComeBack;
            viewModel.NewGame += ViewModel_NewGame;
            viewModel.Close += ViewModel_Close;
        }

        private void ViewModel_Close(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewModel_ComeBack(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void firstColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(1);
        }

        private void secondColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(2);
        }

        private void thirdColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(3);
        }

        private void fourthColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(4);
        }

        private void fifthColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(5);
        }

        private void sixthColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(6);
        }

        private void seventhColoumn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MakeOneMove(7);
        }
        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Draw();
        }

        private void resingButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Resign();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveFile();
        }
    }
}
