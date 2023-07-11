--PRODUCT

--Create
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_create]
(@CatID				int, 
 @Discount			int, 
 @UnitslnStock      int, 
 @Name				nvarchar(200),
 @Details			nvarchar(500),
 @Description		nvarchar(MAX),
 @SeoDescription	nvarchar(MAX),
 @Title				nvarchar(255),
 @SeoTitle			nvarchar(MAX),
 @LanguageId		varchar(5),
 @SeoAlias			nvarchar(200),
 @Price				Int
)
AS
    BEGIN
	  --Thêm vào bảng sản phẩm
      INSERT INTO Products
                (  
				CatID,
			    Discount,
				DateCreated,
				DateModified,
				BestSellers,
				HomeFlag,
				Active,
				Title,
				UnitsInStock,
				SeoAlias
                )
                VALUES
                (@CatID,
				@Discount,
				GETDATE(),
				null,
				0,
				0,
				1,
				@Title,
				@UnitslnStock,
				null
                );
		--Thêm vào bảng Sản phẩm theo ngôn ngữ
		INSERT INTO ProductTranslation
                (  
				ProductId,
			    Name,
				Description,
				Details,
				SeoDescription,
				SeoTitle,
				LanguageId,
				SeoAlias
                )
                VALUES
                (IDENT_CURRENT('Products'),
				@Name,
				@Description,
				@Details,
				@SeoDescription,
				@SeoTitle,
				@LanguageId,
				@SeoAlias
                );
		--Thêm vào bảng giá sản phẩm
		INSERT INTO AttributesPrices
                (  
				ProductId,
				Price,
				Active
                )
                VALUES
                (IDENT_CURRENT('Products'),
				@Price,
				1
                );
			
		SELECT '';
    END;
--bộ test
EXEC dbo.sp_product_create 1,1,1,'test5','test5','test5','test5','test5','test5','vi-VN','test',100000
DROP PROCEDURE [dbo].[sp_product_create];



--GetById
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_get_by_id](@productId int, @languageId varchar(5))
AS
    BEGIN
        SELECT *,
		(SELECT ImagePath
		FROM ListProductImage LI
		WHERE LI.ProductID = p.ProductID FOR JSON PATH) AS ImagePath,
		(SELECT AP.Price
		FROM AttributesPrices AP
		WHERE AP.ProductID = p.ProductID) AS Price
        FROM Products p inner join ProductTranslation pt
		ON p.ProductID = pt.ProductId 
		WHERE  @productId = p.ProductID AND @languageId=pt.LanguageId;
    END;
EXEC dbo.sp_product_get_by_id 1055,'vi-VN'
DROP PROCEDURE [dbo].[sp_product_get_by_id];

		SELECT *,
		(SELECT ImagePath
		FROM ListProductImage LI
		WHERE LI.ProductID = p.ProductID FOR JSON PATH) AS ImagePath,
		(SELECT AP.Price
		FROM AttributesPrices AP
		WHERE AP.ProductID = p.ProductID) AS Price
        FROM Products p inner join ProductTranslation pt
		ON p.ProductID = pt.ProductId 
		WHERE  1053 = p.ProductID AND 'vi-VN'=pt.LanguageId;


		

		

--Delete
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_delete](@productId int)
AS
    BEGIN
		DELETE FROM AttributesPrices
		WHERE ProductId = @productId
		DELETE FROM ListProductImage
		WHERE ProductId = @productId
		DELETE FROM ProductTranslation
		WHERE ProductId = @productId
		DELETE FROM Products
		WHERE ProductID = @productId 
    END;

EXEC dbo.sp_product_delete 1012
DROP PROCEDURE [dbo].[sp_product_delete];

