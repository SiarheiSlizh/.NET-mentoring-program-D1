-- 1.1.1
-- Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года 
-- (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
-- Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia.

SELECT OrderID, ShippedDate, ShipVia 
FROM Orders
WHERE ShippedDate >= '05-06-1998';

-- 1.1.2
-- Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
-- В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ 
-- (использовать системную функцию CASЕ). Запрос должен возвращать только колонки OrderID и ShippedDate.

SELECT OrderID, CASE 
			WHEN ShippedDate IS NULL
			THEN 'Not Shipped'
			END AS ShippedDate
FROM Orders
WHERE ShippedDate IS NULL;

-- 1.1.3
-- Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года
-- (ShippedDate) не включая эту дату или которые еще не доставлены. В запросе должны 
-- возвращаться только колонки OrderID (переименовать в Order Number) и ShippedDate 
-- (переименовать в Shipped Date). В результатах запроса возвращать для колонки ShippedDate 
-- вместо значений NULL строку ‘Not Shipped’, для остальных значений возвращать дату в формате по умолчанию.

SELECT OrderID AS 'Order Number', CASE 
			WHEN ShippedDate IS NULL
			THEN 'Not Shipped'
			ELSE CONVERT(VARCHAR(10), ShippedDate, 101)
			END AS 'Shipped Date'
FROM Orders
WHERE ShippedDate > '05-07-1998' OR
	ShippedDate IS NULL;

-- 1.2.1
-- Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
-- Запрос сделать с только помощью оператора IN. Возвращать колонки с именем пользователя 
-- и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков 
-- и по месту проживания.

SELECT ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Address;

-- 1.2.2
-- Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
-- Запрос сделать с помощью оператора IN. Возвращать колонки с именем пользователя и названием
-- страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.
-- Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора IN. 
-- Возвращать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.

SELECT ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName;

-- 1.2.3
-- Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
-- Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
-- Не использовать предложение GROUP BY. Возвращать только одну колонку в результатах запроса

SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC;

-- 1.3.1
-- Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), где 
-- встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице 
-- Order Details. Использовать оператор BETWEEN. Запрос должен возвращать только колонку OrderID.

SELECT DISTINCT OrderID
FROM [Order Details] 
WHERE Quantity BETWEEN 3 AND 10;

-- 1.3.2
-- Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы 
-- из диапазона b и g. Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает 
-- Germany. Запрос должен возвращать только колонки CustomerID и Country и отсортирован по Country.

SELECT CustomerID, Country
FROM Customers
WHERE LEFT(Country, 1) BETWEEN 'b' AND 'g'
ORDER BY Country;

-- 1.3.3
-- Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы
-- из диапазона b и g, не используя оператор BETWEEN.

SELECT CustomerID, Country
FROM Customers
WHERE LEFT(Country, 1) >= 'b' AND 
	LEFT(Country, 1) <= 'g'
ORDER BY Country;

-- 1.4.1
-- В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
-- Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все 
-- продукты, которые удовлетворяют этому условию.

SELECT ProductName
FROM Products
WHERE ProductName LIKE '%choc_lade%';

-- 2.1.1
-- Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и 
-- скидок по ним. Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.

SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS Totals
FROM [Order Details];

-- 2.1.2
-- По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate
-- нет значения даты доставки). Использовать при этом запросе только оператор COUNT. Не использовать 
-- предложения WHERE и GROUP.

SELECT COUNT(
CASE 
	WHEN ShippedDate IS NULL 
	THEN 1 END) 
FROM Orders;

-- 2.1.3
-- По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
-- Использовать функцию COUNT и не использовать предложения WHERE и GROUP.

SELECT COUNT(DISTINCT CustomerID)
FROM Orders;

-- 2.2.1 
-- По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса 
-- надо возвращать две колонки c названиями Year и Total. Написать проверочный запрос, который 
-- вычисляет количество всех заказов.

SELECT COUNT(OrderID) AS Total, 
	YEAR(ShippedDate) AS Year
FROM ORDERS
GROUP BY YEAR(ShippedDate);

