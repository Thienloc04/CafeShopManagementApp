-- Tạo cơ sở dữ liệu
CREATE DATABASE CafeManagementSystem;
GO

USE CafeManagementSystem;
GO

-- Bảng Role
CREATE TABLE Role (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL
);

-- Bảng User
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Status INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ProfileImg NVARCHAR(255),
    RoleId INT,
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

-- Bảng Category
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(50) NOT NULL
);

-- Bảng Product
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Quantity INT,
    Status INT,
    ProImg NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

-- Bảng Order
CREATE TABLE [Order] (
    OrderId INT PRIMARY KEY IDENTITY,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(10, 2) NOT NULL,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

-- Bảng OrderDetail
CREATE TABLE OrderDetail (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);
