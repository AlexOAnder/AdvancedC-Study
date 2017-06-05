use "Northwind"
Select cust.CustomerId,cust.Country from Customers as cust where cust.Country Between 'B' and 'H' order by cust.Country