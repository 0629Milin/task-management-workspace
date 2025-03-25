CREATE DATABASE RadixMartDB;
GO
 
use RadixMartDB

CREATE TABLE Category (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO
 
CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CategoryId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(18, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);
GO
 
 
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL, 
    Address NVARCHAR(500) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE() 
);
GO


INSERT INTO Category (Name)
VALUES ('Mobile'), ('Laptop');
GO

INSERT INTO Product (CategoryId, Name, Description, Price, StockQuantity)
VALUES
    (1, 'iPhone 15 Pro Max', 'Latest model of iPhone with improved camera', 156000, 50),
    (1, 'Samsung Galaxy S23', 'Flagship smartphone with AMOLED display', 125000, 30),
    (1, 'OnePlus 10', 'Affordable 5G smartphone with fast charging', 30000, 40);
GO

INSERT INTO Product (CategoryId, Name, Description, Price, StockQuantity)
VALUES
    (2, 'MacBook Air Ms', 'High-performance laptop with M1 chip', 115000, 20),
    (2, 'Dell XPS 13', 'Compact laptop with Intel i7 processor', 75000, 25),
    (2, 'HP Spectre x360', 'Convertible laptop with OLED display', 85000, 15);
GO