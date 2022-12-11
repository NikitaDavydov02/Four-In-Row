using System;
using System.Collections.Generic;
using System.Text;

namespace FourInRow2.Model
{
    class VirtualBoard
    {
        //Properties;
        //Int in stacks: 0 - empty cell, 1 - red chip, 2 - green chip;
        private int[,] field;
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
        //Constructor;
        public VirtualBoard()
        {
            rememberedPositions = new List<int[,]>();
            remeberedIndexes = new List<int>();
            IndexOfMover = 1;
            field = new int[7, 6];
            for (int i = 0; i < 7; i++)
                for (int a = 0; a < 6; a++)
                    field[i, a] = 0;
            IndexOfWinner = 0;
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
                }
                else
                {
                    IndexOfMover = 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private List<int> remeberedIndexes;
        public List<int[,]> rememberedPositions;
        public void RememberPosition()
        {
            remeberedIndexes.Add(IndexOfMover);
            int[,] board = new int[7, 6];
            for (int i = 0; i < 7; i++)
                for (int a = 0; a < 6; a++)
                    board[i, a] = field[i, a];
            rememberedPositions.Add(board);
        }
        public void ComeBack(int deepOfPosition)
        {
            IndexOfMover = remeberedIndexes[deepOfPosition - 1];
            remeberedIndexes.RemoveAt(deepOfPosition - 1);
            field = rememberedPositions[deepOfPosition - 1];
            rememberedPositions.RemoveAt(deepOfPosition - 1);
        }
        public string GetNameOfAnotherColor()
        {
            if (color == 1)
                return " зелёные";
            else
                return " красные";
        }
        public List<int> FindEnableColoumns()
        {
            List<int> listToReturn = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                bool thisColoumnHasAlreadyInclude = false;
                for (int a = 0; a < 6; a++)
                {
                    if (field[i, a] == 0 && !thisColoumnHasAlreadyInclude)
                    {
                        listToReturn.Add(i + 1);
                        thisColoumnHasAlreadyInclude = true;
                    }
                }
            }
            return listToReturn;
        }
        public bool CheckTheGame()
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
        public int IndexOfWinner { get; private set; }
        private string incompleteLine;
        private bool AddChipToColoumn(int coloumn)
        {
            for (int i = 0; i < 6; i++)
            {
                if (field[coloumn - 1, i] == 0)
                {
                    field[coloumn - 1, i] = color;
                    return true;
                }
            }
            return false;
        }
        public void UpdateField(int[,] newField)
        {
            field = newField;
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
    }
}
