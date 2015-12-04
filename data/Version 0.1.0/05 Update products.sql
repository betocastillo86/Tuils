delete [dbo].[ProductReview]
go
delete [order]
go
update product set displayorder = 3
go
update [dbo].[LocaleStringResource] set resourcevalue = 'Su plan ha llegado al limite de productos a publicar ({0} productos). Adquiera un plan más alto para poder destacar mejor sus productos' where resourcename = 'selectfeaturedattributesbyplan.hasreachedlimitofproducts' 