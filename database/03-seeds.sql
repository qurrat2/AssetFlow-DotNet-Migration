sql
USE AssetFlow;
GO

DELETE dbo.Assignments; DELETE dbo.Assets; DELETE dbo.Users; DELETE dbo.Departments;
DBCC CHECKIDENT ('dbo.Departments', RESEED, 0);
DBCC CHECKIDENT ('dbo.Users', RESEED, 0);
DBCC CHECKIDENT ('dbo.Assets', RESEED, 0);
DBCC CHECKIDENT ('dbo.Assignments', RESEED, 0);
GO

INSERT dbo.Departments (Name) VALUES ('IT'), ('Operations');

-- Hash for password "Password123!" — generated via BCrypt.Net-Next (work factor 11)
DECLARE @h NVARCHAR(200) = '$2a$11$wvGaoGD.WWTXbN9uMgMyl.8mcVPFHMFSpbgpne1yuw5UwUTnPvsoe';

INSERT dbo.Users (Username, PasswordHash, DepartmentId, Role) VALUES
  ('admin',   @h, 1, 'Admin'),
  ('alice',   @h, 1, 'Staff'),
  ('bob',     @h, 2, 'Staff');

INSERT dbo.Assets (Tag, Description, Status, DepartmentId) VALUES
  ('LAPTOP-001', 'Dell Latitude 5430',  'Available', 1),
  ('LAPTOP-002', 'Lenovo ThinkPad T14', 'Available', 1),
  ('PHONE-001',  'iPhone 14',           'Available', 1),
  ('FORKLIFT-1', 'Toyota 8FBE15U',      'Available', 2),
  ('SCANNER-1',  'Zebra TC52',          'Available', 2),
  ('SCANNER-2',  'Zebra TC52',          'Available', 2);
GO
