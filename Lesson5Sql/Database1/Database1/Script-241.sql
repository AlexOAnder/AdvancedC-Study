use "Northwind"
select * from Suppliers as sup where sup.SupplierID in (select SupplierID from Products where UnitsInStock = 0)