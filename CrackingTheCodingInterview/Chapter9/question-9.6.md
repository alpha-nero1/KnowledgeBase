9.6 Sales Rank: A large eCommerce company wishes to list the best-selling products, overall and by 
category. For example, one product might be the #1056th best-selling product overall but the #13th 
best-selling product under "Sports Equipment" and the #24th best-selling product under "Safety:· 
Describe how you would design this system.

The most basic building block we need for this system to work would be to have an orders table.

The orders table may be a self referential table that links to itself with an order id and parent order id (just a technicality).

Each order record needs to have a structure akin to:

- ORDER - 
+ OrderId integer
+ ParentOrderId integer
+ ProductId integer
+ CustomerId integer
+ CreatedAt datetime
+ UpdatedAt datetime
+ DisabledAt datetime

Note the importance of the `ProductId` that would let us know exactly what product was ordered.

The product records might look like this:

-- PRODUCT --
+ ProductId integer
+ Name string
+ Description string
+ CategoryId int
+ CreatedAt datetime
+ UpdatedAt datetime
+ DisabledAt datetime
... any other relevant fields for the business

This way when orders start coming through, engineers can easily query for and display the product records with the highest frequency in the orders table

And when putting a category into the mix they can see how many orders are attached to products with that category id.

If you wanted the most popular for each category you could group by and get them that way.

given you now have an API for this, have fun and show it in a web app!

PS: the date fields give us flexibility around knowing how recently the products have been purchased, this way you can show trends or even what has most recently been selling really well.