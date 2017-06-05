use "Northwind"
select ord.OrderDate as Year2, Count(OrderID) as Total from Orders as ord group by OrderDate