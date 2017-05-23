use "Northwind"
select ord.OrderID, ord.ShippedDate, ord.ShipVia from dbo.Orders as ord where ord.ShippedDate >= '1998-05-06T00:00:00.000Z' and ord.ShipVia >= 2;
