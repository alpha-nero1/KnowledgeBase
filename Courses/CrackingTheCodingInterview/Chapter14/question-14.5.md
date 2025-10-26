# 14.5. Denormalisation
What is denormalisation? Explain the pros and cons.

Ideally in a relational database we would have fully normalised models, this means that data is organised in a very object oriented way and that there is little to no duplication.

The lack of data duplication means that we can sleep easy knowing that our singular update to the database was all we needed to do and that there are no extra/miscellaneous data points to change.

So for example we may have a `States` table with columns `Id`, `Name`, `Population`. But we want to know what country this state is a part of. In this case we have a seperate `Countries` table representing the country objects with their related ids. One example row might be (1, `Australia`, `Au`). Given the normalised database model, instead of storing the country on the state table directly, we store the `countryId` acting as a reference to where our data will be.

Now talking about denormalisation, instead of having well structured and non-duplicated data across the database, we can instead elect to have certain denormalised properties on a table like so (back to the state example):
`Id, Name, Population, Country, Continent`

Here are the pros and cons:

### CONS
+ duplicate data because `Country` exists here and in the `Countries` table, meaning extra updates would need to be done to keep the DB in sync.
+ bloated list of columns on the table, this may be confusing as to what the true purpose of the table is.
+ On large scales this can cause disarray as it is impossible to determine what the source of truth is.
+ Data redundancy can lead to larger database storage needs.

### PROS
+ This is really really good for searching. The main benefit of having a denormalised table is that when searching through a denormalised table, we never need to do any of those expensive joins to get extra data, instead we have the column right away. In some personal cases this doubled the efficiency of database queries!