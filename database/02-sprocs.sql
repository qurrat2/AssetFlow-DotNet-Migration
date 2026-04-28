sql

Use AssetFlow
GO

IF OBJECT_ID('dbo.sp_GetAssetsByDepartment', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetAssetsByDepartment;
IF OBJECT_ID('dbo.sp_GetAssetById',          'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetAssetById;
IF OBJECT_ID('dbo.sp_GetUserByUsername',     'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetUserByUsername;
IF OBJECT_ID('dbo.sp_AssignAsset',           'P') IS NOT NULL DROP PROCEDURE dbo.sp_AssignAsset;
IF OBJECT_ID('dbo.sp_ReturnAsset',           'P') IS NOT NULL DROP PROCEDURE dbo.sp_ReturnAsset;
GO

CREATE PROCEDURE dbo.sp_GetAssetsByDepartment
    @DepartmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Tag, Description, Status, DepartmentId, AssignedToUserId, CreatedAt
    FROM dbo.Assets
    WHERE DepartmentId = @DepartmentId
    ORDER BY Tag;
END
GO

CREATE PROCEDURE dbo.sp_GetAssetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Tag, Description, Status, DepartmentId, AssignedToUserId, CreatedAt
    FROM dbo.Assets WHERE Id = @Id;
END
GO

CREATE PROCEDURE dbo.sp_GetUserByUsername
    @Username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, PasswordHash, DepartmentId, Role, CreatedAt
    FROM dbo.Users WHERE Username = @Username;
END
GO

CREATE PROCEDURE dbo.sp_AssignAsset
    @AssetId INT, @UserId INT, @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.Assets WHERE Id = @AssetId AND Status = 'Available')
    BEGIN
        ROLLBACK; THROW 50001, 'Asset not available', 1;
    END
    INSERT dbo.Assignments (AssetId, UserId, Notes) VALUES (@AssetId, @UserId, @Notes);
    UPDATE dbo.Assets SET Status='Assigned', AssignedToUserId=@UserId WHERE Id=@AssetId;
    COMMIT;
END
GO

CREATE PROCEDURE dbo.sp_ReturnAsset
    @AssignmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    DECLARE @AssetId INT;
    SELECT @AssetId = AssetId FROM dbo.Assignments WHERE Id=@AssignmentId AND ReturnedOn IS NULL;
    IF @AssetId IS NULL BEGIN ROLLBACK; THROW 50002, 'Active assignment not found', 1; END
    UPDATE dbo.Assignments SET ReturnedOn=SYSUTCDATETIME() WHERE Id=@AssignmentId;
    UPDATE dbo.Assets SET Status='Available', AssignedToUserId=NULL WHERE Id=@AssetId;
    COMMIT;
END
GO
