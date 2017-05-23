use "Northwind"
Select * from dbo.Customers as cust where Country IN ('USA','Canada')
Order by cust.ContactName, cust.Country