# Wooden Puzzle Solver

This is an C# implementation. 

# Semantics

we distinct 3 types of pieces
- piece
- orientedpiece
- pospiece


The `pieces` is a definition : with an id and the squares in their relative positions.  The pieces are loaded/parsed from the `ASP` definition file `13p-definitions.lp`.

The `orientedpiece` is a piece, but already with an orientation (0,80,190,270) and mirroring option associated to it. The sqaures of a orientedpiece are already precalculated to this orientation. The `OrientedPieceBuilder` constructs the orientedpieces based on the piece and all available orientation that are detected using some symmetry detection. Finally, a `pospiece` is even further specialized where the piece is already in a given board position (it is not placed on a board though), and orientation, where the squares of the pieces are already calculated into board position, instead of relative positions. 

# Solving

We precalculate, for each position (x,y) which pospiece would have a square on that position. 

We used this precalculated list , to scan for the first free spot on the board (from upperleft, left to right, to bottom) and place an available unused piece from the precalculated list, along with backtracking. see `solver3.cs` 



