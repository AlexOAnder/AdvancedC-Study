use "Northwind"
select * from Employees as empl where empl.EmployeeID 
in (select ord.EmployeeID 
		from Orders as ord 
		where
			(Select Count(ord2.EmployeeID) from Orders as ord2 where ord2.EmployeeID = ord.EmployeeID ) > 150)