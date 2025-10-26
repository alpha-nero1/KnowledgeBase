9.8 Pastebin: Design a system like Pastebin, where a user can enter a piece of text and get a randomly 
generated URL for public access. 

Well we can simply take the users input and generate a hash/guid to associate with that text. we then store it in a `Texts` table in the db. To track visits we can create a `TextVisits` table.

When users use the web app it shows the text given the guid is in the url.