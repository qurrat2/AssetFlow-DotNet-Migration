sql

IF DB_ID('AssetFlow') IS NULL
    CREATE DATABASE AssetFlow;
GO
USE AssetFlow;
GO

IF OBJECT_ID('dbo.Assignments', 'U') IS NOT NULL DROP TABLE dbo.Assignments;
IF OBJECT_ID('dbo.Assets', 'U') IS NOT NULL DROP TABLE dbo.Assets;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL DROP TABLE dbo.Departments;
GO

CREATE TABLE dbo.Departments (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Name      NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Users (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Username      NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(200) NOT NULL,
    DepartmentId  INT NOT NULL FOREIGN KEY REFERENCES dbo.Departments(Id),
    Role          NVARCHAR(50) NOT NULL,
    CreatedAt     DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Assets (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    Tag               NVARCHAR(50) NOT NULL UNIQUE,
    Description       NVARCHAR(500) NOT NULL,
    Status            NVARCHAR(50) NOT NULL,        -- Available, Assigned, Retired
    DepartmentId      INT NOT NULL FOREIGN KEY REFERENCES dbo.Departments(Id),
    AssignedToUserId  INT NULL FOREIGN KEY REFERENCES dbo.Users(Id),
    CreatedAt         DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Assignments (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    AssetId     INT NOT NULL FOREIGN KEY REFERENCES dbo.Assets(Id),
    UserId      INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(Id),
    AssignedOn  DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    ReturnedOn  DATETIME2 NULL,
    Notes       NVARCHAR(500) NULL
);

CREATE INDEX IX_Assets_DepartmentId ON dbo.Assets(DepartmentId);
CREATE INDEX IX_Assignments_AssetId ON dbo.Assignments(AssetId);
GO
