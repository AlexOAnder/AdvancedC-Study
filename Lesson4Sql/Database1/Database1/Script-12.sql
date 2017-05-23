use "Northwind"
select ord.OrderId, 
CASE   
	WHEN ord.ShippedDate IS NULL THEN 'Not Shipped' 
END  as ShippedDate
from Orders as ord where ord.ShippedDate IS NULL