--Update
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_update]
(
	@ProductId				 INT,
	@LanguageId				 VARCHAR(5),
	@Name				     NVARCHAR(200), 
	@SeoAlias		         NVARCHAR(200), 
	@SeoDescription          NVARCHAR(MAX), 
	@SeoTitle				 NVARCHAR(MAX), 
	@Description		     NVARCHAR(MAX),
	@Details			     NVARCHAR(500)
)
AS
    BEGIN
     
		UPDATE  ProductTranslation 
	    SET  
		Name = IIf(@Name is Null, Name, @Name),
		SeoAlias= IIF(@SeoAlias is Null,SeoAlias,@SeoAlias),
		SeoDescription = IIf(@SeoDescription is Null, SeoDescription, @SeoDescription),
		SeoTitle = IIf(@SeoTitle is Null, SeoTitle, @SeoTitle),
		Description = IIf(@Description is Null, Description, @Description),
		Details = IIf(@Details is Null, Details, @Details)
		Where ProductId=@ProductId and LanguageId = @LanguageId
		SELECT '';
    END;

--Update Stock
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_update_stock]
(
	@ProductId				 INT,
	@addQuantity			 INT
)
AS
    BEGIN
     
		UPDATE  Products
	    SET  
		UnitsInStock -= @addQuantity
		Where ProductId=@ProductId
		SELECT '';
    END;

EXEC dbo.sp_product_update_stock 1055,1
DROP PROCEDURE [dbo].[sp_product_update_stock];

--Update Price
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_product_update_price]
(
	@ProductId				 INT,
	@newPrice				 INT
)
AS
    BEGIN
     
		UPDATE  AttributesPrices
	    SET  
		Price = @newPrice
		Where ProductId=@ProductId
		SELECT '';
    END;


EXEC dbo.sp_product_update_price 46,200000

--Product Search by LanguageId
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[sp_product_getByLanguagetoPaging] (@page_index  INT, 
                                       @page_size   INT,
									   @languageId	VARCHAR(5)
									   )
AS
    BEGIN
        DECLARE @RecordCount BIGINT;
        IF(@page_size <> 0)
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock,
							   PT.Name,
							   PT.Description,
							   PT.Details,
							   PT.SeoDescription,
							   PT.SeoTitle,
							   PT.LanguageId,
							   PT.SeoAlias,
							   (SELECT TOP 1 LP.ImagePath
							   FROM ListProductImage AS LP
							   WHERE LP.ProductID = P.ProductID) AS ImagePath,
							   (SELECT TOP 1 AP.Price
							   FROM AttributesPrices AS AP
							   WHERE AP.ProductID = P.ProductID) AS Price
                        INTO #Results1
                        FROM [Products] AS P INNER JOIN ProductTranslation PT
						ON P.ProductID = PT.ProductId
					    WHERE  (PT.LanguageId like N'%'+@languageId+'%');                  
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results1
                        WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 AND(((@page_index - 1) * @page_size + 1) + @page_size) - 1
                              OR @page_index = -1;
                        DROP TABLE #Results1; 
            END;
            ELSE
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock,
							   PT.Name,
							   PT.Description,
							   PT.Details,
							   PT.SeoDescription,
							   PT.SeoTitle,
							   PT.LanguageId,
							   PT.SeoAlias,
							   (SELECT TOP 1 LP.ImagePath
							   FROM ListProductImage AS LP
							   WHERE LP.ProductID = P.ProductID) AS ImagePath,
							   (SELECT TOP 1 AP.Price
							   FROM AttributesPrices AS AP
							   WHERE AP.ProductID = P.ProductID) AS Price
                        INTO #Results2
                        FROM [Products] AS P INNER JOIN ProductTranslation PT
						ON P.ProductID = PT.ProductId
					    WHERE  (PT.LanguageId like N'%'+@languageId+'%');                       
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results2;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results2;
                        DROP TABLE #Results2;
        END;
    END;

EXEC [dbo].[sp_product_getByLanguagetoPaging] 1,10,'vi-VN'
DROP PROCEDURE [dbo].[sp_product_getByLanguagetoPaging];


--Get By Cate
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[sp_product_getByCate] (@page_index  INT, 
                                       @page_size   INT,
									   @CatID		INT,
									   @languageId	VARCHAR(5)
									   )
