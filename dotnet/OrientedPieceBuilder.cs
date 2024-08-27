using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenPuzzleSolver
{

    /// <summary>
    /// The builder can generate two types of orientedpiece lists. 
    /// One is for the Depth experiment . It will generate all oriented pieces for each piece up to a certain depth, but without mirror.
    /// Ons is for the Main experiment. IT will detect all available orientations automaticly by a quick symmetry check
    /// </summary>
    internal class OrientedPieceBuilder
    {

        private List<Piece> pieces;

        public OrientedPieceBuilder(List<Piece> pieces)
        {
            this.pieces = pieces;
        }

        public List<OrientedPiece> GetByDepth(int maxdepth, bool allowMirroring = false)
        {
            var result = new List<OrientedPiece>();
            foreach (var piece in pieces)
            {
                //based on the max orientation depth, we will either create a list of all oriented pieces or only the first orientation.
                if (piece.id <= maxdepth)
                {
                    foreach (var orientation in Enum.GetValues<Orientations>())
                    {
                        if (!allowMirroring) //skip mirrored orientations
                        {
                            if (orientation == Orientations.mo0 ||
                                orientation == Orientations.mo90 ||
                                orientation == Orientations.mo180 ||
                                orientation == Orientations.mo270)
                            {
                                continue;
                            }
                        }
                        var op = new OrientedPiece(piece, (Orientations)orientation);
                        result.Add(op);
                    }
                }
                else
                {
                    var op = new OrientedPiece(piece, (Orientations)Orientations.o0);
                    result.Add(op);
                }
            }
            return result;
        }

        public List<OrientedPiece> GetSmart(int boardsize, bool allowMirroring=false)
        {

            var result = new List<OrientedPiece>();

            foreach (var piece in pieces)
            {
                result.AddRange(GetSmartSinglePiece(piece, boardsize, allowMirroring));
            }

            return result;  
        }


        private List<OrientedPiece> GetSmartSinglePiece(Piece piece, int boardsize, bool allowMirroring)
        {
            var comparer = new OrientedPieceComparer(boardsize);

            var result = new List<OrientedPiece>();

            //Step 1 : build a list of all oriented pieces for each piece
            var tmp = new List<OrientedPiece>();
            foreach (var orientation in Enum.GetValues<Orientations>())
            {
                if (!allowMirroring) //skip mirrored orientations
                {
                    if (orientation == Orientations.mo0 ||
                        orientation == Orientations.mo90 ||
                        orientation == Orientations.mo180 ||
                        orientation == Orientations.mo270)
                    {
                        continue;
                    }
                }
                var op = new OrientedPiece(piece, (Orientations)orientation);
                tmp.Add(op);
            }



            //Step 2 : always add the first orientation
            result.Add(tmp[0]);

            //Step 3 : for each other orientation, add it only if it is not a mirror of an already added orientation
            for (int i = 1; i < tmp.Count; i++)
            {
                var totest = tmp[i];
                //we need to test it against all already added oriented pieces
                bool add = true;
                foreach (var added in result)
                {
                    if (comparer.IsSymmetric(added, totest))
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    result.Add(totest);
                }
            }
            return result;
        }
    }
}
