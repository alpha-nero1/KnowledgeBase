SELECT TenantName FROM Tenants ts
    INNER JOIN 
    (
        SELECT TenantID, COUNT(AptID) FROM AptTenants
            GROUP BY AptTenants.TenantID
            HAVING COUNT(AptID) > 1
    ) SubQuery
    ON ts.TenantID = SubQuery.TenantID