AS
    BEGIN
        DECLARE @RecordCount BIGINT;
        IF(@page_size <> 0)
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock,
							   PT.Name,
							   PT.Description,
							   PT.Details,
							   PT.SeoDescription,
							   PT.SeoTitle,
							   PT.LanguageId,
							   PT.SeoAlias,
							   (SELECT TOP 1 LP.ImagePath
							   FROM ListProductImage AS LP
							   WHERE LP.ProductID = P.ProductID) AS ImagePath,
							   (SELECT TOP 1 AP.Price
							   FROM AttributesPrices AS AP
							   WHERE AP.ProductID = P.ProductID) AS Price
                        INTO #Results1
                        FROM [Products] AS P INNER JOIN ProductTranslation PT
						ON P.ProductID = PT.ProductId
					    WHERE (P.CatID = @CatID) AND (PT.LanguageId like N'%'+@languageId+'%');                  
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results1
                        WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 AND(((@page_index - 1) * @page_size + 1) + @page_size) - 1
                              OR @page_index = -1;
                        DROP TABLE #Results1; 
            END;
            ELSE
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock,
							   PT.Name,
							   PT.Description,
							   PT.Details,
							   PT.SeoDescription,
							   PT.SeoTitle,
							   PT.LanguageId,
							   PT.SeoAlias,
							   (SELECT TOP 1 LP.ImagePath
							   FROM ListProductImage AS LP
							   WHERE LP.ProductID = P.ProductID) AS ImagePath,
							   (SELECT TOP 1 AP.Price
							   FROM AttributesPrices AS AP
							   WHERE AP.ProductID = P.ProductID) AS Price
                        INTO #Results2
                        FROM [Products] AS P INNER JOIN ProductTranslation PT
						ON P.ProductID = PT.ProductId
					    WHERE (P.CatID = @CatID) AND (PT.LanguageId like N'%'+@languageId+'%');                       
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results2;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results2;
                        DROP TABLE #Results2;
        END;
    END;

EXEC [dbo].[sp_product_getByCate] 1,10,1004,'vi-VN'
DROP PROCEDURE [dbo].[sp_product_getByCate];

--Product Search All To Paging
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[sp_product_getAlltoPaging] (@page_index  INT, 
                                       @page_size   INT
									   )
AS
    BEGIN
        DECLARE @RecordCount BIGINT;
        IF(@page_size <> 0)
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock
                        INTO #Results1
                        FROM [Products] AS P                  
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results1
                        WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 AND(((@page_index - 1) * @page_size + 1) + @page_size) - 1
                              OR @page_index = -1;
                        DROP TABLE #Results1; 
            END;
            ELSE
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY P.ProductID ASC)) AS RowNumber, 
                               P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock
                        INTO #Results2
                        FROM [Products] AS P                       
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results2;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results2;
                        DROP TABLE #Results2;
        END;
    END;

EXEC [dbo].[sp_product_getAlltoPaging] -1,2


--GetProductBestSell


--GetProductBestBuy
CREATE PROCEDURE [dbo].[sp_Product_GetProductBestBuy]
(
 @Quantity				INT,
 @LanguageId            VARCHAR(5)
)
AS
    BEGIN
		SELECT TOP(@Quantity) OD.ProductID,
		PT.Name,
		(SELECT TOP 1 LI.ImagePath
		FROM ListProductImage AS LI
		WHERE OD.ProductID = LI.ProductID) AS ImagePath,
		(SELECT TOP 1 AP.Price
		FROM AttributesPrices AS AP
		WHERE OD.ProductID = AP.ProductID) AS Price,
		SUM(OD.Amount) AS SL
		FROM OrderDetails AS OD INNER JOIN ProductTranslation PT
		ON OD.ProductID = PT.ProductID 
		WHERE PT.LanguageId = @LanguageId
		GROUP BY OD.ProductID,PT.Name
		ORDER BY SL DESC
    END;
DROP PROCEDURE [dbo].[sp_Product_GetProductBestBuy];

EXEC dbo.sp_Product_GetProductBestBuy 4,'vi-VN'




