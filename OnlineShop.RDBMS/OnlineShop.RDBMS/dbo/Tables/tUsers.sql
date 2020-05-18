CREATE TABLE dbo.tUsers
(
	UserID INT IDENTITY (1,1) NOT NULL,
	Pass NVARCHAR(12) NOT NULL,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL,
	UserType NVARCHAR(25) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(11),
	Company VARCHAR(100),
	Address1 VARCHAR(120) NOT NULL,
	City VARCHAR(100) NOT NULL,
	PostalCode VARCHAR(20) NOT NULL,
	CONSTRAINT PK_tUsers_UserID PRIMARY KEY CLUSTERED (UserID ASC)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about single user.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tUsers';
GO

