## Chess Test
" We have the following method used in a chess game: boolean canMoveTo(int x, 
int y). This method is part of the Piece class and returns whether or not the piece can move to 
position (x, y). Explain how you would test this method. "

In this case we need to cover two types of testing:

## Extreme case validation
The function should handle extreme/bad inputs gracefully.
Check for:
- negative values for x and y.
- test with x and y out of bounds.
- test with a completely full board.
- test in an empty board.
- test with far more black peices than white and vice versa.

For these test cases I would prefer return false because
exceptions are quite heavy. false just means you cannot make the move.

## General testing
It may be worth creating a paramater grid mapping pieces to a series of arguments.
Passed into a function this should test a good set of scenarios.
We should at least test each single chess piece (there are 6)