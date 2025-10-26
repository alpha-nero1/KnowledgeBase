UPDATE Requests 
SET Status = 'Closed'
-- You will need to use subqueries for UPDATE statements.
WHERE AptID IN (SELECT AptID FROM Apartments WHERE BuildingId = 11);