--GetProductNew
--CREATE PROCEDURE [dbo].[sp_Product_GetProductNew]
--(
-- @Quantity				INT,
-- @LanguageId            VARCHAR(5)
--)
--AS
--    BEGIN
--		SELECT TOP(@Quantity) OD.ProductID,
--		PT.Name,
--		LI.ImagePath,
--		P.DateCreated
--		FROM OrderDetails AS OD INNER JOIN ProductTranslation PT
--		ON OD.ProductID = PT.ProductID INNER JOIN Products P
--		ON OD.ProductID = P.ProductID INNER JOIN ListProductImage LI
--		ON OD.ProductID = Li.ProductID
--		WHERE PT.LanguageId = @LanguageId
--		GROUP BY OD.ProductID,PT.Name,LI.ImagePath,P.DateCreated
--		ORDER BY P.DateCreated DESC
--    END;



CREATE PROCEDURE [dbo].[sp_Product_GetProductNew]
(
 @Quantity				INT,
 @LanguageId            VARCHAR(5)
)
AS
    BEGIN
		SELECT TOP(@Quantity) P.ProductID,
							   P.CatID,
							   P.Discount,
							   P.DateCreated,
							   P.DateModified,
							   P.BestSellers,
							   P.HomeFlag,
							   P.Active,
							   P.Title,
							   P.UnitsInStock,
							   PT.Name,
							   PT.Description,
							   PT.Details,
							   PT.SeoDescription,
							   PT.SeoTitle,
							   PT.LanguageId,
							   PT.SeoAlias,
							   (SELECT TOP 1 LP.ImagePath
							   FROM ListProductImage AS LP
							   WHERE LP.ProductID = P.ProductID) AS ImagePath,
							   (SELECT TOP 1 AP.Price
							   FROM AttributesPrices AS AP
							   WHERE AP.ProductID = P.ProductID) AS Price

							   FROM [Products] AS P INNER JOIN ProductTranslation PT
								ON P.ProductID = PT.ProductId
								WHERE  (PT.LanguageId like N'%'+@languageId+'%')
								ORDER BY P.DateCreated DESC
    END;

EXEC dbo.sp_Product_GetProductNew 4,'vi-VN'
SELECT * FROM Products
DROP PROCEDURE [dbo].[sp_Product_GetProductNew];

--GetProductBestSeller
CREATE PROCEDURE [dbo].[sp_Product_GetProductBestSeller]
(
 @Quantity				INT,
 @LanguageId            VARCHAR(5)
)
AS
    BEGIN
		SELECT TOP(@Quantity) OD.ProductID,
		PT.Name,
		(SELECT TOP 1 LI.ImagePath
		FROM ListProductImage AS LI
		WHERE OD.ProductID = LI.ProductID) AS ImagePath,
		(SELECT TOP 1 AP.Price
		FROM AttributesPrices AS AP
		WHERE OD.ProductID = AP.ProductID) AS Price,
		SUM(OD.Quantity) AS SL
		FROM ExportBillDetail AS OD INNER JOIN ProductTranslation PT
		ON OD.ProductID = PT.ProductID
		WHERE PT.LanguageId = @LanguageId
		GROUP BY OD.ProductID,PT.Name
		ORDER BY SL DESC
    END;

EXEC dbo.sp_Product_GetProductBestSeller 4,'en-US'
DROP PROCEDURE [dbo].[sp_Product_GetProductBestSeller];

		


		


		



		














/*
-------------------------------------------------------------------------------------------------
									PRODUCTIMAGE
-------------------------------------------------------------------------------------------------
*/

--Lưu ảnh sản phẩm tải lên vào bảng danh sách ảnh
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListProductImage_create]
(
 @ImagePath				nvarchar(255),  
 @FizeSize				bigint
)
AS
    BEGIN
	  --Thêm vào bảng danh sách ảnh sản phẩm
      INSERT INTO ListProductImage
                (  
				ProductID,
			    ImagePath,
				Caption,
				IsDefault,
				DateCreated,
				SortOrder,
				FileSize
                )
                VALUES
                (IDENT_CURRENT('Products'),
				@ImagePath,
				'Thumbnail Image',
				1,
				GETDATE(),
				1,
				@FizeSize
                );	
		SELECT '';
    END;
