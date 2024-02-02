DROP TABLE Purchases;
DROP TABLE Customers;
DROP TABLE Products;
DROP TABLE Manufacturers;
DROP TABLE Categories;

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(200) UNIQUE NOT NULL
);

CREATE TABLE Manufacturers (
    ManufacturerID INT PRIMARY KEY IDENTITY,
    ManufacturerName NVARCHAR(200) UNIQUE NOT NULL
);

CREATE TABLE Products (
    ArticleNumber NVARCHAR(450) PRIMARY KEY,
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(max),
    Price MONEY NOT NULL,
    CategoryID INT NOT NULL,
    ManufacturerID INT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE CASCADE,
    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID) ON DELETE CASCADE
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) UNIQUE NOT NULL,
    Phone VARCHAR(25)
);

CREATE TABLE Purchases (
    PurchaseID INT PRIMARY KEY IDENTITY,
    CustomerID INT NOT NULL,
    ProductArticleNumber NVARCHAR(450),
    Quantity INT NOT NULL,
    UnitPrice MONEY NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (ProductArticleNumber) REFERENCES Products(ArticleNumber)
);