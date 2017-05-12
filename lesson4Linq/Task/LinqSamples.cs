// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();
		delegate IEnumerable searchExpression(int i);

		[Category("LINQ")]
		[Title("Task001")]
		[Description("Get a list of all customers whose total sum of all orders exceeds some value of X")]
		public void Linq001()
		{
			//Get a list of all customers whose total turnover (the sum of all orders) exceeds some value of X
			searchExpression myDelegate = total => dataSource.Customers
			   .Where(c => c.Orders.Sum(t => t.Total) > total)
			   .Select(c => c.CustomerID);

			var customers = myDelegate(5000);

			ObjectDumper.Write("With total x = 5000");
			foreach (var c in customers)
			{
				ObjectDumper.Write(c);
			}

			customers = myDelegate(20000);
			ObjectDumper.Write("With total x = 20000");
			foreach (var c in customers)
			{
				ObjectDumper.Write(c);
			}
		}

		[Category("LINQ")]
		[Title("Task002")]
		[Description("For each customer, make a list of suppliers located in the same country and the same city")]

		public void Linq002()
		{
			//For each customer, make a list of suppliers located in the same country and the same city
			var countries = dataSource.Suppliers.Join(dataSource.Customers,  // inner sequence
				e => e.Country, // outerSelector key
				o => o.Country, // innerSelector key
				(e, o) => new // result selector (we create new Select view with fields that we need)
					{
						Country = e.Country,
						Supplier = e.SupplierName,
						Customer = o.CompanyName
					});

			// other type of the same request (but it will return other result with sorting and grouping)
			var suppliersCountries = from suppliers in dataSource.Suppliers
									 join customers in dataSource.Customers 
										on suppliers.Country equals customers.Country
									 select new
									 {
										 Country = suppliers.Country,
										 Supplier = suppliers.SupplierName,
										 Customer = customers.CompanyName
									 };

			foreach (var c in countries)
			{
				ObjectDumper.Write(c);
			}
		}

		[Category("LINQ")]
		[Title("Task003")]
		[Description("Find all customers who have orders that exceed the sum of X")]

		public void Linq003()
		{
			//Find all customers who have orders that exceed the sum of X
			var x = 10000;

			var customers = dataSource.Customers
				.Where(c => c.Orders
					.Where(o => o.Total > x).ToArray().Length > 0)
				.Select(c => c.CompanyName);
			foreach (var c in customers)
			{
				ObjectDumper.Write(c);
			}
		}


		[Category("LINQ")]
		[Title("Task004")]
		[Description("Get a list of customers indicating which month of the year they became clients (take for the month and year of the first order)")]

		public void Linq004()
		{
			//Get a list of customers indicating which month of the year they became clients(take for the month and year of the first order)
			var customers = dataSource.Customers.Select(c => new
				{
					Customer = c.CompanyName,
					Date = c.Orders.Any() ?
						c.Orders.DefaultIfEmpty()
								.OrderBy(o => o.OrderDate)
								.FirstOrDefault().OrderDate : // it can be replaced with ?. but i using 2015 VS without C#6
								new DateTime()
				});

			foreach (var c in customers)
			{
				ObjectDumper.Write(c.Customer + ":  " + c.Date.ToShortDateString());
			}

		}

		[Category("LINQ")]
		[Title("Task005")]
		[Description("Priveous task with the sorted date of the first order")]

		public void Linq005()
		{
			//Priveous task with the sorted date of the first order
			var customers = dataSource.Customers.Select(c => new
				{
					Customer = c.CompanyName,
					Date = c.Orders.Any() ?
						c.Orders.DefaultIfEmpty()
								.OrderBy(o => o.OrderDate)
								.FirstOrDefault().OrderDate :
								new DateTime()
				})
				.OrderBy(c => c.Date.Year)
				.ThenBy(c => c.Date.Month)
				.ThenBy(c => c.Customer);

			foreach (var c in customers)
			{
				ObjectDumper.Write(c.Customer + ":  " + c.Date.ToShortDateString());
			}
		}

		[Category("LINQ")]
		[Title("Task006")]
		[Description("Get all clients that have a non-digital code specified or the region is not filled " +
			"or the operator code is not specified in the phone (for simplicity, consider that this is equivalent " +
			"to 'there are no round brackets at the beginning')")]

		public void Linq006()
		{
			var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			var operatorCodeSymbols = new char[] { '(', ')' };
			var customers = dataSource.Customers
				.Where(c => (!string.IsNullOrEmpty(c.PostalCode) && c.PostalCode.IndexOfAny(letters) > -1)
					|| string.IsNullOrEmpty(c.Region)
					|| c.Phone.IndexOfAny(operatorCodeSymbols) == -1).Select(c => c.CompanyName);

			foreach (var c in customers)
			{
				ObjectDumper.Write(c);
			}
		}

		[Category("LINQ")]
		[Title("Task007")]
		[Description("Group all products by category, inside - by stock, within the last group sort by cost")]

		public void Linq007()
		{
			//Group all products by category, inside - by stock, within the last group sort by cost
			var products = dataSource.Products.OrderBy(p => p.Category)
											  .ThenBy(p => p.UnitsInStock)
											  .ThenBy(p => p.UnitPrice);

			foreach (var c in products)
			{
				ObjectDumper.Write(c.Category + "  " + c.UnitsInStock + "  " + c.UnitPrice);
			}
		}

		[Category("LINQ")]
		[Title("Task008")]
		[Description("Group products into groups 'cheap', 'average price', 'expensive'")]

		public void Linq008()
		{
			var smaillPrice = 5;
			var mediumPrice = 15;

			var products1 = dataSource.Products.Where(p => p.UnitPrice <= smaillPrice);
			var products2 = dataSource.Products.Where(p => p.UnitPrice <= mediumPrice && p.UnitPrice >= smaillPrice);
			var products3 = dataSource.Products.Where(p => p.UnitPrice >= mediumPrice);

			var products = products1.Concat(products2).Concat(products3);

			foreach (var c in products)
			{
				ObjectDumper.Write(c.ProductName + "  " + c.UnitPrice);
			}
		}

		[Category("LINQ")]
		[Title("Task009")]
		[Description("Calculate the average profitability of each city and the average intensity")]
		public void Linq009()
		{
			var cities = dataSource.Customers.Select(c => c.City).Distinct().ToArray();
			var profitability = new decimal[cities.Length];
			var intensity = new double[cities.Length];
			for (int i = 0; i < cities.Length; i++)
			{
				var customersInTheCity = dataSource.Customers.Where(c => c.City == cities[i] && c.Orders.Any());
				profitability[i] = customersInTheCity.Select(c => c.Orders.Average(x => x.Total)).FirstOrDefault();
				intensity[i] = customersInTheCity.Average(c => c.Orders.Length);
			}
			for (int i = 0; i < cities.Length; i++)
			{
				ObjectDumper.Write(cities[i] + "   " + profitability[i] + "   " + intensity[i]);
			}
		}

		[Category("LINQ")]
		[Title("Task010")]
		[Description("Make average annual activity statistics of clients by months (without taking into account the year), " +
			"statistics by years, by years and by months (that is, when one month in different years has its value).")]

		public void Linq010()
		{
			var clients = this.dataSource.Customers;
			//by month
			var byMonth =
				from order in clients.SelectMany(client => client.Orders).Select(o => o.OrderDate)
				group order by order.Month
				into groupped
				select new KeyValuePair<string, int>(
						CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupped.Key),
						groupped.Count());

			foreach (var item in byMonth)
			{
				ObjectDumper.Write(string.Format("{0} - {1}", item.Key, item.Value));
			}

			//by Year
			var byYear = from order in clients.SelectMany(client => client.Orders).Select(o => o.OrderDate)
						 group order by order.Year
				into groupped
						 select new KeyValuePair<int, int>(groupped.Key,
								 groupped.Count());
			var orderedByYear = byYear.OrderBy(g => g.Key);

			foreach (var item in orderedByYear)
			{
				ObjectDumper.Write(string.Format("{0} - {1}", item.Key, item.Value));
			}

			//by years and by months
			var byYearAndByMonth = from order in clients
									   .SelectMany(client => client.Orders)
									   .Select(o => o.OrderDate)
								   group order by new { year = order.Year, mounth = order.Month }
				into groupped
						 select
							 new
							 {
								 Key = new KeyValuePair<int, int>(groupped.Key.year, groupped.Key.mounth),
								 Value = groupped.Count(),
							 };

			foreach (var item in byYearAndByMonth.OrderBy(g => g.Key.Key).ThenBy(g => g.Key.Value))
			{
				ObjectDumper.Write(string.Format("{0}, {1} - {2}", item.Key.Key,
					CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key.Value), item.Value));
			}
		}
	}
}
