use "Northwind"
select ord.EmployeeID, (select CONCAT(emp.FirstName ,' ', emp.LastName) from Employees as emp where emp.EmployeeID = ord.EmployeeID) as Seller, Count(ord.OrderID) as Amount
from Orders as ord 
group by ord.EmployeeID
order by Amount DESC