// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
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

        [Category("Tasks Linq")]
        [Title("Task 1")]
        [Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void Linq01()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.Sum(v => v.Total) > 7000m)
                .Select(c => new { CustomerID = c.CustomerID });

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CustomerID);
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 2")]
        [Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе. Сделайте задания с использованием группировки и без.")]
        public void Linq02()
        {
            ObjectDumper.Write("First approach");

            var result = dataSource.Customers
                .Select(c => new
                {
                    Customer = c.CustomerID,
                    Suppliers = dataSource.Suppliers
                            .Where(s => c.Country == s.Country && c.City == s.City)
                            .Select(s => new { SupplierName = s.SupplierName })
                }
                );

            foreach (var dict in result)
            {
                if (dict.Suppliers.Count() != 0)
                {
                    ObjectDumper.Write(dict.Customer + ": ");
                    foreach (var supplier in dict.Suppliers)
                    {
                        ObjectDumper.Write(supplier.SupplierName);
                    }
                }
            }

            ObjectDumper.Write("Second approach");

            var groupByResult = dataSource.Customers
                .GroupBy(c => new
                {
                    Country = c.Country,
                    City = c.City
                },
                    cust => new
                    {
                        Customer = cust,
                        Suppliers = dataSource.Suppliers
                                .Where(s => s.City == cust.City && s.Country == cust.Country)
                    }
                    );

            foreach (var groupDictionary in groupByResult)
            {
                foreach (var dictionary in groupDictionary)
                {
                    if (dictionary.Suppliers.Count() != 0)
                    {
                        ObjectDumper.Write(dictionary.Customer.CustomerID);
                        foreach (var supplier in dictionary.Suppliers)
                        {
                            ObjectDumper.Write(supplier.SupplierName);
                        }
                    }
                }
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 3")]
        [Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]
        public void Linq03()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.FirstOrDefault(o => o.Total > 10000m) != null)
                .Select(c => new { CustomerID = c.CustomerID });

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CustomerID);
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 4")]
        [Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]
        public void Linq04()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.FirstOrDefault() != null)
                .Select(c => new
                {
                    CustomerID = c.CustomerID,
                    DateStart = c.Orders.Min(o => o.OrderDate)
                });

            foreach (var customer in customers)
            {
                ObjectDumper.Write($"{customer.CustomerID} - {customer.DateStart.Month}.{customer.DateStart.Year}");
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 5")]
        [Description("Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента")]
        public void Linq05()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.FirstOrDefault() != null)
                .Select(c => new
                {
                    CustomerID = c.CustomerID,
                    DateStart = c.Orders.Min(o => o.OrderDate),
                    Sum = c.Orders.Sum(t => t.Total)
                })
                .OrderBy(c => c.DateStart.Year)
                .ThenBy(c => c.DateStart.Month)
                .ThenByDescending(c => c.Sum)
                .ThenBy(c => c.CustomerID);

            foreach (var customer in customers)
            {
                ObjectDumper.Write($"{customer.CustomerID} - {customer.DateStart.Month}.{customer.DateStart.Year} - {customer.Sum}");
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).")]
        public void Linq06()
        {
            var customers = dataSource.Customers
                .Where(c => !int.TryParse(c.PostalCode, out int postCode)
                    || string.IsNullOrEmpty(c.Region)
                    || !c.Phone.Contains('('));

            foreach (var customer in customers)
            {
                ObjectDumper.Write($"{customer.CustomerID} - postcode: {customer.PostalCode} - region?: {customer.Region} - phone: {customer.Phone}");
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 7")]
        [Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости")]
        public void Linq07()
        {
            var products = dataSource.Products
                .GroupBy(p => p.Category, (pKey, pData) => new
                {
                    Category = pKey,
                    data = pData
                        .GroupBy(uis => uis.UnitsInStock != 0, (uisKey, uisData) => new
                        {
                            Availability = uisKey,
                            data = uisData.OrderBy(sum => sum.UnitPrice * sum.UnitsInStock)
                        })
                });

            foreach (var product in products)
            {
                foreach (var productByAvailable in product.data)
                {
                    foreach (var item in productByAvailable.data)
                    {
                        ObjectDumper.Write($"{product.Category} - {item.ProductName} - {item.UnitsInStock} - {item.UnitsInStock * item.UnitPrice}");
                    }
                }
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 8")]
        [Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами")]
        public void Linq08()
        {
            var products = dataSource.Products
                .GroupBy(p => p.UnitPrice < 25 ? "Cheap" : p.UnitPrice < 50 ? "Average" : "Expensive")
                .Select(p => p.Select(c => new
                {
                    ProductName = c.ProductName,
                    Price = c.UnitPrice,
                    Type = p.Key
                }));

            foreach (var product in products)
            {
                foreach (var item in product)
                {
                    ObjectDumper.Write($"{item.Type} - {item.ProductName} - {item.Price}");
                }
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 9")]
        [Description("Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)")]
        public void Linq09()
        {
            var result = dataSource.Customers
                .GroupBy(c => c.City, (cKey, cData) => new
                {
                    City = cKey,
                    CityIncome = cData
                        .Where(c => c.Orders.FirstOrDefault() != null)
                        .Sum(c => c.Orders.Sum(o => o.Total)),
                    OrderIntensity = cData
                        .Where(c => c.Orders.FirstOrDefault() != null)
                        .Sum(c => c.Orders.Count()) / cData.Count()
                });

            foreach (var info in result)
            {
                ObjectDumper.Write($"{info.City} - {info.CityIncome} - {info.OrderIntensity}");
            }
        }

        [Category("Tasks Linq")]
        [Title("Task 10")]
        [Description("Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void Linq10()
        {
            var activity = dataSource.Customers
                .Select(c => new
                {
                    CustomerID = c.CustomerID,
                    monthActivity = c.Orders.GroupBy(o => o.OrderDate.Month)
                        .Select(ord => new
                        {
                            Month = ord.Key,
                            Count = ord.Count()
                        }),
                    yearActivity = c.Orders.GroupBy(o => o.OrderDate.Year)
                        .Select(ord => new
                        {
                            Year = ord.Key,
                            Count = ord.Count()
                        }),
                    monthYearActivity = c.Orders.GroupBy(o => new { o.OrderDate.Month, o.OrderDate.Year })
                        .Select(ord => new
                        {
                            Month = ord.Key.Month,
                            Year = ord.Key.Year,
                            Count = ord.Count()
                        })
                });

            foreach (var result in activity)
            {
                ObjectDumper.Write(result.CustomerID);

                ObjectDumper.Write("By month without year");
                foreach (var item in result.monthActivity)
                {
                    ObjectDumper.Write($"{item.Month} - {item.Count}");
                }

                ObjectDumper.Write("By year");
                foreach (var item in result.yearActivity)
                {
                    ObjectDumper.Write($"{item.Year} - {item.Count}");
                }

                ObjectDumper.Write("By month with year");
                foreach (var item in result.monthYearActivity)
                {
                    ObjectDumper.Write($"{item.Month}.{item.Year} - {item.Count}");
                }
            }
        }
    }
}
