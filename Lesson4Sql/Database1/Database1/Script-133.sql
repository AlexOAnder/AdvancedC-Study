use "Northwind"
Select cust.CustomerId,cust.Country from Customers as cust where cust.Country>'b' and cust.Country<'h' order by cust.Country