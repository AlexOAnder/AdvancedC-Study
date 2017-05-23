use "Northwind"
select ord.EmployeeId,ord.CustomerId, Count(ord.OrderID) as Amount from dbo.Orders as ord
where OrderDate between '1/1/1998' and '1/1/1999' /*1/1/199 not included in between*/
 group by ord.EmployeeId,ord.CustomerId
 Order by ord.EmployeeId