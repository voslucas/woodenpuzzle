using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{

    /// <summary>
    /// A piece that has been rotated to a certain orientation
    /// The squares are already rotated according to this new orientation! 
    /// </summary>
    internal class OrientedPiece : Piece
    {

        public Orientations Orientation;

        public OrientedPiece(Piece piece, Orientations orientation)
        {
            this.id = piece.id;
            this.Orientation = orientation;

            this.Squares = new List<Square>();
            foreach (Square s in piece.Squares)
            {
                //rotate the squares according to the orientation
                Pos newPos;
                switch (orientation)
                {
                    case Orientations.o0:
                        newPos = new Pos(s.Position.x, s.Position.y);
                        break;
                    case Orientations.o90:
                        newPos = new Pos(-s.Position.y, s.Position.x);
                        break;
                    case Orientations.o180:
                        newPos = new Pos(-s.Position.x, -s.Position.y);
                        break;
                    case Orientations.o270:
                        newPos = new Pos(s.Position.y, -s.Position.x);
                        break;

                    case Orientations.mo0:
                        newPos = new Pos(-s.Position.x, s.Position.y);
                        break;
                    case Orientations.mo90:
                        newPos = new Pos(-s.Position.y, -s.Position.x);
                        break;
                    case Orientations.mo180:
                        newPos = new Pos(s.Position.x, -s.Position.y);
                        break;
                    case Orientations.mo270:
                        newPos = new Pos(s.Position.y, s.Position.x);
                        break;

                    default:
                        throw new Exception("Invalid orientation");
                }

                this.Squares.Add(new Square(newPos, s.Color));
            }

        }




    }
}
