using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{
    internal class OrientedPieceComparer
    {

        private int boardsize;
        public OrientedPieceComparer(int boardsize)
        {
            this.boardsize = boardsize;
        }

        public bool IsSymmetric(OrientedPiece op1, OrientedPiece op2)
        {

            if (op1.id != op2.id) { return false; }

            if (op1.Orientation == op2.Orientation) { return true; }

            //two oriented pieces are similar , if their square representations are equal by checking all positions.
            //for example : a rod of length 4 and a 180 degree rotated rod, are similar but
            //can differ in translation among the x and y coords
            //so we place them in the (0,0) corner of a board and compare the boards.

            var x1 = op1.Squares.Min(s => s.Position.x);
            var y1 = op1.Squares.Min(s => s.Position.y);
            
            var b1 = new Board(boardsize);
            b1.PlacePiece(new PosPiece(op1, new Pos(-x1, -y1)));

            var x2 = op2.Squares.Min(s => s.Position.x);
            var y2 = op2.Squares.Min(s => s.Position.y);

            var b2 = new Board(boardsize);
            b2.PlacePiece(new PosPiece(op2, new Pos(-x2, -y2)));

            return b1.Equals(b2);


        }

       

    }
}
