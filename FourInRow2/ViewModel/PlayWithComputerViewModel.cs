using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using FourInRow2.View;
using FourInRow2.Model;
//using Windows.UI.Popups;
//using Windows.UI;
//using Windows.UI.Xaml.Media;
using System.ComponentModel;
//using Windows.Storage;
//using Windows.Storage.Pickers;
//using Windows.UI.Xaml;
using System.Linq;
using System.Windows.Media;

namespace FourInRow2.ViewModel
{
        class PlayWithComputerViewModel : INotifyPropertyChanged
        {
            public System.Windows.Visibility ComputerIsThinking { get; private set; }
            public bool ButtonsEnabled
            {
                get
                {
                    if (model.GameRecord.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            public SolidColorBrush Color
            {
                get
                {
                    if (model.color == 1)
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 225, 0, 0));
                    else
                        return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 128, 0));
                }
            }
            public INotifyCollectionChanged GameRecord { get { return gameRecord; } }
            public ObservableCollection<string> gameRecord = new ObservableCollection<string>();
            public INotifyCollectionChanged Chips { get { return chips; } }
            private ObservableCollection<Chip> chips = new ObservableCollection<Chip>();
            private PlayWithComputerModel model;
            private bool humanIsPlayingAsRed;
            DispatcherTimer timer = new DispatcherTimer();
            private int deep;
            public PlayWithComputerViewModel(bool humanIsStarter, int deep)
            {
                this.deep = deep;
                if (humanIsStarter)
                    model = new PlayWithComputerModel(2);
                else
                    model = new PlayWithComputerModel(1);
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                model.ChipChanged += ChipChangedEventHandler;
                //saveFile = null;
                humanIsPlayingAsRed = humanIsStarter;
                if (!humanIsPlayingAsRed)
                    MakeOneMove(4);
            }

            private void Timer_Tick(object sender, object e)
            {
                MakeOneMove(model.ToThinkAboutMove(deep));
                timer.Stop();
            }
            private void ChipChangedEventHandler(object sender, ChipChangedEventArgs e)
            {
                if (e.Color == 1)
                    chips.Add(new Chip(e.X, e.ToY, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 225, 0, 0))));
                if (e.Color == 2)
                    chips.Add(new Chip(e.X, e.ToY, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 128, 0))));
            }
            async public void MakeOneMove(int coloumn)
            {
                if (!model.MakeOneMove(coloumn))
                {
                    ComputerIsThinking = System.Windows.Visibility.Collapsed;
                    OnPropertyChanged("ComputerIsThinking");
                    OnPropertyChanged("Color");
                    gameRecord.Clear();
                    foreach (string s in model.GameRecord)
                        gameRecord.Add(s);
                    ShowEndDialog(model.GetColorOfWinner());
                }
                else
                {
                    if ((model.IndexOfMover == 1 && humanIsPlayingAsRed) || (model.IndexOfMover == 2 && !humanIsPlayingAsRed))
                        ComputerIsThinking = System.Windows.Visibility.Collapsed;
                    if ((model.IndexOfMover == 2 && humanIsPlayingAsRed) || (model.IndexOfMover == 1 && !humanIsPlayingAsRed))
                    {
                        ComputerIsThinking = System.Windows.Visibility.Visible;
                    }
                    OnPropertyChanged("MaxValue");
                    OnPropertyChanged("ComputerIsThinking");
                    OnPropertyChanged("Color");
                    gameRecord.Clear();
                    foreach (string s in model.GameRecord)
                        gameRecord.Add(s);
                    if (model.FieldIsFull())
                    {
                        DrawWithoutDialog();
                        return;
                    }
                    if ((model.IndexOfMover == 1 && !humanIsPlayingAsRed) || (model.IndexOfMover == 2 && humanIsPlayingAsRed))
                        timer.Start();
                }
                OnPropertyChanged("ButtonsEnabled");
            }
            async public void Resign()
            {
                model.Resign();
                gameRecord.Clear();
                foreach (string s in model.GameRecord)
                    gameRecord.Add(s);
                ShowEndDialog(model.GetNameOfAnotherColor());
            }
            public event EventHandler Close;
            private void OnClose()
            {
                EventHandler close = Close;
                if (close != null)
                    close(this, new EventArgs());
            }
            async public void Draw()
            {
                System.Windows.MessageBox.Show("Предложение не принято!");
            }
            private void ShowEndDialog(string nameOfWinner)
            {
                throw new NotImplementedException();
                //MessageBoxResult result = MessageBox.Show(MessageBoxButton.)
                //MessageBox.Show("Победили " + nameOfWinner, "Игра завершена!",);
                //Dialog
                //    dialog.Commands.Add(new UICommand("Вернуться в меню"));
                //dialog.Commands.Add(new UICommand("Новая игра"));
                //dialog.Commands.Add(new UICommand("Закрыть"));
                //UICommand result = await dialog.ShowAsync() as UICommand;
                //if (result != null && result.Label == "Вернуться в меню")
                //    OnComeBack();
                //else if (result != null && result.Label == "Закрыть")
                //    OnClose();
                //else
                //    OnNewGame();
            }
            async public void DrawWithoutDialog()
            {
                model.Draw();
                gameRecord.Clear();
                foreach (string s in model.GameRecord)
                    gameRecord.Add(s);
                ShowEndDialog(" дружба");
            }
            public event EventHandler ComeBack;
            private void OnComeBack()
            {
                EventHandler comeBack = ComeBack;
                if (comeBack != null)
                    comeBack(this, new EventArgs());
            }
            public event EventHandler NewGame;
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (propertyChanged != null)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            private void OnNewGame()
            {
                EventHandler newGame = NewGame;
                if (newGame != null)
                    newGame(this, new EventArgs());
            }
            //IStorageFile saveFile;
            public async void SaveFile()
            {
            throw new NotImplementedException();
            //    if (saveFile == null)
            //    {
            //        FileSavePicker picker = new FileSavePicker
            //        {
            //            DefaultFileExtension = ".txt",
            //            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            //        };
            //        picker.FileTypeChoices.Add("Text File", new List<string>() { ".txt" });
            //        saveFile = await picker.PickSaveFileAsync();
            //        if (saveFile == null)
            //            return;
            //    }
            //    await FileIO.WriteLinesAsync(saveFile, model.GameRecord);
            //    await new MessageDialog("Запись партии сохранена.").ShowAsync();
        }
            
        }
}
