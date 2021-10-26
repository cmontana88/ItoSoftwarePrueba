IF NOT EXISTS(SELECT 1 FROM sys.sysdatabases WHERE name = 'PruebaIto')
BEGIN
	CREATE DATABASE PruebaIto
END
GO

USE PruebaIto
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Customer')
BEGIN
	CREATE TABLE Customer (
		CustomerId		INT IDENTITY,
		CustomerName	NVARCHAR(40),
		Phone			NVARCHAR(20) NULL,
		CONSTRAINT		PK_Customer PRIMARY KEY (CustomerId),
		CONSTRAINT		AK_Person_CustomerName UNIQUE (CustomerName)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Supplier')
BEGIN
	CREATE TABLE Supplier (
		SupplierId		INT IDENTITY,
		CompanyName		NVARCHAR(40),
		Phone			NVARCHAR(20) NULL,
		CONSTRAINT		PK_Supplier PRIMARY KEY (SupplierId),
		CONSTRAINT		AK_Supplier_CompanyName UNIQUE (CompanyName)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Order')
BEGIN
	CREATE TABLE "Order" (
		OrderId			INT IDENTITY,
		OrderNumber		NVARCHAR(10) NULL,
		CustomerId		INT,
		OrderDate		DATETIME,
		TotalAmount		DECIMAL(12, 2)
		CONSTRAINT		PK_Order PRIMARY KEY (OrderId),
		CONSTRAINT		FK_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
		CONSTRAINT		AK_Order_OrderNumber UNIQUE (OrderNumber)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Product')
BEGIN
	CREATE TABLE Product (
		ProductId		INT IDENTITY,
		ProductName		NVARCHAR(50),
		SupplierId		INT,
		UnitPrice		DECIMAL(12, 2) NULL,
		IsDiscontinued	BIT,
		CONSTRAINT		PK_Product PRIMARY KEY (ProductId),
		CONSTRAINT		FK_Supplier FOREIGN KEY (SupplierId) REFERENCES Supplier(SupplierId),
		CONSTRAINT		AK_Product_SupplierId UNIQUE (SupplierId),
		CONSTRAINT		AK_Product_ProductName UNIQUE (ProductName)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'OrderItem')
BEGIN
	CREATE TABLE OrderItem (
		OrderId		INT,
		ProductId	INT,
		UnitPrice	DECIMAL(12, 2),
		Quantity	INT,
		CONSTRAINT	PK_OrderItem PRIMARY KEY (OrderId, ProductId)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM Customer)
BEGIN
	INSERT INTO Customer 
	VALUES	('Carlos', '3005226655')
		,	('Andres', '3005227752')
END 
GO

IF NOT EXISTS(SELECT 1 FROM Supplier)
BEGIN
	INSERT INTO Supplier
	VALUES	('Noel', '3008456655')
		,	('Colombina', '3014582136')
END 
GO

IF NOT EXISTS(SELECT 1 FROM Product)
BEGIN
	INSERT INTO Product
	VALUES	('Galletas Saltin Noel', 1, 5600.00, 0)		
		,	('Cosecha Pura 1000 ml', 2, 5200.00, 0)
END
GO

