-- Chạy script này trên SQL Server Management Studio (SSMS)
-- để tạo database và bảng trước khi chạy ứng dụng

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TodoDB')
BEGIN
    CREATE DATABASE TodoDB;
END
GO

USE TodoDB;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Todos' AND xtype='U')
BEGIN
    CREATE TABLE Todos (
        Id          INT IDENTITY(1,1) PRIMARY KEY,
        Title       NVARCHAR(500)     NOT NULL,
        IsCompleted BIT               NOT NULL DEFAULT 0
    );
END
GO
