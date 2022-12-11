using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace FourInRow2.Model
{
    class PlayWithComputerModel
    {
        //Properties;
        //Int in stacks: 0 - empty cell, 1 - red chip, 2 - green chip;
        public int[,] field { get; private set; }
        //Int in IndexOfMover: 1 - red; 2 - green;
        public int IndexOfMover { get; private set; }
        private VirtualBoard virtualBoard = new VirtualBoard();
        private int indexOfComputer;
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
        public PlayWithComputerModel(int indexOfComputer)
        {
            this.indexOfComputer = indexOfComputer;
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
        private int FindMaxDeep()
        {
            if (21 - GameRecord.Count >= 5)
                return 5;
            else
                return 21 - GameRecord.Count;
        }
        public int ToThinkAboutMove(int maxDeep)
        {
            return ToFindTheMostPowerfullMove(maxDeep);
        }
        private int ToFindTheMostPowerfullMove(int maxDeep)
        {
            if (GameRecord.Count == 0)
                return 4;
            List<int> freeColoumns = virtualBoard.FindEnableColoumns();
            Dictionary<int, double> assessmentOfMoves = new Dictionary<int, double>();
            foreach (int variantOfMove in freeColoumns)
            {
                double assessment = ToAssesMove(variantOfMove, maxDeep, 1);
                assessmentOfMoves.Add(variantOfMove, assessment);
            }
            double theMostHightAssessment = -1;
            foreach (double assessment in assessmentOfMoves.Values)
            {
                if (assessment > theMostHightAssessment)
                    theMostHightAssessment = assessment;
            }
            foreach (int move in assessmentOfMoves.Keys)
            {
                if (assessmentOfMoves[move] == theMostHightAssessment)
                {
                    return move;
                }
            }
            return -1;
        }
        private double ToAssesMove(int move, int maxDeep, int currentDeep)
        {
            virtualBoard.RememberPosition();
            virtualBoard.MakeOneMove(move);
            if (!virtualBoard.CheckTheGame() && virtualBoard.IndexOfMover == indexOfComputer)
            {
                virtualBoard.ComeBack(currentDeep);
                return 1 / (currentDeep * 0.6);
            }
            if (!virtualBoard.CheckTheGame() && virtualBoard.IndexOfMover != indexOfComputer)
            {
                virtualBoard.ComeBack(currentDeep);
                return -1 / (currentDeep * 0.6);
            }
            if (virtualBoard.FieldIsFull() || currentDeep == maxDeep)
            {
                virtualBoard.ComeBack(currentDeep);
                return 0;
            }
            List<int> freeColoumns = virtualBoard.FindEnableColoumns();
            Dictionary<int, double> assessmentsOfMoves = new Dictionary<int, double>();
            foreach (int coloumn in freeColoumns)
            {
                double assessment = ToAssesMove(coloumn, maxDeep, currentDeep + 1);
                assessmentsOfMoves.Add(coloumn, assessment);
            }
            //.
            virtualBoard.ComeBack(currentDeep);
            double amount = 0;
            foreach (double assess in assessmentsOfMoves.Values)
            {
                amount += assess;
            }
            double arithmeticMean = amount / assessmentsOfMoves.Keys.Count;
            return arithmeticMean;
        }
        public bool MakeOneMove(int coloumn)
        {
            if (!AddChipToColoumn(coloumn))
                return true;
            virtualBoard.MakeOneMove(coloumn);
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
                        incompleteLine += (GameRecord.Count + 1) + "." + " ";
                        incompleteLine += coloumn.ToString() + " ";
                    }
                    else
                    {
                        incompleteLine += coloumn.ToString() + " ";
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
}
