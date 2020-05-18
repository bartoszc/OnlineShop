CREATE TABLE dbo.tPromotions
(
PromoID INT IDENTITY (1,1) NOT NULL,
DiscountValue FLOAT,
CONSTRAINT PK_tPromotions_PromoID PRIMARY KEY CLUSTERED (PromoID ASC) 
);
GO
 
EXECUTE sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Contains information about current promotions.',
	@level0type = N'SCHEMA',
	@level0name = N'dbo',
	@level1type = N'TABLE',
	@level1name = N'tPromotions';
GO