--bộ test
EXEC dbo.sp_ListProductImage_create 'nhatdeptrai.jpg',6688
DROP PROCEDURE [dbo].[sp_ListProductImage_create];



-- Thêm ảnh vào danh sách ảnh
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListProductImage_AddImage]
(
 @ProductID				int,
 @Caption               nvarchar(200),
 @IsDefault				bit,
 @DateCreated			datetime,
 @SortOrder				int,
 @ImagePath				nvarchar(255),  
 @FizeSize				bigint
)
AS
    BEGIN
	  --Thêm vào bảng danh sách ảnh sản phẩm
      INSERT INTO ListProductImage
                (  
				ProductID,
			    ImagePath,
				Caption,
				IsDefault,
				DateCreated,
				SortOrder,
				FileSize
                )
                VALUES(
                @ProductID,
				@ImagePath,
				@Caption,
				@IsDefault,
				@DateCreated,
				@SortOrder,
				@FizeSize
                );	
		SELECT '';
    END;



--GetById
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listProductImage_get_by_id](@imageId int)
AS
    BEGIN
        SELECT *                       
        FROM ListProductImage lpi
		WHERE  @imageId = lpi.ListProductImageID 
    END;

--GetAll
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listProductImage_getAll](@ProductId int)
AS
    BEGIN
        SELECT *                       
        FROM ListProductImage lpi
		WHERE  @ProductId = lpi.ProductID 
    END;


--Delete
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listProductImage_delete](@imageId int)
AS
    BEGIN
		DELETE FROM ListProductImage
		WHERE ListProductImageID = @imageId
    END;

--Update
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listproductImage_update]
(
	@ListProductImageID      INT,
	@ImagePath				 NVARCHAR(255),
	@Caption				 NVARCHAR(200), 
	@IsDefault		         bit, 
	@SortOrder				 INT, 
	@FileSize				 BIGINT
)
AS
    BEGIN
     
		UPDATE  ListProductImage 
	    SET  
		ImagePath = IIf(@ImagePath is Null, ImagePath, @ImagePath),
		Caption= IIF(@Caption is Null,Caption,@Caption),
		IsDefault = IIf(@IsDefault is Null, IsDefault, @IsDefault),
		SortOrder = IIf(@SortOrder is Null, SortOrder, @SortOrder),
		FileSize = IIf(@FileSize is Null, FileSize, @FileSize)
		Where ListProductImageID=@ListProductImageID
		SELECT '';
    END;

--Lấy danh sách các ảnh của sản phẩm
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listProductImage_id](@ProductId int)
AS
    BEGIN
		SELECT *
		FROM ListProductImage
		WHERE ListProductImage.ProductID = @ProductId
    END;

DROP PROCEDURE [dbo].[sp_listProductImage_id];


		SELECT *
		FROM ListProductImage
		WHERE ListProductImage.ProductID = 24



 




/*
-------------------------------------------------------------------------------------------------
									CATEGORIES
-------------------------------------------------------------------------------------------------
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_categories_get_by_languageId](@LanguageId nvarchar(5))
AS
    BEGIN
        SELECT 
		C.CatID,
		C.ParentID,
		C.SortOrder,
		C.IsShowOnHome,
		C.Status,
		Ct.SeoDescription,
		Ct.SeoTitle,
		Ct.LanguageId,
		Ct.SeoAlias,
		Ct.Name
        FROM Categories C
		Inner Join CategoryTranslation Ct on C.CatID = Ct.CatID
		where  Ct.LanguageId = @LanguageId;
    END;

EXEC [dbo].[sp_categories_get_by_languageId] 'vi-VN'





/*
-------------------------------------------------------------------------------------------------
									USER
-------------------------------------------------------------------------------------------------
*/


