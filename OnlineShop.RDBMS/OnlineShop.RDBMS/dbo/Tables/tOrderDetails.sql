CREATE TABLE dbo.tOrderDetails
(
	OrderDetailsID INT IDENTITY (1,1) NOT NULL,
	OrderID INT NOT NULL,
	ProductID INT NOT NULL,
	OrderCost FLOAT NOT NULL,
	Quantity INT NOT NULL,
	PromoID INT NOT NULL,
	CONSTRAINT PK_tOrderDetails_OrderDetailsID PRIMARY KEY CLUSTERED (OrderDetailsID ASC),
	CONSTRAINT FK_tOrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES dbo.tOrders (OrderID),
	CONSTRAINT FK_tOrderDetails_Products FOREIGN KEY (ProductID) REFERENCES dbo.tProducts (ProductID),
	CONSTRAINT FK_tOrderDetails_Promotions FOREIGN KEY (PromoID) REFERENCES dbo.tPromotions (PromoID)
);
GO

EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains info about single order.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tOrderDetails';
GO