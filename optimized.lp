
#const size=8.
%const depth=2.   %You would normally set this as an argument to clingo 

coord(0..size-1).

% Define the orientations (0, 90, 180, 270 degrees).
orientation(0).
orientation(90).
orientation(180).
orientation(270).

% Option to also define the mirrored orientations.
orientation(m0).
orientation(m90).
orientation(m180).
orientation(m270).

#include "13p-definitions.lp".

pieces(P) :- piece(P,_,_,_).
colors(C) :- piece(_,_,_,C).

% We set the 0-degree orientation for each piece as the default.
available_orientation(P,0) :- pieces(P).

% Choose between either depth based solutions (read the paper).
% For the depth based you should disable the mirrored orientations above! 
available_orientation(P,X) :- P=1..depth, orientation(X).
% Or to load all logical orientations.
% include "13p-definitions.lp".

pos_color(X,Y, red) :- coord(X) , 
                       coord(Y) , 
                       (X + Y) = 2*Z, 
                       Z = (X + Y) / 2.

pos_color(X,Y, white) :- coord(X) , coord(Y) ,
                         not pos_color(X,Y, red).


% Define rotation matrices for each orientation.
rotation(0, 1, 0, 0, 1).   % No rotation: (dx, dy) -> (dx, dy)
rotation(90, 0, -1, 1, 0). % 90 degrees: (dx, dy) -> (-dy, dx)
rotation(180, -1, 0, 0, -1). % 180 degrees: (dx, dy) -> (-dx, -dy)
rotation(270, 0, 1, -1, 0). % 270 degrees: (dx, dy) -> (dy, -dx)

% Optionally, also define mirrored rotation matrices for each orientation.
rotation(m0,  -1, 0, 0, 1).   % No rotation: (dx, dy) -> (dx, dy)
rotation(m90,  0,-1,-1, 0). % 90 degrees: (dx, dy) -> (-dy, dx)
rotation(m180, 1, 0, 0,-1). % 180 degrees: (dx, dy) -> (-dx, -dy)
rotation(m270, 0, 1, 1, 0). % 270 degrees: (dx, dy) -> (dy, -dx)


outside_board(P, O, X, Y) :- available_orientation(P, O), 
                             coord(X), coord(Y),
                             rotation(O, A, B, _, _),
                             piece(P, DX, DY, _),
                             SX = X  + A*DX + B*DY,
                             SX < 0.
                       
outside_board(P, O, X, Y) :- available_orientation(P, O), 
                             coord(X), coord(Y),
                             rotation(O, A, B, _, _),
                             piece(P, DX, DY,_),
                             SX = X  + A*DX + B*DY ,
                             SX > (size-1).

outside_board(P, O, X, Y) :- available_orientation(P, O), 
                             coord(X), coord(Y),
                             rotation(O, _, _, C, D),
                             piece(P, DX, DY,_),
                             SY = Y + C*DX + D*DY,
                             SY <  0.
                       
outside_board(P, O, X, Y) :- available_orientation(P, O), 
                             coord(X), coord(Y),
                             rotation(O, _, _, C, D),
                             piece(P, DX, DY,_),
                             SY = Y + C*DX + D*DY,
                             SY > (size-1). 
            

good_color(P, O, X, Y) :-  available_orientation(P, O), 
                            coord(X), coord(Y),
                            piece(P, DX, DY, COLOR),  
                            rotation(O, A, B, C, D),
                            SX = X + A*DX + B*DY,
                            SY = Y + C*DX + D*DY,
                            pos_color(SX, SY, COLOR).

available_placing(P, O, X, Y) :- available_orientation(P, O), 
                                 coord(X), 
                                 coord(Y), 
                                 good_color(P, O, X, Y),
                                 not outside_board(P, O, X, Y).


1 { placed(P, O, X, Y) : available_placing(P, O, X, Y) } 1 :- pieces(P).

:- placed(P1,O1,X1,Y1), 
   placed(P2,O2,X2,Y2), 
   P1 != P2,
   piece(P1, DX1, DY1, COL1),
    rotation(O1, A1, B1, C1, D1),
      SX1 = X1 + A1*DX1 + B1*DY1,
      SY1 = Y1 + C1*DX1 + D1*DY1,
   piece(P2, DX2, DY2, COL2),
    rotation(O2, A2, B2, C2, D2),
      SX2 = X2 + A2*DX2 + B2*DY2,
      SY2 = Y2 + C2*DX2 + D2*DY2, 
   SX1 = SX2, SY1 = SY2.



#show placed/4.
