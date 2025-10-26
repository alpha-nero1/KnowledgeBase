# 14.4. Joins
What are the different types of joins? Please explain how they differ and why certain types
are better in certain situations.

A JOIN in SQL is the primary way to merge two tables together.
The JOIN keyword on its own would default to a 'INNER JOIN' where
if the row is not present in the right table, the row is not returned.
Here are the different joins explained.

## LEFT JOIN
Othwerwise known as a `LEFT OUTER JOIN`, LEFT JOIN takes the left table as the base table to join rows onto, if the row is found in
the left table but not in the right then the right side of the row will still be included but
will be all nulls.

## RIGHT JOIN
Exactly same as LEFT JOIN however the right table is the base table. Also known as `RIGHT OUTER JOIN`

## FULL OUTER JOIN
Combines the results of the left and right table and if any row is missing, then full null rows are provided.

## INNER JOIN
The rows shown would be that where the condition matched and no full null rows would return.