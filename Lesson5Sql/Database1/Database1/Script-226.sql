use "Northwind"
select empl.EmployeeID, concat(empl.LastName,' ',empl.FirstName) as Name , empl.ReportsTo , (select concat(empl2.LastName,' ', empl2.FirstName) from Employees as empl2 where empl2.EmployeeId = empl.ReportsTo) as ReportToName from Employees as empl where empl.ReportsTo is not null
 