using System.Diagnostics;
using System.Drawing;

namespace WoodenPuzzleSolver
{
    public  class Program
    {

        public const int boardSize = 8;
        public const int maxThreads = 8;

        static void Main(string[] args)
        {

            //load the 13 pieces
            var pl = new PieceLoader();
            var pieces = pl.LoadPieces("..//..//..//..//13p-definitions.lp");


            //get the max orientation depth from the first args parameter
            int maxOrientationDepth = 0;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out maxOrientationDepth);
            }


            //build list of oriented pieces
            var opb = new OrientedPieceBuilder(pieces);

            
            var orientedPieces = opb.GetSmart(boardSize, allowMirroring: true);
            //var orientedPieces = opb.GetByDepth(maxOrientationDepth, allowMirroring: false);

            //display the list of selected oriented pieces
            foreach (var op in orientedPieces)
            {
                Console.WriteLine($"{op.id} {op.Orientation}");
            }
            Console.WriteLine("Total oriented pieces: " + orientedPieces.Count);


            Stopwatch sw = new Stopwatch();
            sw.Restart();

          
            var solver3 = new Solver3(new Board(boardSize));
            solver3.Solve(new Board(boardSize), orientedPieces);

            sw.Stop();
            Console.WriteLine("Solver3 took: " + sw.ElapsedMilliseconds + " ms");


        }
    }
}
