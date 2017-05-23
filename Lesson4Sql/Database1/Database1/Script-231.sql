use "Northwind"
Select empl.EmployeeID, empl.LastName,empl.FirstName,reg.RegionDescription from Employees as empl 
inner join EmployeeTerritories as emplterr ON emplterr.EmployeeID = empl.EmployeeID
inner join Territories as terr ON terr.TerritoryID = emplterr.TerritoryID
inner join Region as reg ON reg.RegionID = terr.RegionID 
where reg.RegionID = 2 /*or reg.RegionDescription = 'Western'*/