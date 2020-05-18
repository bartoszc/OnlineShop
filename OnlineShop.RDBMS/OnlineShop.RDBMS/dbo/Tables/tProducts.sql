CREATE TABLE dbo.tProducts
(
	ProductID INT IDENTITY (1,1) NOT NULL,
	CategoryID INT NOT NULL,
	ProductName NVARCHAR(50) NOT NULL,
	Price FLOAT NOT NULL,
	ImageColumn VARBINARY (MAX),
	ProductDescription TEXT,
	CONSTRAINT PK_tProducts_ProductID PRIMARY KEY CLUSTERED (ProductID ASC),
	CONSTRAINT AK_tProducts_ProductName UNIQUE (ProductName ASC),
	CONSTRAINT FK_tProducts_tCategories FOREIGN KEY (CategoryID) REFERENCES dbo.tCategories (CategoryID)
);
GO
 
EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about products in shop.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tProducts';
GO