--Kiểm tra đăng nhập
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Accounts_User_CheckLogin](@UserName nvarchar(50), @Password nvarchar(50))
AS
    BEGIN
        SELECT 
		u.UserID,
		u.FullName,
		u.Gender,
		u.Thumb,
		a.UserName,
		a.Password,
		r.RoleName
        FROM Accounts a inner join Users u		
		ON a.UserID = u.UserID 
		inner join Roles r
		On a.RoleID = r.RoleID
		WHERE  a.UserName = @UserName AND a.Password=@Password;
    END;

DROP PROCEDURE [dbo].[sp_Accounts_User_CheckLogin];


---Đăng ký tài khoản
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Accounts_Users_create]
(@Birthday				DATETIME,
 @Gender				nvarchar(20), 
 @Thumb					varchar(500), 
 @Address				nvarchar(1500), 
 @Email					nvarchar(100),
 @Phone					varchar(20),
 @FullName				nvarchar(200),
 @UserName				nvarchar(50),
 @Password				nvarchar(50),
 @RoleID				INT
)
AS
    BEGIN
	  --Thêm vào bảng users
      INSERT INTO Users
                (  
				BirthDay,
			    Gender,
				Thumb,
				Address,
				Email,
				Phone,
				Status,
				FullName
                )
                VALUES
                (
				@Birthday,
				@Gender,
				@Thumb,
				@Address,
				@Email,
				@Phone,
				1,
				@FullName
                );
		--Thêm vào bảng Accounts
		INSERT INTO Accounts
                (  
				Password,
			    Active,
				CreateDate,
				UserID,
				UserName,
				RoleID
                )
                VALUES
                (
				@Password,
				1,
				GETDATE(),
				IDENT_CURRENT('Users'),
				@UserName,
				@RoleID
                );	
		SELECT '';
    END;

DROP PROCEDURE [dbo].[sp_Accounts_Users_create];


--get user_to_paging
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[sp_user_getUsertoPaging] (@page_index  INT, 
                                       @page_size   INT
									   )
AS
	BEGIN
        DECLARE @RecordCount BIGINT;
        IF(@page_size <> 0)
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY A.AccountID ASC)) AS RowNumber, 
                               A.UserID,
							   U.FullName,
							   U.Gender,
							   U.Thumb,
							   A.UserName,
							   A.Password,
							   R.RoleName
                        INTO #Results1
                        FROM [Accounts] AS A INNER JOIN Users U
						ON A.UserID = U.UserID INNER JOIN Roles R
						ON R.RoleID = A.RoleID		
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results1
                        WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 AND(((@page_index - 1) * @page_size + 1) + @page_size) - 1
                              OR @page_index = -1;
                        DROP TABLE #Results1; 
            END;
            ELSE
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY  A.AccountID ASC)) AS RowNumber, 
                               A.UserID,
							   U.FullName,
							   U.Gender,
							   U.Thumb,
							   A.UserName,
							   A.Password,
							   R.RoleName
                        INTO #Results2
                        FROM [Accounts] AS A INNER JOIN Users U
						ON A.UserID = U.UserID  INNER JOIN Roles R
						ON A.RoleID = R.RoleID
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results2;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results2;
                        DROP TABLE #Results2;
        END;
    END;
    

EXEC [dbo].[sp_user_getUsertoPaging] 2,1
DROP PROCEDURE [dbo].[sp_user_getUsertoPaging];


--Update
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_user_update]
(
	@UserID					 INT,	
	@FullName				 NVARCHAR(200),
	@BirthDay				 DATETIME,
	@Gender				     NVARCHAR(20), 
	@Thumb					 VARCHAR(500), 
	@Address			     NVARCHAR(1500), 
	@Email					 NVARCHAR(100), 
	@Phone				     CHAR(20)
)
AS
    BEGIN
     
		UPDATE  Users 
	    SET  
		FullName = IIf(@FullName is Null, FullName, @FullName),
		BirthDay= IIF(@BirthDay is Null,BirthDay,@BirthDay),
		Gender = IIf(@Gender is Null, Gender, @Gender),
		Thumb = IIf(@Thumb is Null, Thumb, @Thumb),
		Address = IIf(@Address is Null, Address, @Address),
		Email = IIf(@Email is Null, Email, @Email),
		Phone = IIf(@Phone is Null, Phone, @Phone)
		Where UserID=@UserID
		SELECT '';
    END;

