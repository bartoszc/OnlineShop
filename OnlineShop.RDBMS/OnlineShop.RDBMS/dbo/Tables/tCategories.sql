CREATE TABLE dbo.tCategories
(
	CategoryID INT IDENTITY (1,1) NOT NULL,
	CategoryName NVARCHAR(25) NOT NULL,
	CategoryDescription TEXT,
	CONSTRAINT PK_tCategories_CategoryID PRIMARY KEY CLUSTERED (CategoryID ASC),
	CONSTRAINT AK_tCategories_CategorytName UNIQUE (CategoryName ASC)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about categories of products.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tCategories';
GO