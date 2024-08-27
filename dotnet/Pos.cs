using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{
    internal struct Pos
    {

        public int x;
        public int y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Pos Add(Pos other)
        {
            return new Pos(x + other.x, y + other.y);
        }

        public Pos Subtract(Pos other)
        {
            return new Pos(x - other.x, y - other.y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Pos other = (Pos)obj;
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }   
    }
}