EXEC [dbo].[sp_user_update] 3,NULL,NULL,NULL,NULL,N'Hưng yên',NULL,NULL


--GetById
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_get_by_id](@UserID int)
AS
    BEGIN
        SELECT U.UserID,
		U.FullName,
		U.BirthDay,
		U.Gender,
		U.Thumb,
		U.Address,
		U.Email,
		U.Phone,
		A.UserName,
		A.Password,
		R.RoleID
        FROM Users U INNER JOIN Accounts A
		ON U.UserID = A.UserID INNER JOIN Roles R
		ON A.RoleID = R.RoleID
		WHERE  @UserID = U.UserID
    END;

DROP PROCEDURE [dbo].[sp_User_get_by_id];
EXEC [dbo].[sp_User_get_by_id] 2

--Delete
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_user_delete](@UserID int)
AS
    BEGIN
		DELETE FROM Accounts
		WHERE UserID = @UserID 
		DELETE FROM Users
		WHERE UserID = @UserID
    END;

DROP PROCEDURE [dbo].[sp_user_delete];




/*
-------------------------------------------------------------------------------------------------
									ROLES
-------------------------------------------------------------------------------------------------
*/

---GetAll
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Role_GetAll]
AS
    BEGIN
        SELECT *                       
        FROM Roles 
    END;

