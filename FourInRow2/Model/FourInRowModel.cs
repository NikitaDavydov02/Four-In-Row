using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInRow2.Model
{
    class FourInRowModel
    {
        //Properties;
        //Int in stacks: 0 - empty cell, 1 - red chip, 2 - green chip;
        public int[,] field { get; private set; }
        //Int in IndexOfMover: 1 - red; 2 - green;
        public int IndexOfMover { get; private set; }
        public int color
        {
            get
            {
                if (IndexOfMover == 1)
                    return 1;
                else
                    return 2;
            }
        }
        public List<string> GameRecord { get; private set; }
        //Constructor;
        public FourInRowModel()
        {
            IndexOfMover = 1;
            GameRecord = new List<string>();
            field = new int[7, 6];
            for (int i = 0; i < 7; i++)
                for (int a = 0; a < 6; a++)
                    field[i, a] = 0;
        }
        //Event Handlers;
        public EventHandler<ChipChangedEventArgs> ChipChanged;
        private void OnChipChanged(ChipChangedEventArgs args)
        {
            EventHandler<ChipChangedEventArgs> chipChanged = ChipChanged;
            if (chipChanged != null)
                chipChanged(this, args);
        }
        public bool MakeOneMove(int coloumn)
        {
            if (!AddChipToColoumn(coloumn))
                return true;
            if (CheckTheGame())
            {
                if (IndexOfMover == 1)
                {
                    IndexOfMover = 2;
                    GameRecord.Add(incompleteLine);
                }
                else
                {
                    IndexOfMover = 1;
                    GameRecord.RemoveAt(GameRecord.Count - 1);
                    GameRecord.Add(incompleteLine);
                    incompleteLine = "";
                }
                return true;
            }
            else
            {
                if (IndexOfMover == 1)
                {
                    GameRecord.Add(incompleteLine + "# [1 - 0]");
                }
                else
                {
                    GameRecord.RemoveAt(GameRecord.Count - 1);
                    GameRecord.Add(incompleteLine + "# [0 - 1]");
                }
                return false;
            }
        }
        public void Draw()
        {
            GameRecord.RemoveAt(GameRecord.Count - 1);
            GameRecord.Add(incompleteLine + " [1/2 - 1/2]");
        }
        public string GetNameOfAnotherColor()
        {
            if (color == 1)
                return " зелёные";
            else
                return " красные";
        }
        private bool CheckTheGame()
        {
            for (int x = 0; x < 7; x++)
                for (int y = 0; y < 6; y++)
                {
                    //Horizontal check;
                    if (x < 4)
                    {
                        if ((field[x, y] == field[x + 1, y]) && (field[x + 2, y] == field[x + 3, y]) && (field[x + 1, y] == field[x + 2, y]) && (field[x, y] != 0))
                            return false;
                    }
                    //Vertical check;
                    if (y < 3)
                    {
                        if ((field[x, y] == field[x, y + 1]) && (field[x, y + 2] == field[x, y + 3]) && (field[x, y + 1] == field[x, y + 2]) && (field[x, y] != 0))
                            return false;
                    }
                    //Diagonal right-bottom check;
                    if (x < 4 && y > 2)
                    {
                        if ((field[x, y] == field[x + 1, y - 1]) && (field[x + 2, y - 2] == field[x + 3, y - 3]) && (field[x + 1, y - 1] == field[x + 2, y - 2]) && (field[x, y] != 0))
                            return false;
                    }
                    //Diagonal right-top check;
                    if (x < 4 && y < 3)
                    {
                        if ((field[x, y] == field[x + 1, y + 1]) && (field[x + 2, y + 2] == field[x + 3, y + 3]) && (field[x + 1, y + 1] == field[x + 2, y + 2]) && (field[x, y] != 0))
                            return false;
                    }
                }
            return true;
        }
        private string incompleteLine;
        private bool AddChipToColoumn(int coloumn)
        {
            for (int i = 0; i < 6; i++)
            {
                if (field[coloumn - 1, i] == 0)
                {
                    field[coloumn - 1, i] = color;
                    if (IndexOfMover == 1)
                    {
                        incompleteLine += (GameRecord.Count + 1) + "." + "  ";
                        incompleteLine += coloumn.ToString() + "   ";
                    }
                    else
                    {
                        incompleteLine += coloumn.ToString() + "  ";
                    }
                    OnChipChanged(new ChipChangedEventArgs((coloumn * 90) - 90, ((6 - i) * 90) - 90, color));
                    return true;
                }
            }
            return false;
        }
        public string GetColorOfWinner()
        {
            if (IndexOfMover == 1)
                return " красные!";
            else
                return " зелёные!";
        }
        public bool FieldIsFull()
        {
            for (int i = 0; i < 7; i++)
                for (int a = 0; a < 6; a++)
                {
                    if (field[i, a] == 0)
                        return false;
                }
            return true;
        }
        public void Resign()
        {
            if (IndexOfMover == 1)
                GameRecord.Add(incompleteLine + " [0 - 1]");
            else
            {
                GameRecord.RemoveAt(GameRecord.Count - 1);
                GameRecord.Add(incompleteLine + " [1 - 0]");
            }
        }
    }
    class ChipChangedEventArgs : EventArgs
    {
        public double X { get; private set; }
        public double ToY { get; private set; }
        public int Color { get; private set; }
        public ChipChangedEventArgs(double x, double toY, int color)
        {
            X = x;
            ToY = toY;
            Color = color;
        }
    }
}
