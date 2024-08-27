using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{
    internal class Board
    {

        public int size;
        public int[,] data;  // 0 = empty ; >0 if piece id red ; <0 if piece id white

        // history of the board. the list of pieces placed on the board
        public List<PosPiece> placed = new List<PosPiece>();

        public Board(int size)
        {
            this.size = size;
            this.data = new int[size, size];
        }

        public Board(int size, int[,] data) : this(size)
        {
            this.data = (int[,])data.Clone();
        }

        public Board Clone()
        {
            var result = new Board(size,data);
            result.placed.AddRange(placed);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Board other = (Board)obj;
            if (size != other.size )
            {
                return false;
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (data[x, y] != other.data[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsInBounds(Pos pos)
        {
            return pos.x >= 0 && pos.x < size && pos.y >= 0 && pos.y < size;
        }

        public bool IsInBounds(OrientedPiece p, Pos placement)
        {
            foreach (Square s in p.Squares)
            {
                Pos pos = s.Position.Add(placement);
                if (!IsInBounds(pos))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsCorrectColor(OrientedPiece o, Pos placement)
        {
            foreach (Square s in o.Squares)
            {
                Pos pos = s.Position.Add(placement);

                // for even values of pos.x+pos.y, the color should be red
                // for odd values of pos.x+pos.y, the color should be white
                if ((pos.x + pos.y) % 2 == 0 && s.Color != Colors.Red)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsNotConflicting(PosPiece p)
        {
            foreach (Square s in p.Squares)
            {
                if (data[s.Position.x, s.Position.y] != 0)
                {
                    return false;
                }
            }
            return true;
        }


        public void PlacePiece(PosPiece p)
        {
            foreach (Square s in p.Squares)
            {
                if (data[s.Position.x, s.Position.y]>0)
                {
                    throw new Exception("Placing a piece on a square that is already occupied");
                }
                data[s.Position.x, s.Position.y] = s.Color== Colors.Red? p.id : -p.id;
            }
            placed.Add(p);
        }


        public void Display()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(data[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (var pp in placed)
            {
                Console.Write($"{pp.id} in {pp.Orientation.ToString()} at ({pp.BoardPosition.x},{pp.BoardPosition.y}) ");
            }
            Console.WriteLine();
        }

        public void DisplayAnswerSets()
        {
            foreach (var pp in placed)
            {
                Console.Write($"placed({pp.id},{pp.Orientation.ToString()},{pp.BoardPosition.x},{pp.BoardPosition.y}). ");
            }
            Console.WriteLine();
        }

        public string AsString()
        {
            var datastr = "";
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    datastr += data[x, y] + ",";    
                }
            }
            return $"{size};{datastr}";
        }



    }
}