/*
-------------------------------------------------------------------------------------------------
									CART
-------------------------------------------------------------------------------------------------
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_order_create]
(@khach      NVARCHAR(MAX),  
 @listchitiet NVARCHAR(MAX)
)
AS
    BEGIN
	 IF(@khach IS NOT NULL)
	 Begin
	   INSERT INTO Customers
                (FullName, 
                 Address, 
                 Phone,
				 Email,
				 CreateDate
                )
		 SELECT JSON_VALUE(@khach, '$.FullName'), 
				JSON_VALUE(@khach, '$.Address'), 
				JSON_VALUE(@khach, '$.Phone') ,
				JSON_VALUE(@khach, '$.Email'),
				GETDATE()
	 end;
	 IF(@listchitiet IS NOT NULL)
	 Begin
	    -- Thêm bảng đơn hàng
		INSERT INTO Orders
        (CustomerID, 
            OrderDate, 
            TransactStatusID,
			Deleted,
			Paid
        )
        VALUES
        (IDENT_CURRENT('Customers'), 
            GETDATE(), 
            1,
			0,
			0
        );
		-- Thêm bảng chi tiết đơn hàng
        INSERT INTO OrderDetails
                (   OrderID, 
                    ProductID, 
                    quantity, 
                    Price,
					Discount,
					CreateDate
                )
        SELECT 
			IDENT_CURRENT('Orders'),		
			JSON_VALUE(p.value, '$.ProductId'), 
			JSON_VALUE(p.value, '$.quantity'), 
			JSON_VALUE(p.value, '$.Price'),
			0,
			GETDATE()
        FROM OPENJSON(@listchitiet) AS p;
	end;
    SELECT '';
   END;

DROP PROCEDURE [dbo].[sp_order_create];


---ExportBill
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_exportBill_create]
(@khach      NVARCHAR(50),  
 @listchitiet NVARCHAR(MAX)
)
AS
    BEGIN
	 Declare @CustomerID INT;
	 SET @CustomerID = (SELECT U.CustomerID
	 FROM Customers U
	 WHERE U.Email LIKE @khach);
	 IF(@listchitiet IS NOT NULL)

	 Begin
	    -- Thêm bảng hóa đơn xuất
		INSERT INTO ExportBill
        (BillNumber, 
            ExportDate, 
            CustomerID
        )
        VALUES
        (6688, 
            GETDATE(), 
            @CustomerID
        );
		-- Thêm bảng chi tiết hóa đơn xuất
        INSERT INTO ExportBillDetail
                (   ExportBillID, 
                    ProductID, 
                    Quantity, 
                    Discount,
					Price,
					Status
                )
        SELECT 
			IDENT_CURRENT('ExportBill'),		
			JSON_VALUE(p.value, '$.ProductID'), 
			JSON_VALUE(p.value, '$.Quantity'), 
			0,
			JSON_VALUE(p.value, '$.Price'),
			1
        FROM OPENJSON(@listchitiet) AS p;
	end;
    SELECT '';
   END;
DROP PROCEDURE [dbo].[sp_exportBill_create];




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Carts_get_by_email](@Email NVARCHAR(150),@OrderID INT)
AS
    BEGIN
        SELECT C.FullName,
		C.Address,
		C.Email,
		C.Phone,
		O.OrderDate,
		OD.ProductID,
		(SELECT T.Description
		FROM TransactStatus T 
		WHERE T.Status = O.TransactStatusID) AS StatusOrder,
		(SELECT PT.Name
		FROM ProductTranslation PT 
		WHERE PT.ProductId = OD.ProductID AND PT.LanguageId='vi-VN') AS ProductName,
		(SELECT TOP(1) LI.ImagePath
		FROM ListProductImage LI
		WHERE LI.ProductID = OD.ProductID) AS ProductImage,
		OD.quantity,
		OD.Price,
		OD.TotalMoney
        FROM ((Customers C INNER JOIN Orders O ON C.CustomerID = O.CustomerID)
		INNER JOIN OrderDetails OD ON O.OrderID = OD.OrderID)
		WHERE  (C.Email LIKE @Email) AND (O.OrderID = @OrderID)
    END;
EXEC [dbo].[sp_Carts_get_by_email] 'nguyengiaba@gmail.com',16
DROP PROCEDURE [dbo].[sp_Carts_get_by_email];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Carts_getAll]
AS
    BEGIN
        SELECT C.FullName,
		C.Address,
		C.Email,
		C.Phone,
		O.OrderID,
		O.OrderDate,
		(SELECT T.Description
		FROM TransactStatus T 
		WHERE T.Status = O.TransactStatusID) AS StatusOrder,
		O.Deleted,
		O.Paid,
		O.TotalMoney,
		O.Note
        FROM (Customers C INNER JOIN Orders O ON C.CustomerID = O.CustomerID)
    END;
DROP PROCEDURE [dbo].[sp_Carts_getAll];



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_order_update_status]
(
	@OrderID				 INT
)
AS
    BEGIN
		UPDATE  Orders
	    SET  
		TransactStatusID = 2
		Where OrderID=@OrderID
		SELECT '';
    END;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_order_update_status_user]
(
	@OrderID				 INT
)
AS
    BEGIN
		UPDATE  Orders
	    SET  
		TransactStatusID = 3
		Where OrderID=@OrderID
		SELECT '';
    END;


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Cart_getEmail_User](@UserName NVARCHAR(150))
AS
    BEGIN
        SELECT U.Email
		FROM Users U INNER JOIN Accounts A
		ON U.UserID = A.UserID
		WHERE A.UserName LIKE @UserName
    END;
EXEC [dbo].[sp_Cart_getEmail_User] 'vinamilk'
DROP PROCEDURE [dbo].[sp_Cart_getEmail_User];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Cart_getOrder_Use](@Email NVARCHAR(150))
AS
    BEGIN
        SELECT C.FullName,
		C.Address,
		C.Email,
		C.Phone,
		O.OrderDate,
		(SELECT T.Description
		FROM TransactStatus T 
		WHERE T.Status = O.TransactStatusID) AS StatusOrder,
		O.OrderID
        FROM (Customers C INNER JOIN Orders O ON C.CustomerID = O.CustomerID)
		WHERE  C.Email LIKE @Email
    END;
EXEC [dbo].[sp_Cart_getOrder_Use] 'nhatnguyen20102002@gmail.com'
DROP PROCEDURE [dbo].[sp_Cart_getOrder_Use];

	


		



		


		

		










		
















		
















 