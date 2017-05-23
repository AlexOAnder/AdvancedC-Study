use "Northwind"
select ord.EmployeeId,ord.CustomerId from dbo.Orders as ord 
where (select empl.City from Employees as empl where empl.EmployeeID = ord.EmployeeID) = (select cust.City from Customers as cust where cust.CustomerID = ord.CustomerID)
group by ord.EmployeeId,ord.CustomerId
 Order by ord.EmployeeId