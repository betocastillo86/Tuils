USE [Tuils]
GO
/****** Object:  StoredProcedure [dbo].[ProductLoadAllPaged]    Script Date: 03/08/2015 6:58:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Gabriel Castillo
-- Update date: 14 mayo 2015
-- Description:	Se agrega tipo de filtro Fechaa de actualización
--				Se agrega parametro Published para realizar filtros sobre el campo directamente
-- =============================================
-- Author:		Gabriel Castillo
-- Update date: 22 mayo 2015
-- Description:	Se agrega conteo de productos por especificacion, ciudad, precio
-- =============================================
-- Author:		Gabriel Castillo
-- Update date: Junio 04 2015
-- Description:	Se agrega filtro y conteo por categorias especiales de los productos
-- =============================================
-- Author:		Gabriel Castillo
-- Update date: Junio 19 2015
-- Description:	Se agrega filtro por destacado en menú izquierdo
-- =============================================
-- Author:		Gabriel Castillo
-- Update date: Agosto 3 2015
-- Description:	Se cambia tipo de filtro de Fulltext search index sin CONTAINS y con formsof(INFLECTIONAL
-- =============================================
ALTER PROCEDURE [dbo].[ProductLoadAllPaged]
(
	@CategoryIds		nvarchar(MAX) = null,	--a list of category IDs (comma-separated list). e.g. 1,2,3
	@ManufacturerId		int = 0,
	@StoreId			int = 0,
	@VendorId			int = 0,
	@WarehouseId		int = 0,
	@ParentGroupedProductId	int = 0,
	@ProductTypeId		int = null, --product type identifier, null - load all products
	@VisibleIndividuallyOnly bit = 0, 	--0 - load all products , 1 - "visible indivially" only
	@ProductTagId		int = 0,
	@FeaturedProducts	bit = null,	--0 featured only , 1 not featured only, null - load all products
	@PriceMin			decimal(18, 4) = null,
	@PriceMax			decimal(18, 4) = null,
	@Keywords			nvarchar(4000) = null,
	@SearchDescriptions bit = 0, --a value indicating whether to search by a specified "keyword" in product descriptions
	@SearchSku			bit = 0, --a value indicating whether to search by a specified "keyword" in product SKU
	@SearchProductTags  bit = 0, --a value indicating whether to search by a specified "keyword" in product tags
	@UseFullTextSearch  bit = 0,
	@FullTextMode		int = 0, --0 - using CONTAINS with <prefix_term>, 5 - using CONTAINS and OR with <prefix_term>, 10 - using CONTAINS and AND with <prefix_term>
	@FilteredSpecs		nvarchar(MAX) = null,	--filter by attributes (comma-separated list). e.g. 14,15,16
	@LanguageId			int = 0,
	@OrderBy			int = 0, --0 - position, 5 - Name: A to Z, 6 - Name: Z to A, 10 - Price: Low to High, 11 - Price: High to Low, 15 - creation date
	@AllowedCustomerRoleIds	nvarchar(MAX) = null,	--a list of customer role IDs (comma-separated list) for which a product should be shown (if a subjet to ACL)
	@PageIndex			int = 0, 
	@PageSize			int = 2147483644,
	@ShowHidden			bit = 0,
	@Published			bit = null, --Null no realiza ningún filtro. True o False cancela el campo ShowHidden y busca directamente contra el valor
	@StateProvinceId	int = null, --Filtro por ciudad
	@SpecialCategoryId	int = null, --Filtro por categoria especial
	@LeftFeatured		bit = null, --filtra los productos destacados en el menú izquierdo
	@LoadPriceRange     int = 0,
	@LoadFilterableSpecificationAttributeOptionIds bit = 0, --a value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)
	@LoadFilterableCategoryIds  bit = 0, --Realiza el conteo de las categorias que están en el filtro dado
	@LoadFilterableStateProvinceIds  bit = 0, --Realiza el conteo de las ciudades que están en el filtro dado
	@LoadFilterableManufacturerIds  bit = 0, --Realiza el conteo de las marcas que están en el filtro dado
	@LoadFilterableSpecialCategoryIds  bit = 0, --Realiza el conteo de las categorias especiales que están en el filtro dado
	@OrderBySpecialCategoryId	int = null, --Si viene un numero realiza el ordenamiento por la categoria especial relacionada con el producto
	@FilterableSpecialCategoryIds nvarchar(MAX) = null OUTPUT, --Asigna el valor de las categorías especiales y el conteo respectivo por cada una
	@FilterableManufacturerIds nvarchar(MAX) = null OUTPUT, --Asigna el valor de las marcas y el conteo respectivo por cada una
	@FilterableStateProvinceIds nvarchar(MAX) = null OUTPUT, --Asigna el valor de las ciudades y el conteo respectivo por cada una
	@FilterableCategoryIds nvarchar(MAX) = null OUTPUT, --Asigna el valor de las categorias y el conteo respectivo por cada una
	@FilterableSpecificationAttributeOptionIds nvarchar(MAX) = null OUTPUT, --the specification attribute option identifiers applied to loaded products (all pages). returned as a comma separated list of identifiers
	@MinPrice			int = null OUTPUT, --Contiene el valor del menor precio de todos los productos filtrados
	@MaxPrice			int = null OUTPUT, --Contiene el valor del mayor precio de todos los productos filtrados
	@TotalRecords		int = null OUTPUT
)
AS
BEGIN
	
	/* Products that filtered by keywords */
	CREATE TABLE #KeywordProducts
	(
		[ProductId] int NOT NULL
	)

	DECLARE
		@SearchKeywords bit,
		@sql nvarchar(max),
		@sql_orderby nvarchar(max)

	SET NOCOUNT ON
	
	--filter by keywords
	SET @Keywords = isnull(@Keywords, '')
	SET @Keywords = rtrim(ltrim(@Keywords))
	IF ISNULL(@Keywords, '') != ''
	BEGIN
		SET @SearchKeywords = 1
		
		IF @UseFullTextSearch = 1
		BEGIN
			--remove wrong chars (' ")
			SET @Keywords = REPLACE(@Keywords, '''', '')
			SET @Keywords = REPLACE(@Keywords, '"', '')
			
			--full-text search
			IF @FullTextMode = 0 
			BEGIN
				--0 - using CONTAINS with <prefix_term>
				SET @Keywords = ' "' + @Keywords + '*" '
			END
			ELSE
			BEGIN
				--5 - using CONTAINS and OR with <prefix_term>
				--10 - using CONTAINS and AND with <prefix_term>

				--clean multiple spaces
				WHILE CHARINDEX('  ', @Keywords) > 0 
					SET @Keywords = REPLACE(@Keywords, '  ', ' ')

				DECLARE @concat_term nvarchar(100)				
				IF @FullTextMode = 5 --5 - using CONTAINS and OR with <prefix_term>
				BEGIN
					SET @concat_term = 'OR'
				END 
				IF @FullTextMode = 10 --10 - using CONTAINS and AND with <prefix_term>
				BEGIN
					SET @concat_term = 'AND'
				END

				--now let's build search string
				declare @fulltext_keywords nvarchar(4000)
				set @fulltext_keywords = N''
				declare @index int		
		
				set @index = CHARINDEX(' ', @Keywords, 0)

				-- if index = 0, then only one field was passed
				IF(@index = 0)
					set @fulltext_keywords = ' formsof(INFLECTIONAL, "' + @Keywords + '") '
				ELSE
				BEGIN		
					DECLARE @first BIT
					SET  @first = 1			
					WHILE @index > 0
					BEGIN
						IF (@first = 0)
							SET @fulltext_keywords = @fulltext_keywords + ' ' + @concat_term + ' '
						ELSE
							SET @first = 0

						SET @fulltext_keywords = @fulltext_keywords + ' formsof(INFLECTIONAL, "' + SUBSTRING(@Keywords, 1, @index - 1) + '")'					
						SET @Keywords = SUBSTRING(@Keywords, @index + 1, LEN(@Keywords) - @index)						
						SET @index = CHARINDEX(' ', @Keywords, 0)
					end
					
					-- add the last field
					IF LEN(@fulltext_keywords) > 0
						SET @fulltext_keywords = @fulltext_keywords + ' ' + @concat_term + ' ' + '"' + SUBSTRING(@Keywords, 1, LEN(@Keywords)) + '*"'	
				END
				SET @Keywords = @fulltext_keywords
			END
		END
		ELSE
		BEGIN
			--usual search by PATINDEX
			SET @Keywords = '%' + @Keywords + '%'
		END
		PRINT @Keywords

		--product name
		SET @sql = '
		INSERT INTO #KeywordProducts ([ProductId])
		SELECT p.Id
		FROM Product p with (NOLOCK)
		WHERE '
		IF @UseFullTextSearch = 1
			SET @sql = @sql + 'CONTAINS(p.[Name], @Keywords) '
		ELSE
			SET @sql = @sql + 'PATINDEX(@Keywords, p.[Name]) > 0 '



		--localized product name
		SET @sql = @sql + '
		UNION
		SELECT lp.EntityId
		FROM LocalizedProperty lp with (NOLOCK)
		WHERE
			lp.LocaleKeyGroup = N''Product''
			AND lp.LanguageId = ' + ISNULL(CAST(@LanguageId AS nvarchar(max)), '0') + '
			AND lp.LocaleKey = N''Name'''
		IF @UseFullTextSearch = 1
			SET @sql = @sql + ' AND CONTAINS(lp.[LocaleValue], @Keywords) '
		ELSE
			SET @sql = @sql + ' AND PATINDEX(@Keywords, lp.[LocaleValue]) > 0 '
	

		IF @SearchDescriptions = 1
		BEGIN
			--product short description
			SET @sql = @sql + '
			UNION
			SELECT p.Id
			FROM Product p with (NOLOCK)
			WHERE '
			IF @UseFullTextSearch = 1
				SET @sql = @sql + 'CONTAINS(p.[ShortDescription], @Keywords) '
			ELSE
				SET @sql = @sql + 'PATINDEX(@Keywords, p.[ShortDescription]) > 0 '


			--product full description
			SET @sql = @sql + '
			UNION
			SELECT p.Id
			FROM Product p with (NOLOCK)
			WHERE '
			IF @UseFullTextSearch = 1
				SET @sql = @sql + 'CONTAINS(p.[FullDescription], @Keywords) '
			ELSE
				SET @sql = @sql + 'PATINDEX(@Keywords, p.[FullDescription]) > 0 '



			--localized product short description
			SET @sql = @sql + '
			UNION
			SELECT lp.EntityId
			FROM LocalizedProperty lp with (NOLOCK)
			WHERE
				lp.LocaleKeyGroup = N''Product''
				AND lp.LanguageId = ' + ISNULL(CAST(@LanguageId AS nvarchar(max)), '0') + '
				AND lp.LocaleKey = N''ShortDescription'''
			IF @UseFullTextSearch = 1
				SET @sql = @sql + ' AND CONTAINS(lp.[LocaleValue], @Keywords) '
			ELSE
				SET @sql = @sql + ' AND PATINDEX(@Keywords, lp.[LocaleValue]) > 0 '
				

			--localized product full description
			SET @sql = @sql + '
			UNION
			SELECT lp.EntityId
			FROM LocalizedProperty lp with (NOLOCK)
			WHERE
				lp.LocaleKeyGroup = N''Product''
				AND lp.LanguageId = ' + ISNULL(CAST(@LanguageId AS nvarchar(max)), '0') + '
				AND lp.LocaleKey = N''FullDescription'''
			IF @UseFullTextSearch = 1
				SET @sql = @sql + ' AND CONTAINS(lp.[LocaleValue], @Keywords) '
			ELSE
				SET @sql = @sql + ' AND PATINDEX(@Keywords, lp.[LocaleValue]) > 0 '
		END

		--SKU
		IF @SearchSku = 1
		BEGIN
			SET @sql = @sql + '
			UNION
			SELECT p.Id
			FROM Product p with (NOLOCK)
			WHERE '
			IF @UseFullTextSearch = 1
				SET @sql = @sql + 'CONTAINS(p.[Sku], @Keywords) '
			ELSE
				SET @sql = @sql + 'PATINDEX(@Keywords, p.[Sku]) > 0 '
		END


		IF @SearchProductTags = 1
		BEGIN
			--product tag
			SET @sql = @sql + '
			UNION
			SELECT pptm.Product_Id
			FROM Product_ProductTag_Mapping pptm with(NOLOCK) INNER JOIN ProductTag pt with(NOLOCK) ON pt.Id = pptm.ProductTag_Id
			WHERE '
			IF @UseFullTextSearch = 1
				SET @sql = @sql + 'CONTAINS(pt.[Name], @Keywords) '
			ELSE
				SET @sql = @sql + 'PATINDEX(@Keywords, pt.[Name]) > 0 '

			--localized product tag
			SET @sql = @sql + '
			UNION
			SELECT pptm.Product_Id
			FROM LocalizedProperty lp with (NOLOCK) INNER JOIN Product_ProductTag_Mapping pptm with(NOLOCK) ON lp.EntityId = pptm.ProductTag_Id
			WHERE
				lp.LocaleKeyGroup = N''ProductTag''
				AND lp.LanguageId = ' + ISNULL(CAST(@LanguageId AS nvarchar(max)), '0') + '
				AND lp.LocaleKey = N''Name'''
			IF @UseFullTextSearch = 1
				SET @sql = @sql + ' AND CONTAINS(lp.[LocaleValue], @Keywords) '
			ELSE
				SET @sql = @sql + ' AND PATINDEX(@Keywords, lp.[LocaleValue]) > 0 '
		END

		--PRINT (@sql)
		EXEC sp_executesql @sql, N'@Keywords nvarchar(4000)', @Keywords

	END
	ELSE
	BEGIN
		SET @SearchKeywords = 0
	END

	--filter by category IDs
	SET @CategoryIds = isnull(@CategoryIds, '')	
	CREATE TABLE #FilteredCategoryIds
	(
		CategoryId int not null
	)
	INSERT INTO #FilteredCategoryIds (CategoryId)
	SELECT CAST(data as int) FROM [nop_splitstring_to_table](@CategoryIds, ',')	
	DECLARE @CategoryIdsCount int	
	SET @CategoryIdsCount = (SELECT COUNT(1) FROM #FilteredCategoryIds)

	--filter by attributes
	SET @FilteredSpecs = isnull(@FilteredSpecs, '')	
	CREATE TABLE #FilteredSpecs
	(
		SpecificationAttributeOptionId int not null
	)
	INSERT INTO #FilteredSpecs (SpecificationAttributeOptionId)
	SELECT CAST(data as int) FROM [nop_splitstring_to_table](@FilteredSpecs, ',')
	DECLARE @SpecAttributesCount int	
	SET @SpecAttributesCount = (SELECT COUNT(1) FROM #FilteredSpecs)

	--filter by customer role IDs (access control list)
	SET @AllowedCustomerRoleIds = isnull(@AllowedCustomerRoleIds, '')	
	CREATE TABLE #FilteredCustomerRoleIds
	(
		CustomerRoleId int not null
	)
	INSERT INTO #FilteredCustomerRoleIds (CustomerRoleId)
	SELECT CAST(data as int) FROM [nop_splitstring_to_table](@AllowedCustomerRoleIds, ',')

	--Cuando debe ordenar los productos por una categoria especial llena esta tabla con los productos que aplican a esa categoria
	CREATE TABLE #FilteredProductsWithSpecialCategoryId
	(
		ProductId int not null
	)
	--Solo carga datos cuando debe ordenar por categoria especial
	if @orderBySpecialCategoryId > 0
	begin
		INSERT INTO #FilteredProductsWithSpecialCategoryId (ProductId)
		SELECT
			Distinct 
			ProductId
		From
			[dbo].[SpecialCategoryProduct]
		where
			CategoryId = @orderBySpecialCategoryId
	end

	

	
	--paging
	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int
	DECLARE @RowsToReturn int
	SET @RowsToReturn = @PageSize * (@PageIndex + 1)	
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1
	
	CREATE TABLE #DisplayOrderTmp 
	(
		[Id] int IDENTITY (1, 1) NOT NULL,
		[ProductId] int NOT NULL,
		--Columna para identificar si el producto está destacado para la categoria enviada en la propiedad @OrderBySpecialCategoryId
		[Featured] bit NULL
	)

	
	
	--Si debe ordenar por categoría especial agrega una columna 
	if @orderBySpecialCategoryId > 0
	begin
		set @sql = '
		SELECT p.Id,
		(select count(0) from #FilteredProductsWithSpecialCategoryId where ProductId = p.Id) as OrderCategory,
		p.DisplayOrder '
	end
	--Si no, solo toma el valor del id del producto y lo inserta en la tabla temporal
	else
	begin
		SET @sql = '
		INSERT INTO #DisplayOrderTmp ([ProductId])
		SELECT p.Id '
	end
	
	set @sql = @sql + 'FROM
		Product p with (NOLOCK)'
	
	IF @CategoryIdsCount > 0
	BEGIN
		SET @sql = @sql + '
		LEFT JOIN Product_Category_Mapping pcm with (NOLOCK)
			ON p.Id = pcm.ProductId'
	END
	
	IF @ManufacturerId > 0
	BEGIN
		SET @sql = @sql + '
		LEFT JOIN Product_Manufacturer_Mapping pmm with (NOLOCK)
			ON p.Id = pmm.ProductId'
	END


	IF @SpecialCategoryId > 0 or @OrderBySpecialCategoryId > 0
	BEGIN
		SET @sql = @sql + '
		LEFT JOIN SpecialCategoryProduct scp with (NOLOCK)
			ON p.Id = scp.ProductId'
	END
	
	IF ISNULL(@ProductTagId, 0) != 0
	BEGIN
		SET @sql = @sql + '
		LEFT JOIN Product_ProductTag_Mapping pptm with (NOLOCK)
			ON p.Id = pptm.Product_Id'
	END
	
	--searching by keywords
	IF @SearchKeywords = 1
	BEGIN
		SET @sql = @sql + '
		JOIN #KeywordProducts kp
			ON  p.Id = kp.ProductId'
	END
	
	SET @sql = @sql + '
	WHERE
		p.Deleted = 0'
	
	--filter by category
	IF @CategoryIdsCount > 0
	BEGIN
		SET @sql = @sql + '
		AND pcm.CategoryId IN (SELECT CategoryId FROM #FilteredCategoryIds)'
		
		IF @FeaturedProducts IS NOT NULL
		BEGIN
			SET @sql = @sql + '
		AND pcm.IsFeaturedProduct = ' + CAST(@FeaturedProducts AS nvarchar(max))
		END
	END
	
	--filter by manufacturer
	IF @ManufacturerId > 0
	BEGIN
		SET @sql = @sql + '
		AND pmm.ManufacturerId = ' + CAST(@ManufacturerId AS nvarchar(max))
		
		IF @FeaturedProducts IS NOT NULL
		BEGIN
			SET @sql = @sql + '
		AND pmm.IsFeaturedProduct = ' + CAST(@FeaturedProducts AS nvarchar(max))
		END
	END

	--filter by special Category
	IF @SpecialCategoryId > 0
	BEGIN
		SET @sql = @sql + '
		AND scp.CategoryId = ' + CAST(@SpecialCategoryId AS nvarchar(max))
	END
	
	--filter by vendor
	IF @VendorId > 0
	BEGIN
		SET @sql = @sql + '
		AND p.VendorId = ' + CAST(@VendorId AS nvarchar(max))
	END

	--filter by stateprovince
	IF not @StateProvinceId is null and @stateProvinceId > 0
	BEGIN
		SET @sql = @sql + '
		AND p.StateProvinceId = ' + CAST(@stateProvinceId AS nvarchar(max))
	END

	
	--filter by warehouse
	IF @WarehouseId > 0
	BEGIN
		--we should also ensure that 'ManageInventoryMethodId' is set to 'ManageStock' (1)
		--but we skip it in order to prevent hard-coded values (e.g. 1) and for better performance
		SET @sql = @sql + '
		AND  
			(
				(p.UseMultipleWarehouses = 0 AND
					p.WarehouseId = ' + CAST(@WarehouseId AS nvarchar(max)) + ')
				OR
				(p.UseMultipleWarehouses > 0 AND
					EXISTS (SELECT 1 FROM ProductWarehouseInventory [pwi]
					WHERE [pwi].WarehouseId = ' + CAST(@WarehouseId AS nvarchar(max)) + ' AND [pwi].ProductId = p.Id))
			)'
	END
	
	--filter by parent grouped product identifer
	IF @ParentGroupedProductId > 0
	BEGIN
		SET @sql = @sql + '
		AND p.ParentGroupedProductId = ' + CAST(@ParentGroupedProductId AS nvarchar(max))
	END
	
	--filter by product type
	IF @ProductTypeId is not null
	BEGIN
		SET @sql = @sql + '
		AND p.ProductTypeId = ' + CAST(@ProductTypeId AS nvarchar(max))
	END
	
	--filter by parent product identifer
	IF @VisibleIndividuallyOnly = 1
	BEGIN
		SET @sql = @sql + '
		AND p.VisibleIndividually = 1'
	END
	
	--filter by product tag
	IF ISNULL(@ProductTagId, 0) != 0
	BEGIN
		SET @sql = @sql + '
		AND pptm.ProductTag_Id = ' + CAST(@ProductTagId AS nvarchar(max))
	END
	
	-- Si published es null significa que debe realizar el filtro según la variable ShowHidden
	if @Published is null
	begin
		--show hidden
		IF @ShowHidden = 0 
		BEGIN
			SET @sql = @sql + '
			AND p.Published = 1
			AND p.Deleted = 0
			AND (getutcdate() BETWEEN ISNULL(p.AvailableStartDateTimeUtc, ''1/1/1900'') and ISNULL(p.AvailableEndDateTimeUtc, ''1/1/2999''))'
		END
	end
	else
	begin
		-- Si no es null realiza la busqueda contra el valor de la variable published
		SET @sql = @sql + '
			AND p.Published = '+convert(varchar, @Published)+'
			AND p.Deleted = 0
			AND (getutcdate() BETWEEN ISNULL(p.AvailableStartDateTimeUtc, ''1/1/1900'') and ISNULL(p.AvailableEndDateTimeUtc, ''1/1/2999''))'
	end


	if not @LeftFeatured is null 
	begin
		SET @sql = @sql + '
			AND p.LeftFeatured = '+convert(varchar, @LeftFeatured)
	end
	
	--min price
	IF @PriceMin is not null
	BEGIN
		SET @sql = @sql + '
		AND (
				(
					--special price (specified price and valid date range)
					(p.SpecialPrice IS NOT NULL AND (getutcdate() BETWEEN isnull(p.SpecialPriceStartDateTimeUtc, ''1/1/1900'') AND isnull(p.SpecialPriceEndDateTimeUtc, ''1/1/2999'')))
					AND
					(p.SpecialPrice >= ' + CAST(@PriceMin AS nvarchar(max)) + ')
				)
				OR 
				(
					--regular price (price isnt specified or date range isnt valid)
					(p.SpecialPrice IS NULL OR (getutcdate() NOT BETWEEN isnull(p.SpecialPriceStartDateTimeUtc, ''1/1/1900'') AND isnull(p.SpecialPriceEndDateTimeUtc, ''1/1/2999'')))
					AND
					(p.Price >= ' + CAST(@PriceMin AS nvarchar(max)) + ')
				)
			)'
	END
	
	--max price
	IF @PriceMax is not null
	BEGIN
		SET @sql = @sql + '
		AND (
				(
					--special price (specified price and valid date range)
					(p.SpecialPrice IS NOT NULL AND (getutcdate() BETWEEN isnull(p.SpecialPriceStartDateTimeUtc, ''1/1/1900'') AND isnull(p.SpecialPriceEndDateTimeUtc, ''1/1/2999'')))
					AND
					(p.SpecialPrice <= ' + CAST(@PriceMax AS nvarchar(max)) + ')
				)
				OR 
				(
					--regular price (price isnt specified or date range isnt valid)
					(p.SpecialPrice IS NULL OR (getutcdate() NOT BETWEEN isnull(p.SpecialPriceStartDateTimeUtc, ''1/1/1900'') AND isnull(p.SpecialPriceEndDateTimeUtc, ''1/1/2999'')))
					AND
					(p.Price <= ' + CAST(@PriceMax AS nvarchar(max)) + ')
				)
			)'
	END
	
	--show hidden and ACL
	IF @ShowHidden = 0
	BEGIN
		SET @sql = @sql + '
		AND (p.SubjectToAcl = 0 OR EXISTS (
			SELECT 1 FROM #FilteredCustomerRoleIds [fcr]
			WHERE
				[fcr].CustomerRoleId IN (
					SELECT [acl].CustomerRoleId
					FROM [AclRecord] acl with (NOLOCK)
					WHERE [acl].EntityId = p.Id AND [acl].EntityName = ''Product''
				)
			))'
	END
	
	--show hidden and filter by store
	IF @StoreId > 0
	BEGIN
		SET @sql = @sql + '
		AND (p.LimitedToStores = 0 OR EXISTS (
			SELECT 1 FROM [StoreMapping] sm with (NOLOCK)
			WHERE [sm].EntityId = p.Id AND [sm].EntityName = ''Product'' and [sm].StoreId=' + CAST(@StoreId AS nvarchar(max)) + '
			))'
	END
	
	--filter by specs
	IF @SpecAttributesCount > 0
	BEGIN
		SET @sql = @sql + '
		AND NOT EXISTS (
			SELECT 1 FROM #FilteredSpecs [fs]
			WHERE
				[fs].SpecificationAttributeOptionId NOT IN (
					SELECT psam.SpecificationAttributeOptionId
					FROM Product_SpecificationAttribute_Mapping psam with (NOLOCK)
					WHERE psam.AllowFiltering = 1 AND psam.ProductId = p.Id
				)
			)'
	END
	
	--sorting
	SET @sql_orderby = ''	
	IF @OrderBy = 5 /* Name: A to Z */
		SET @sql_orderby = ' p.[Name] ASC'
	ELSE IF @OrderBy = 6 /* Name: Z to A */
		SET @sql_orderby = ' p.[Name] DESC'
	ELSE IF @OrderBy = 10 /* Price: Low to High */
		SET @sql_orderby = ' p.[Price] ASC'
	ELSE IF @OrderBy = 11 /* Price: High to Low */
		SET @sql_orderby = ' p.[Price] DESC'
	ELSE IF @OrderBy = 15 /* creation date */
		SET @sql_orderby = ' p.[CreatedOnUtc] DESC'
	ELSE IF @OrderBy = 16 /* creation date */
		SET @sql_orderby = ' p.[UpdatedOnUtc] DESC'
	else if @OrderBySpecialCategoryId > 0 /* categoria especial*/
		SET @sql_orderby = ' subq.OrderCategory desc,  subq.[DisplayOrder] asc'
	ELSE /* default sorting, 0 (position) */
	BEGIN
		--category position (display order)
		IF @CategoryIdsCount > 0 SET @sql_orderby = ' pcm.DisplayOrder ASC'
		
		--manufacturer position (display order)
		IF @ManufacturerId > 0
		BEGIN
			IF LEN(@sql_orderby) > 0 SET @sql_orderby = @sql_orderby + ', '
			SET @sql_orderby = @sql_orderby + ' pmm.DisplayOrder ASC'
		END
		
		--parent grouped product specified (sort associated products)
		--IF @ParentGroupedProductId > 0
		--BEGIN
		--	IF LEN(@sql_orderby) > 0 SET @sql_orderby = @sql_orderby + ', '
		--	SET @sql_orderby = @sql_orderby + ' p.[DisplayOrder] ASC'
		--END
		
		----name
		--IF LEN(@sql_orderby) > 0 SET @sql_orderby = @sql_orderby + ', '
		--SET @sql_orderby = @sql_orderby + ' p.[Name] ASC'

		--se cambia para que ordene por defecto por  display order
		IF LEN(@sql_orderby) > 0 SET @sql_orderby = @sql_orderby + ', '
		SET @sql_orderby = @sql_orderby + ' p.[DisplayOrder] ASC'
	END
	

	
	
	--Si debe ordenar por categoría especial realiza una subconsulta sobre el resultado obtenido ordenando por 
	--los productos que si tienen asignada esa categoria como especial
	if @orderBySpecialCategoryId > 0
	begin
		--Julio 07 2015 - Se agrega columna subq.OrderCategory para poder destacar un producto en el filtro cuando @orderBySpecialCategoryId > 0
		set @sql = '
		INSERT INTO #DisplayOrderTmp ([ProductId], [Featured])
		SELECT subq.Id, subq.OrderCategory
		from(
		' + @sql + ' ) as subq
		order by' + @sql_orderby
	end
	else
	begin
		--agrega el orden como lo venía realizando anteriormente
		SET @sql = @sql + '
		ORDER BY' + @sql_orderby
	end


	

	--PRINT (@sql)
	EXEC sp_executesql @sql
	DROP TABLE #FilteredProductsWithSpecialCategoryId
	DROP TABLE #FilteredCategoryIds
	DROP TABLE #FilteredSpecs
	DROP TABLE #FilteredCustomerRoleIds
	DROP TABLE #KeywordProducts

	--Julio 23 2015 - Se agrega columna [ProductId] para poder controlar los detaados cuando se ordenen por categoria especial
	CREATE TABLE #PageIndex 
	(
		[IndexId] int IDENTITY (1, 1) NOT NULL,
		[ProductId] int NOT NULL,
		[Featured] bit default 0 
	)

	--Julio 23 2015 - Se divide en dos la consulta para no cargar mucho el sistema cuando no tiene categorua destacada
	if @OrderBySpecialCategoryId > 0
	begin
		INSERT INTO #PageIndex ([ProductId], [Featured])
		SELECT ProductId, [Featured]
		FROM #DisplayOrderTmp
		GROUP BY ProductId, [Featured]
		ORDER BY min([Id]), [Featured]
	end
	else
	begin
		INSERT INTO #PageIndex ([ProductId])
		SELECT ProductId
		FROM #DisplayOrderTmp
		GROUP BY ProductId
		ORDER BY min([Id])
	end 
	



	--total records
	SET @TotalRecords = @@rowcount
	
	DROP TABLE #DisplayOrderTmp

	--prepare filterable specification attribute option identifier (if requested)
	IF @LoadFilterableSpecificationAttributeOptionIds = 1
	BEGIN		
		CREATE TABLE #FilterableSpecs 
		(
			[SpecificationAttributeOptionId] int NOT NULL,
			CountProducts int not null
		)
		INSERT INTO #FilterableSpecs ([SpecificationAttributeOptionId], CountProducts)
		SELECT [psam].SpecificationAttributeOptionId, count(0)
		FROM [Product_SpecificationAttribute_Mapping] [psam] with (NOLOCK)
		WHERE [psam].[AllowFiltering] = 1
		AND [psam].[ProductId] IN (SELECT [pi].ProductId FROM #PageIndex [pi])
		group by SpecificationAttributeOptionId

		--recorre el listado de especificaciones separados por un guion por la cantidad encontrada		
		select  @FilterableSpecificationAttributeOptionIds = COALESCE(@FilterableSpecificationAttributeOptionIds + ',' , '') + CAST(SpecificationAttributeOptionId as nvarchar(4000))+ '-'+ cast(CountProducts as varchar)
		FROM #FilterableSpecs

		DROP TABLE #FilterableSpecs
 	END

	if @LoadFilterableCategoryIds = 1 
	begin
		CREATE TABLE #FilterableCategories
		(
			[CategoryId] int NOT NULL,
			CountProducts int not null
		)
		INSERT INTO #FilterableCategories ([CategoryId], CountProducts)
		SELECT CategoryId, count(0)
		FROM Product_Category_Mapping with (NOLOCK)
		WHERE [ProductId] IN (SELECT [pi].ProductId FROM #PageIndex [pi])
		group by [CategoryId]

		--recorre el listado de especificaciones separados por un guion por la cantidad encontrada		
		select  @FilterableCategoryIds = COALESCE(@FilterableCategoryIds + ',' , '') + CAST(CategoryId as nvarchar(4000))+ '-'+ cast(CountProducts as varchar)
		FROM #FilterableCategories

		DROP TABLE #FilterableCategories
	end


	if @LoadFilterableManufacturerIds = 1 
	begin
		CREATE TABLE #FilterableManufacturers
		(
			[ManufacturerId] int NOT NULL,
			CountProducts int not null
		)
		INSERT INTO #FilterableManufacturers ([ManufacturerId], CountProducts)
		SELECT [ManufacturerId], count(0)
		FROM Product_Manufacturer_Mapping with (NOLOCK)
		WHERE [ProductId] IN (SELECT [pi].ProductId FROM #PageIndex [pi])
		group by ManufacturerId

		--recorre el listado de marcas separados por un guion por la cantidad encontrada		
		select  @FilterableManufacturerIds = COALESCE(@FilterableManufacturerIds + ',' , '') + CAST(ManufacturerId as nvarchar(4000))+ '-'+ cast(CountProducts as varchar)
		FROM #FilterableManufacturers

		DROP TABLE #FilterableManufacturers
	end


	if @LoadFilterableSpecialCategoryIds = 1 
	begin
		CREATE TABLE #FilterableSpecialCategories
		(
			[SpecialCategoryId] int NOT NULL,
			CountProducts int not null
		)
		INSERT INTO #FilterableSpecialCategories ([SpecialCategoryId], CountProducts)
		SELECT [CategoryId], count(0)
		FROM SpecialCategoryProduct with (NOLOCK)
		WHERE [ProductId] IN (SELECT [pi].ProductId FROM #PageIndex [pi])
		group by CategoryId

		--recorre el listado de marcas separados por un guion por la cantidad encontrada		
		select  @FilterableSpecialCategoryIds = COALESCE(@FilterableSpecialCategoryIds + ',' , '') + CAST(SpecialCategoryId as nvarchar(4000))+ '-'+ cast(CountProducts as varchar)
		FROM #FilterableSpecialCategories

		DROP TABLE #FilterableSpecialCategories
	end


	if @LoadFilterableStateProvinceIds = 1 
	begin
		CREATE TABLE #FilterableStatesProvinces
		(
			[StateProvinceId] int NOT NULL,
			CountProducts int not null
		)
		INSERT INTO #FilterableStatesProvinces ([StateProvinceId], CountProducts)
		SELECT StateProvinceId, count(0)
		FROM Product 
		---Revisar si se puede mejorar agregando columna nueva a #PageIndex
		where id IN (SELECT [pi].ProductId FROM #PageIndex [pi])
		group by StateProvinceId

		--recorre el listado de especificaciones separados por un guion por la cantidad encontrada		
		select  @FilterableStateProvinceIds = COALESCE(@FilterableStateProvinceIds + ',' , '') + CAST(StateProvinceId as nvarchar(4000))+ '-'+ cast(CountProducts as varchar)
		FROM #FilterableStatesProvinces

		DROP TABLE #FilterableStatesProvinces
	end

	if @LoadPriceRange = 1 
	begin
		--carga el menor procio y el mayor precio de todos los productso
		SELECT @MaxPrice = max(Price), @MinPrice = min(Price)
		FROM Product 
		where id IN (SELECT [pi].ProductId FROM #PageIndex [pi])
	end

	

	--return products
	SELECT TOP (@RowsToReturn)
		[Id]
		,[ProductTypeId]
		,[ParentGroupedProductId]
		,[VisibleIndividually]
		,[Name]
		,[ShortDescription]
		,[FullDescription]
		,[AdminComment]
		,[ProductTemplateId]
		,[VendorId]
		,[ShowOnHomePage]
		,[MetaKeywords]
		,[MetaDescription]
		,[MetaTitle]
		,[AllowCustomerReviews]
		,[ApprovedRatingSum]
		,[NotApprovedRatingSum]
		,[ApprovedTotalReviews]
		,[NotApprovedTotalReviews]
		,[SubjectToAcl]
		,[LimitedToStores]
		,[Sku]
		,[ManufacturerPartNumber]
		,[Gtin]
		,[IsGiftCard]
		,[GiftCardTypeId]
		,[RequireOtherProducts]
		,[RequiredProductIds]
		,[AutomaticallyAddRequiredProducts]
		,[IsDownload]
		,[DownloadId]
		,[UnlimitedDownloads]
		,[MaxNumberOfDownloads]
		,[DownloadExpirationDays]
		,[DownloadActivationTypeId]
		,[HasSampleDownload]
		,[SampleDownloadId]
		,[HasUserAgreement]
		,[UserAgreementText]
		,[IsRecurring]
		,[RecurringCycleLength]
		,[RecurringCyclePeriodId]
		,[RecurringTotalCycles]
		,[IsRental]
		,[RentalPriceLength]
		,[RentalPricePeriodId]
		,[IsShipEnabled]
		,[IsFreeShipping]
		,[ShipSeparately]
		,[AdditionalShippingCharge]
		,[DeliveryDateId]
		,[IsTaxExempt]
		,[TaxCategoryId]
		,[IsTelecommunicationsOrBroadcastingOrElectronicServices]
		,[ManageInventoryMethodId]
		,[UseMultipleWarehouses]
		,[WarehouseId]
		,[StockQuantity]
		,[DisplayStockAvailability]
		,[DisplayStockQuantity]
		,[MinStockQuantity]
		,[LowStockActivityId]
		,[NotifyAdminForQuantityBelow]
		,[BackorderModeId]
		,[AllowBackInStockSubscriptions]
		,[OrderMinimumQuantity]
		,[OrderMaximumQuantity]
		,[AllowedQuantities]
		,[AllowAddingOnlyExistingAttributeCombinations]
		,[DisableBuyButton]
		,[DisableWishlistButton]
		,[AvailableForPreOrder]
		,[PreOrderAvailabilityStartDateTimeUtc]
		,[CallForPrice]
		,[Price]
		,[OldPrice]
		,[ProductCost]
		,[SpecialPrice]
		,[SpecialPriceStartDateTimeUtc]
		,[SpecialPriceEndDateTimeUtc]
		,[CustomerEntersPrice]
		,[MinimumCustomerEnteredPrice]
		,[MaximumCustomerEnteredPrice]
		,[HasTierPrices]
		,[HasDiscountsApplied]
		,[Weight]
		,[Length]
		,[Width]
		,[Height]
		,[AvailableStartDateTimeUtc]
		,[AvailableEndDateTimeUtc]
		,[DisplayOrder]
		,[Published]
		,[Deleted]
		,[CreatedOnUtc]
		,[UpdatedOnUtc]
		,[Year]
		,[IsNew]
		,[StateProvinceId]
		,[DetailShipping]
		,[IncludeSupplies]
		,[SuppliesValue]
		,[Visits]
		,[TotalSales]
		,[UnansweredQuestions]
		,[LeftFeatured]
		,pi.Featured as FeaturedBySpecialCategory
	FROM
		#PageIndex [pi]
		INNER JOIN Product p with (NOLOCK) on p.Id = [pi].[ProductId]
	WHERE
		[pi].IndexId > @PageLowerBound AND 
		[pi].IndexId < @PageUpperBound
	ORDER BY
		[pi].IndexId
	
	DROP TABLE #PageIndex
END

