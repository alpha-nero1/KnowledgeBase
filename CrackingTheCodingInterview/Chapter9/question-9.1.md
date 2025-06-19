## Stock data.
To build a system that accepts 1000 connections for clients to get simple end of day stock data we could potentially do the following.

## Django + Pandas solution
We could build a simple django web project that leverages pandas to read in the CSV files with the information in them from an S3 bucket.
Once we have the CSV data from S3 we can then do ANYTHING we want with it because pandas is such a great DataScience/Machine Learning tool
we can actually use pandas to easily and efficiently query this data for the user.

Pandas and Seaborn would also allow us to easily display the data in tables and graphs.