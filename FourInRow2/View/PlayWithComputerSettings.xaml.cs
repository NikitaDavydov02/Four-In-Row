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
    /// Логика взаимодействия для PlayWithComputerSettings.xaml
    /// </summary>
    public partial class PlayWithComputerSettings : Page
    {
        public PlayWithComputerSettings()
        {
            InitializeComponent();
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (slider.Value == 0)
            {
                MessageBox.Show("Выберите уровень сложности игры!");
            }
            if ((bool)playAsRed.IsChecked)
                parametr = slider.Value;
            else
                parametr = slider.Value * -1;
            OnStartGameWithComputer((int)slider.Value, (bool)playAsRed.IsChecked);
        }
        private double parametr;

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBlock.Text = slider.Value.ToString();
        }
        //private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    textBlock.Text = slider.Value.ToString();
        //}
        public event EventHandler StartGameWithComputer;
        public void OnStartGameWithComputer(int deep, bool humanIsPlayingAsRed)
        {
            EventHandler hadler = StartGameWithComputer;
            if (hadler != null)
                hadler(this, new StartGameWithComputerEventArgs(deep, humanIsPlayingAsRed));
        }
    }
    public class StartGameWithComputerEventArgs : EventArgs 
    { 
        public int Deep { get; private set; }
        public bool HumanIsPlayingAsRed { get; private set; }
        public StartGameWithComputerEventArgs(int deep, bool humanIsPlayingAsRed)
        {
            Deep = deep;
            HumanIsPlayingAsRed = humanIsPlayingAsRed;
        }
    }

}
