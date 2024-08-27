using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{
    internal class PieceLoader
    {

        /// <summary>
        /// Load pieces from a file with the following format:
        /// piece(1,0,0,red).
        /// piece(1,1,0, white).
        /// piece(2,0,0, white).
        /// piece(2,0,1, red).
        /// Where the first number is the piece number, the second number is the relative x position of the square, the third number is the relative y position of the sqaure, 
        /// and the fourth number is the color.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<Piece> LoadPieces(string filename)
        { 
            var pieces = new List<Piece>();
            var lines = System.IO.File.ReadAllLines(filename);
            foreach (var line in lines)
            {

                //if the line is empty, just skip it
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                //if the line starts with % , it is a comment, so skip it
                if (line.StartsWith("%"))
                {
                    continue;
                }

                var parts = line.Split(',');
                var id = int.Parse(parts[0].Substring(6));
                var x = int.Parse(parts[1]);
                var y = int.Parse(parts[2]);

                // part[3] is followed by a ).  We need to remove the ) from the end of the string
                
                var color = parts[3].Trim().ToLower().StartsWith('r') ? Colors.Red : Colors.White;
                var piece = pieces.FirstOrDefault(p => p.id == id);
                if (piece == null)
                {
                    piece = new Piece { id = id, Squares = new List<Square>() };
                    pieces.Add(piece);
                }
                piece.Squares.Add(new Square(new Pos(x,y), color));
            }
            return pieces;


        }


    }
}