-- 2.2.2 
-- По таблице Orders найти количество заказов, cделанных каждым продавцом. Заказ для указанного
-- продавца – это любая запись в таблице Orders, где в колонке EmployeeID задано значение для 
-- данного продавца. В результатах запроса надо возвращать колонку с именем продавца 
-- (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
-- Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного 
-- запроса. Также основной запрос должен использовать группировку по EmployeeID.) с названием 
-- колонки ‘Seller’ и колонку c количеством заказов возвращать с названием 'Amount'. 
-- Результаты запроса должны быть упорядочены по убыванию количества заказов.

SELECT EmployeeID AS Seller, 
	(SELECT CONCAT(LastName, ' ', FirstName)
	FROM Employees
	WHERE EmployeeID = Orders.EmployeeID),
	COUNT(EmployeeID) AS Amount
FROM Orders
GROUP BY EmployeeID
ORDER BY Amount DESC;

-- 2.2.3 
-- По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя. 
-- Необходимо определить это только для заказов, сделанных в 1998 году.

SELECT EmployeeID, CustomerID, COUNT(CustomerID) AS Amount
FROM Orders
WHERE YEAR(ShippedDate) = '1998'
GROUP BY EmployeeID, CustomerID;

-- 2.2.4
-- Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один
-- или несколько продавцов, или только один или несколько покупателей, то информация о таких 
-- покупателя и продавцах не должна попадать в результирующий набор. Не использовать конструкцию JOIN.

SELECT ContactName,
	CONCAT(LastName, ' ', FirstName),
	C.City
FROM Customers AS C, Employees AS E
WHERE C.City = E.City;

-- 2.2.5
-- Найти всех покупателей, которые живут в одном городе.

SELECT City,
	COUNT(CompanyName) AS Amount
FROM Customers
GROUP BY City

-- 2.2.6
-- По таблице Employees найти для каждого продавца его руководителя.

SELECT CONCAT(LastName, ' ', FirstName) AS Employee,
	CASE 
	WHEN ReportsTo IS NULL
	THEN
		'No Header'
	ELSE
		(SELECT CONCAT(LastName, ' ', FirstName)
		FROM Employees AS R
		WHERE R.EmployeeID = E.ReportsTo)
	END AS Header
FROM Employees AS E;

-- 2.3.1
-- Определить продавцов, которые обслуживают регион 'Western' (таблица Region).

SELECT FirstName, LastName, RegionDescription
FROM Employees AS E
JOIN EmployeeTerritories AS ET ON ET.EmployeeID = E.EmployeeID
JOIN Territories AS T ON T.TerritoryID = ET.TerritoryID 
JOIN Region AS R ON T.RegionID = R.RegionID AND RegionDescription = 'Western'
GROUP BY FirstName, LastName, RegionDescription;

-- 2.3.2
-- Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество
-- их заказов из таблицы Orders. Принять во внимание, что у некоторых заказчиков нет заказов, но 
-- они также должны быть выведены в результатах запроса. Упорядочить результаты запроса по возрастанию количества заказов.

SELECT C.CompanyName,
	COUNT(O.OrderID) AS Amount
FROM Customers AS C
LEFT JOIN Orders AS O ON O.CustomerID = C.CustomerID
GROUP BY C.CompanyName
ORDER BY Amount;

-- 2.4.1
-- Выдать всех поставщиков (колонка CompanyName в таблице Suppliers), у которых нет хотя бы одного продукта
-- на складе (UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием оператора IN.

SELECT CompanyName
FROM Suppliers
WHERE SupplierID IN 
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0);

-- 2.4.2 
-- Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.

SELECT FirstName, LastName, EmployeeID
FROM Employees
WHERE EmployeeID IN 
	(SELECT EmployeeID
	FROM Orders
	GROUP BY EmployeeID
	HAVING COUNT(EmployeeID) > 150);

-- 2.4.3 
-- Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа 
-- (подзапрос по таблице Orders). Использовать оператор EXISTS.

SELECT CompanyName
FROM Customers AS C
WHERE NOT EXISTS
	(SELECT CustomerID
	FROM Orders AS O
	WHERE O.CustomerID = C.CustomerID);