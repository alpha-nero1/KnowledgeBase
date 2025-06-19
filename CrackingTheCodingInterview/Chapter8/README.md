# Recursion and Dynamic Programming.

A good hint that a problem is recursive is that "it can be built off of sub-problems"
Like if you hear the question begin with:
- Design an algorithm to calculate the nth..
- Write code to list the first n...
- Implement a method to compute all...

## Approaching the issue.
- Bottom up approach: Build the solution from a starting point.
- Top-down approach: We divide the solution by n steps.
- Half and half approach: Divide the dataset in half.

## Recursive vs Iteraitve.
- Recursive algorithms can be very space inefficient because each call adds a call to the stack. meaning if the dataset is large it could be better to use iteration instead.

## Dynamic programming and Memoization.
- Dynamic programming is taking a recursive problem and finding the overlapping sub-problems. you cache these results for later recursive calls.
- A simple example of dynamic programming is "Finding the nth fibonaci number"

Memoization: Remembering calculations for later use.
