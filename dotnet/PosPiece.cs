using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{

    /// <summary>
    /// Represents a oriented piece on the board with a board position
    /// The square position are precalculated to contain the absolute board positions.
    /// </summary>
    internal class PosPiece : Piece
    {
        public Pos BoardPosition;
        public Orientations Orientation;


        public PosPiece(OrientedPiece op, Pos boardPosition)
        {
            this.id = op.id;
            this.Orientation = op.Orientation;
            this.BoardPosition = boardPosition;

            //the square should be updated 
            this.Squares = new List<Square>();
            foreach (Square s in op.Squares)
            {
                this.Squares.Add(new Square(s.Position.Add(boardPosition), s.Color));
            }
        }

        public bool HasASquareOnPos(Pos checkPos)
        {
            foreach (Square s in Squares)
            {
                if (s.Position.Equals(checkPos))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
