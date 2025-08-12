IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StoreManagementDB')
BEGIN
    CREATE DATABASE StoreManagementDB;
    PRINT 'Database StoreManagementDB created successfully.';
END
GO

USE StoreManagementDB;
GO

-- Bước 2: Tạo các Bảng
-- ==============================================================================

-- Bảng Roles (Phân quyền: Admin, Sales, Warehouse)
IF OBJECT_ID('dbo.Roles', 'U') IS NULL
BEGIN
    CREATE TABLE Roles (
        RoleID INT PRIMARY KEY IDENTITY,
        RoleName NVARCHAR(50) NOT NULL UNIQUE
    );
    PRINT 'Table Roles created successfully.';
END
GO

-- Bảng Employees (Thông tin nhân viên)
IF OBJECT_ID('dbo.Employees', 'U') IS NULL
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT PRIMARY KEY IDENTITY,
        FullName NVARCHAR(100) NOT NULL,
        Position NVARCHAR(50),
        Phone NVARCHAR(20),
        Email NVARCHAR(100) UNIQUE
    );
    PRINT 'Table Employees created successfully.';
END
GO

-- Bảng Accounts (Tài khoản đăng nhập của nhân viên)
IF OBJECT_ID('dbo.Accounts', 'U') IS NULL
BEGIN
    CREATE TABLE Accounts (
        AccountID INT PRIMARY KEY IDENTITY,
        Username NVARCHAR(50) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        RoleID INT NOT NULL,
        EmployeeID INT NOT NULL UNIQUE,
        FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
        FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID) ON DELETE CASCADE
    );
    PRINT 'Table Accounts created successfully.';
END
GO

-- Bảng Categories (Danh mục sản phẩm)
IF OBJECT_ID('dbo.Categories', 'U') IS NULL
BEGIN
    CREATE TABLE Categories (
        CategoryID INT PRIMARY KEY IDENTITY,
        CategoryName NVARCHAR(100) NOT NULL UNIQUE
    );
    PRINT 'Table Categories created successfully.';
END
GO

-- Bảng Products (Thông tin sản phẩm)
IF OBJECT_ID('dbo.Products', 'U') IS NULL
BEGIN
    CREATE TABLE Products (
        ProductID INT PRIMARY KEY IDENTITY,
        ProductName NVARCHAR(200) NOT NULL,
        UnitPrice DECIMAL(18, 2) NOT NULL CHECK (UnitPrice >= 0),
        Quantity INT NOT NULL CHECK (Quantity >= 0),
        CategoryID INT,
        FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE SET NULL
    );
    PRINT 'Table Products created successfully.';
END
GO

-- Bảng Customers (Thông tin khách hàng)
IF OBJECT_ID('dbo.Customers', 'U') IS NULL
BEGIN
    CREATE TABLE Customers (
        CustomerID INT PRIMARY KEY IDENTITY,
        FullName NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20) UNIQUE,
        Address NVARCHAR(255)
    );
    PRINT 'Table Customers created successfully.';
END
GO

-- Bảng Orders (Thông tin đơn hàng)
IF OBJECT_ID('dbo.Orders', 'U') IS NULL
BEGIN
    CREATE TABLE Orders (
        OrderID INT PRIMARY KEY IDENTITY,
        OrderDate DATETIME NOT NULL,
        CustomerID INT NOT NULL,
        EmployeeID INT NOT NULL,
        TotalAmount DECIMAL(18, 2) NOT NULL,
        FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
        FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
    );
    PRINT 'Table Orders created successfully.';
END
GO

-- Bảng OrderDetails (Chi tiết các sản phẩm trong một đơn hàng)
IF OBJECT_ID('dbo.OrderDetails', 'U') IS NULL
BEGIN
    CREATE TABLE OrderDetails (
        OrderDetailID INT PRIMARY KEY IDENTITY,
        OrderID INT NOT NULL,
        ProductID INT NOT NULL,
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(18, 2) NOT NULL,
        FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );
    PRINT 'Table OrderDetails created successfully.';
END
GO

-- Bước 3: Chèn Dữ liệu Mẫu (Initial Data Seeding)
-- ==============================================================================

-- Chèn dữ liệu cho bảng Roles (chỉ chạy một lần nếu bảng trống)
IF NOT EXISTS (SELECT 1 FROM Roles)
BEGIN
    INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Sales'), ('Warehouse');
    PRINT 'Sample data inserted into Roles.';
END
GO

-- Chèn dữ liệu cho bảng Categories
IF NOT EXISTS (SELECT 1 FROM Categories)
BEGIN
    INSERT INTO Categories (CategoryName) VALUES 
    (N'Điện tử'), 
    (N'Sách'), 
    (N'Thời trang'), 
    (N'Hàng tiêu dùng'),
    (N'Phụ kiện');
    PRINT 'Sample data inserted into Categories.';
END
GO

-- Chèn dữ liệu cho bảng Employees
IF NOT EXISTS (SELECT 1 FROM Employees)
BEGIN
    INSERT INTO Employees (FullName, Position, Phone, Email) VALUES 
    (N'Phạm Hiếu Chung', 'System Admin', '0900000001', 'admin@store.com'),
    (N'Nguyễn Thị Lan Anh', 'Sales Staff', '0900000002', 'sales@store.com'),
    (N'Trần Văn Lộc', 'Warehouse Staff', '0900000003', 'warehouse@store.com');
    PRINT 'Sample data inserted into Employees.';
END
GO

DELETE FROM Accounts;
GO
-- Chèn dữ liệu cho bảng Accounts (Mật khẩu mặc định cho tất cả là '123456')
IF NOT EXISTS (SELECT 1 FROM Accounts)
BEGIN
    -- Lưu ý: Các chuỗi hash này được tạo ra từ mật khẩu '123456' bằng BCrypt
    INSERT INTO Accounts (Username, PasswordHash, RoleID, EmployeeID) VALUES 
    ('admin', '123456', 1, 1),
    ('sales', '123456', 2, 2),
    ('warehouse', '123456', 3, 3);
    PRINT 'Sample data inserted into Accounts.';
END
GO

-- Chèn dữ liệu cho bảng Products
IF NOT EXISTS (SELECT 1 FROM Products)
BEGIN
    INSERT INTO Products (ProductName, UnitPrice, Quantity, CategoryID) VALUES
    (N'Laptop Dell XPS 15', 35000000, 10, 1),
    (N'iPhone 14 Pro 256GB', 28000000, 25, 1),
    (N'Sách Đắc Nhân Tâm', 150000, 50, 2),
    (N'Áo thun Uniqlo', 350000, 100, 3),
    (N'Chuột không dây Logitech', 550000, 75, 5);
    PRINT 'Sample data inserted into Products.';
END
GO

-- Chèn dữ liệu cho bảng Customers
IF NOT EXISTS (SELECT 1 FROM Customers)
BEGIN
    INSERT INTO Customers (FullName, Phone, Address) VALUES
    (N'Nguyễn Văn An', '0912345678', N'123 Lê Lợi, Quận 1, TPHCM'),
    (N'Trần Thị Bình', '0987654321', N'456 Nguyễn Huệ, Quận 1, TPHCM');
    PRINT 'Sample data inserted into Customers.';
END
GO

PRINT 'Database setup is complete.';
GO

SELECT Username, PasswordHash FROM Accounts;
GO