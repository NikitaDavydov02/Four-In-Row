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
using System.Windows.Shapes;

namespace FourInRow2.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartPage startPage;
        private TwoPlayersPage twoPlayersPage;
        private PlayWithComputerSettings playWithComputerSettings;
        private PlayWithComputerPage playWithComputerPage;
        //private Training trainingPage;
        public MainWindow()
        {
            InitializeComponent();
            startPage = new StartPage();

            this.Content = startPage;
            startPage.GoToTwoPlayers += StartPage_GoToTwoPlayers;
            startPage.GoToPlayWithComputer += StartPage_GoToPlayWithComputer;
        }

        private void StartPage_GoToPlayWithComputer(object sender, EventArgs e)
        {
            playWithComputerSettings = new PlayWithComputerSettings();
            playWithComputerSettings.StartGameWithComputer += PlayWithComputerSettings_StartGameWithComputer;
            this.Content = playWithComputerSettings;
        }

        private void PlayWithComputerSettings_StartGameWithComputer(object sender, EventArgs e)
        {
            StartGameWithComputerEventArgs args = e as StartGameWithComputerEventArgs;
            playWithComputerPage = new PlayWithComputerPage(args.Deep, args.HumanIsPlayingAsRed);
            this.Content = playWithComputerPage;
        }

        private void StartPage_GoToTwoPlayers(object sender, EventArgs e)
        {
            twoPlayersPage = new TwoPlayersPage();
            this.Content = twoPlayersPage;
        }

        private void StartPage_GoToSettings(object sender, EventArgs e)
        {
            //Settings settingsPage = new Settings();
            //this.Content = settingsPage;
            //settingsPage.GoToHomePage += SettingsPage_GoToHomePage;
        }

        private void SettingsPage_GoToHomePage(object sender, EventArgs e)
        {
            //this.Content = startPage;
        }

        private void StartPage_TrainingStart(object sender, EventArgs e)
        {
            //TrainingStartEventArgs args = e as TrainingStartEventArgs;
            //trainingPage = new Training(args.trainingViewModel);
            //trainingPage.TrainingFinished += TrainingPage_TrainingFinished;
            //this.Content = trainingPage;
        }

        private void TrainingPage_TrainingFinished(object sender, EventArgs e)
        {
           // this.Content = startPage;
            //startPage.TrainigIsFinished();
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (this.Content == startPage)
            //{
            //    if (startPage.CheckForUnsavedChanges())
            //    {
            //        MessageBoxResult result = System.Windows.MessageBox.Show("У вас есть несохранённые изменения в картах. Вы действительно хотите продолжить?", "", MessageBoxButton.YesNo);
            //        if (result == MessageBoxResult.No)
            //            e.Cancel = true;
            //    }
            //}
            //else if (this.Content == trainingPage)
            //{
            //    if (trainingPage.CheckForUnsavedChanges())
            //        e.Cancel = true;
            //}
        }
    }
}
