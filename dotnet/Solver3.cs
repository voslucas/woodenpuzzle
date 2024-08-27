using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{
    internal class Solver3
    {


        //board index is : x+ y * board.width

        // in this solver we keep a list of all pospiece for each position on the board (based on boardindex)
        // the idea is that for each (x,y) position on the board, we have a list of all possible pieces that can be placed there.


        private List<PosPiece>[] pieceListArr;
        private int maxIndex;
        private bool outputEnabled = false;
        private int solutionCount = 0;
        private List<string> uniqueBoardString = new List<string>();

        public Solver3(Board b, bool outputEnabled = false)
        {
            maxIndex = b.size * b.size;
            pieceListArr = new List<PosPiece>[maxIndex];
            this.outputEnabled = outputEnabled;
        }


        public void Solve(Board b, List<OrientedPiece> orientedPieces)
        {

            // First we generate all possible positions for each piece based on the oriented pieces.
            List<PosPiece> allPosPieces= new List<PosPiece>();
            foreach (var op in orientedPieces)
            {
                for (int x = 0; x < b.size; x++)
                {
                    for (int y = 0; y < b.size; y++)
                    {
                        Pos pos = new Pos(x, y);
                        if (b.IsInBounds(op, pos) && b.IsCorrectColor(op, pos))
                        {
                            allPosPieces.Add(new PosPiece(op, pos));
                        }
                    }
                }
            }

            // Now we generate the list of pospieces for each position on the board that could 'reach' that position with one of its squares.
            for (int x = 0; x < b.size; x++)
            {
                for (int y = 0; y < b.size; y++)
                {

                    Pos pos = new Pos(x, y);
                    int index = x + y * b.size;
                    foreach (var pp in allPosPieces)
                    {
                        if (pp.HasASquareOnPos(pos))
                        {
                            if (pieceListArr[index] == null)
                            {
                                pieceListArr[index] = new List<PosPiece>();
                            }
                            pieceListArr[index].Add(pp);
                        }
                    }
                }
            }
            solutionCount = 0;
            uniqueBoardString.Clear();
            Step(0, b.Clone(), new int[] { } );


            Console.WriteLine($"Found {solutionCount} solutions on {uniqueBoardString.Count} unique boards");
          
        }

        public bool Step(int curIndex, Board b, int[] alreadyPlaced)
        {

            int c = 0;

            foreach (var pp in pieceListArr[curIndex])
            {
                if (alreadyPlaced.Contains(pp.id)) continue;
               
                if (b.IsNotConflicting(pp))
                {
                    Board newBoard = b.Clone();
                    newBoard.PlacePiece(pp);
                    //find new empty spot on the newBoard
                    int newCurIndex = curIndex;
                    while (newCurIndex < maxIndex && 
                           newBoard.data[newCurIndex % newBoard.size, newCurIndex / newBoard.size] != 0)
                    {
                        newCurIndex++;
                    }

                    //is this a solutions?
                    if (newCurIndex == maxIndex)
                    {
                        newBoard.DisplayAnswerSets();
                        if (!uniqueBoardString.Contains(newBoard.AsString())) {
                            uniqueBoardString.Add(newBoard.AsString());
                        }
                        solutionCount++; 
                        return true;
                    } else
                    //nope, continue our search
                    {
                        //keep track of the pieces we have placed
                        var newplaced = new int[alreadyPlaced.Length + 1];
                        alreadyPlaced.CopyTo(newplaced, 0);
                        newplaced[alreadyPlaced.Length] = pp.id;

                        //go on step deeper
                        Step(newCurIndex, newBoard, newplaced);
                    }
                }   
            }

            return true;
        }

    }
}
