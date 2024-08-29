available_orientation(1,0).
available_orientation(1,90).

available_orientation(2,0).
available_orientation(2,90).
available_orientation(2,m0).
available_orientation(2,m90).

available_orientation(3,0).
available_orientation(3,90).

available_orientation(4,0).
available_orientation(4,90).
available_orientation(4,180).
available_orientation(4,270).

available_orientation(5,X) :- orientation(X).
available_orientation(6,X) :- orientation(X).
available_orientation(7,X) :- orientation(X).
available_orientation(8,X) :- orientation(X).

available_orientation(9,0).
available_orientation(9,90).
available_orientation(9,180).
available_orientation(9,270).

available_orientation(10,0).

available_orientation(11,0).
available_orientation(11,90).
available_orientation(11,180).
available_orientation(11,270).

available_orientation(12,X) :- orientation(X).

available_orientation(13,0).
available_orientation(13,90).
available_orientation(13,180).
available_orientation(13,270).