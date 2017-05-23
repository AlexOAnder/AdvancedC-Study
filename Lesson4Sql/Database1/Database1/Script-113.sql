use "Northwind"
Select ord.OrderId as "Order Number",
CASE 
	WHEN ord.ShippedDate IS NULL THEN 'Not Shipped'
	ELSE convert(char(12),ord.ShippedDate,0)
END as "Shipped Date"

from dbo.Orders as ord where ord.ShippedDate IS NULL or ord.ShippedDate > '1998-05-06T00:00:00.000Z'