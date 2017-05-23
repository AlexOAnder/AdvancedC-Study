use "Northwind"
select cust.ContactName, Count(ord.OrderID) as ordersCount from Customers as cust 
left join Orders as ord ON ord.CustomerID = cust.CustomerID
group by cust.ContactName
order by ordersCount