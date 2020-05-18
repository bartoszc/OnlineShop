CREATE TABLE dbo.tOrders
(
	OrderID INT NOT NULL,
	UserID INT NOT NULL,
	OrderDate DateTime2(0) NOT NULL,
	ShippedDate DateTime2(0) NOT NULL,
	ShippingAddress NVARCHAR(100) NOT NULL,
	ShippingMethod NVARCHAR(25) NOT NULL,
	OrderStatus NVARCHAR(25) NOT NULL,
	Comments Text,
	CONSTRAINT PK_tOrders_OrderID PRIMARY KEY CLUSTERED (OrderID ASC),
	CONSTRAINT FK_tOrders_Users FOREIGN KEY (UserID) REFERENCES dbo.tUsers (UserID)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about all orders.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tOrders';
GO