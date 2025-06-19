# Testing a Real world object.
The following details key testing steps.

## Things to understand.
1. Who will use it and why?
2. What are the use cases?
3. What are the bounds of use?
4. What are the stress/failure conditions? (when it may be necessary for the product to fail)
5. How would you perform testing?
"For example, if you 
need to make sure a chair can withstand normal usage for five years, you probably can't actually place it in a 
home and wait five years. Instead, you'd need to define what"normal" usage is (How many "sits" per year on 
the seat? What about the armrest?)"

## Define the test cases.
In general you shoudl think about the following test cases.
1. The normal case given predictable inputs.
2. The extremes (what if you pass empty array or one element array, or very large one).
3. Nulls and illegal inputs.
4. Strange inputs (like an already sorted array or reveresed array?)

## Define the exepected results.
## Write test code.

# What about troubleshooting?

## Step 1: Understand the scenatio.
Ask the user everything.

## Step 2: Break down the problem.
Break down the flow into key parts where you could isolate the problem in a part (windows start menu > chrome icon > browser instance starts > browser loads settings > browser issues http requests for home page > browser receives response > browser pardes web page > displays content)

## Step 3: Create specific isolated tests.
Come up with tests to test those core parts.