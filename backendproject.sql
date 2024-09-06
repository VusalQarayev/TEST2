-- Step 1: Create the Database
CREATE DATABASE PcStoreDB;
GO

-- Use the newly created database
USE PcStoreDB;
GO

-- Step 2: Create the Products Table
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Auto-increment primary key
    Name NVARCHAR(100) NOT NULL,     -- Product name, cannot be null
    Price DECIMAL(18, 2) NOT NULL,   -- Product price with two decimal places
    Image NVARCHAR(200),             -- URL of the product image
    Category NVARCHAR(50),           -- Category of the product
    Stock INT NOT NULL               -- Stock quantity, cannot be null
);
GO

-- Step 3: Insert Sample Data into Products Table
INSERT INTO Products (Name, Price, Image, Category, Stock)
VALUES 
    ('ADESSO IMOUSES50', 25, '/img/mouseadesso.jpg', 'Accessories', 100),
    ('Logitech M100 Wired Ambidextrous', 20, '/img/mouselogitech.jpg', 'Accessories', 150),
    ('NZXT H9 Flow', 300, '/img/casenzth.jpg', 'Cases', 50),
    ('CORSAIR 3500X ARGB', 199, '/img/casepurple.jpg', 'Cases', 30),
    ('MSI Apex Pro', 180, '/img/msiheadset.jpg', 'Headsets', 70),
    ('Razer BlackWidow V3', 170, '/img/razer.jpg', 'Keyboards', 120),
    ('Samsung Odyssey G5', 450, '/img/samsung.jpg', 'Monitors', 20),
    ('RTX 2080', 750, '/img/2080.jpg', 'Graphics Cards', 10),
    ('Asus Keyboard', 50, '/img/asuskeyboard.jpg', 'Keyboards', 80),
    ('AMD Ryzen 2700', 200, '/img/ryzen2700.jpg', 'Processors', 60);
GO
