using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{

    /// <summary>
    /// A square definition, consisting of a position and a color
    /// The position can be either relative or absolute, depending on the context
    /// It is relative when it is part of the Piece and OrientedPiece classes
    /// It is absolute when it is part of the PosPiece class
    /// </summary>
    internal class Square
    {

        public Pos Position;
        public Colors Color;

        public Square(Pos position, Colors color)
        {
            this.Position = position;
            this.Color = color;
        }

    }
}
