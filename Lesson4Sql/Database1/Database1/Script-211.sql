use "Northwind"
/*Select  (ord.UnitPrice * ord.Quantity) - (ord.UnitPrice*ord.Quantity * Discount) as TotalPrice, * from dbo."Order Details" as ord*/
Select  SUM((ord.UnitPrice * ord.Quantity) - (ord.UnitPrice*ord.Quantity * Discount)) as Totals from dbo."Order Details" as ord