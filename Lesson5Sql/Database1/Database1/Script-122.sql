use "Northwind"
Select cust.ContactName,cust.Country from dbo.Customers as cust where Country NOT IN ('USA','Canada')
Order by cust.ContactName