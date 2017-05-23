use "Northwind"

select cus.CustomerID,cus.ContactName from Customers as cus where EXISTS (select ord.OrderID from Orders as ord where ord.CustomerID = cus.CustomerID)