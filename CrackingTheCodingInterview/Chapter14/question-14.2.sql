SELECT bs.BuildingName, bs.BuildingID, COUNT(rqs.*) FROM Buildings bs
    LEFT JOIN Apartments apts ON apts.BuildingID = bs.BuildingID
    LEFT JOIN Requests rqs ON rqs.AptID = apts.AptID AND rqs.Status = 'Open'
    GROUP BY bs.BuildingID;

-- FYI Solution from book is:
-- But i'm pretty sure we can avoid a subquery here... 
SELECT BuildingName, ISNULL(Count, 0) as 'Count' 
FROM Buildings 
LEFT JOIN 
(SELECT Apartments.BuildingID, count(*) as 'Count' 
FROM Requests INNER JOIN Apartments 
ON Requests.AptID = Apartments.AptID 
WHERE Requests.Status = 'Open' 
GROUP BY Apartments.BuildingID) ReqCounts 
ON ReqCounts.BuildingID = Buildings.BuildingID 