CREATE TABLE dbo.tCards
(
	CardID INT IDENTITY (1,1) NOT NULL,
	CardNumber NVARCHAR(25) NOT NULL,
	NameOnCard NVARCHAR(50) NOT NULL,
	ExpirationDate DateTime2(0) NOT NULL,
	CVV INT NOT NULL,
	CONSTRAINT PK_tCards_CardID PRIMARY KEY CLUSTERED (CardID ASC)
);
GO
 
EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about cards.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tCards';
GO
