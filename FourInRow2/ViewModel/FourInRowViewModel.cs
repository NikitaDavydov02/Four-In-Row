using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
//using Windows.UI.Popups;
//using Windows.UI;
//using Windows.UI.Xaml.Media;
using System.ComponentModel;
//using Windows.Storage;
//using Windows.Storage.Pickers;
using FourInRow2.Model;
using FourInRow2.View;
using System.Windows.Media;

namespace FourInRow2.ViewModel
{
    class FourInRowViewModel:INotifyPropertyChanged
    {
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
        public System.Windows.Media.SolidColorBrush Color
        {
            get
            {
                if (model.color == 1)
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(225, 225, 0, 0));
                else
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 128, 0));
            }
        }
        public INotifyCollectionChanged GameRecord { get { return gameRecord; } }
        public ObservableCollection<string> gameRecord = new ObservableCollection<string>();
        public INotifyCollectionChanged Chips { get { return chips; } }
        private ObservableCollection<Chip> chips = new ObservableCollection<Chip>();
        private FourInRowModel model = new FourInRowModel();
        public FourInRowViewModel()
        {
            model.ChipChanged += ChipChangedEventHandler;
            //saveFile = null;
        }
        private void ChipChangedEventHandler(object sender, ChipChangedEventArgs e)
        {
            if (e.Color == 1)
                chips.Add(new Chip(e.X, e.ToY, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 225, 0, 0))));
            if (e.Color == 2)
                chips.Add(new Chip(e.X, e.ToY, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 128, 0))));
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
        public void MakeOneMove(int coloumn)
        {
            if (!model.MakeOneMove(coloumn))
            {
                OnPropertyChanged("Color");
                gameRecord.Clear();
                foreach (string s in model.GameRecord)
                    gameRecord.Add(s);
                ShowEndDialog(model.GetColorOfWinner());
            }
            else
            {
                OnPropertyChanged("Color");
                gameRecord.Clear();
                foreach (string s in model.GameRecord)
                    gameRecord.Add(s);
                if (model.FieldIsFull())
                {
                    DrawWithoutDialog();
                    return;
                }
            }
            OnPropertyChanged("ButtonsEnabled");
        }
        public void Resign()
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
            MessageBoxResult result = MessageBox.Show("Оба игрока согласны на ничью?", "", MessageBoxButton.YesNo);
            if (result != null && result == MessageBoxResult.Yes)
            {
                model.Draw();
                gameRecord.Clear();
                foreach (string s in model.GameRecord)
                    gameRecord.Add(s);
                ShowEndDialog(" дружба");
            }
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
        public async void SaveFile()
        {
            throw new NotImplementedException();
            //if (saveFile == null)
            //{
            //    FileSavePicker picker = new FileSavePicker
            //    {
            //        DefaultFileExtension = ".txt",
            //        SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            //    };
            //    picker.FileTypeChoices.Add("Text File", new List<string>() { ".txt" });
            //    saveFile = await picker.PickSaveFileAsync();
            //    if (saveFile == null)
            //        return;
            //}
            //await FileIO.WriteLinesAsync(saveFile, model.GameRecord);
            //await new MessageDialog("Запись партии сохранена.").ShowAsync();
        }
    }
}
