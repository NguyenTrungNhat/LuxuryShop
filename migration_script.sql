-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: dbMarkets
-- Source Schemata: dbMarkets
-- Created: Mon Apr 10 21:28:03 2023
-- Workbench Version: 8.0.32
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema dbMarkets
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `dbMarkets` ;
CREATE SCHEMA IF NOT EXISTS `dbMarkets` ;

-- ----------------------------------------------------------------------------
-- Table dbMarkets.CheckWHDetail
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`CheckWHDetail` (
  `CheckWHDetailID` INT NOT NULL,
  `ProductID` INT NULL,
  `CheckWH` INT NULL,
  `QuantityCount` INT NULL,
  `QuantityCalculate` INT NULL,
  `QuantityChange` INT NULL,
  PRIMARY KEY (`CheckWHDetailID`),
  CONSTRAINT `FK_CheckWHDetail_CheckWHD`
    FOREIGN KEY (`CheckWH`)
    REFERENCES `dbMarkets`.`CheckWH` (`CheckID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_CheckWHDetail_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Languages
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Languages` (
  `Id` VARCHAR(5) NOT NULL,
  `Name` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  `IsDefault` TINYINT(1) NOT NULL,
  PRIMARY KEY (`Id`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Categories
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Categories` (
  `CatID` INT NOT NULL,
  `ParentID` INT NULL,
  `SortOrder` INT NOT NULL,
  `IsShowOnHome` TINYINT(1) NOT NULL,
  `Status` INT NOT NULL,
  PRIMARY KEY (`CatID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.CategoryTranslation
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`CategoryTranslation` (
  `Id` INT NOT NULL,
  `CatID` INT NOT NULL,
  `SeoDescription` VARCHAR(500) CHARACTER SET 'utf8mb4' NULL,
  `SeoTitle` VARCHAR(200) CHARACTER SET 'utf8mb4' NULL,
  `LanguageId` VARCHAR(5) NOT NULL,
  `SeoAlias` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  `Name` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_CategoryTranslation_Category`
    FOREIGN KEY (`CatID`)
    REFERENCES `dbMarkets`.`Categories` (`CatID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `FK_CategoryTranslation_Language`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `dbMarkets`.`Languages` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Products
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Products` (
  `ProductID` INT NOT NULL,
  `CatID` INT NULL,
  `Discount` INT NULL,
  `DateCreated` DATETIME NULL,
  `DateModified` DATETIME NULL,
  `BestSellers` TINYINT(1) NOT NULL,
  `HomeFlag` TINYINT(1) NOT NULL,
  `Active` TINYINT(1) NOT NULL,
  `Title` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `UnitsInStock` INT NULL,
  `SeoAlias` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`ProductID`),
  CONSTRAINT `FK_Products_Categories`
    FOREIGN KEY (`CatID`)
    REFERENCES `dbMarkets`.`Categories` (`CatID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ProductTranslation
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ProductTranslation` (
  `Id` INT NOT NULL,
  `ProductId` INT NOT NULL,
  `Name` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  `Description` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `Details` VARCHAR(500) CHARACTER SET 'utf8mb4' NULL,
  `SeoDescription` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `SeoTitle` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `LanguageId` VARCHAR(5) NOT NULL,
  `SeoAlias` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_ProductTranslation_Product`
    FOREIGN KEY (`ProductId`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProductTranslation_Language`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `dbMarkets`.`Languages` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.AttributesPrices
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`AttributesPrices` (
  `AttributesPriceID` INT NOT NULL,
  `ProductID` INT NULL,
  `Price` DECIMAL(18,2) NULL,
  `Active` TINYINT(1) NOT NULL,
  PRIMARY KEY (`AttributesPriceID`),
  CONSTRAINT `FK_AttributesPrices_Products`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Locations
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Locations` (
  `LocationID` INT NOT NULL,
  `Name` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Parent` INT NULL,
  `Levels` INT NULL,
  `Slug` VARCHAR(100) CHARACTER SET 'utf8mb4' NULL,
  `NameWithType` VARCHAR(100) CHARACTER SET 'utf8mb4' NULL,
  `Type` VARCHAR(10) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`LocationID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.OrderDetails
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`OrderDetails` (
  `OrderDetailID` INT NOT NULL,
  `OrderID` INT NULL,
  `ProductID` INT NULL,
  `OrderNumber` INT NULL,
  `Amount` INT NULL,
  `Discount` BIGINT NULL,
  `TotalMoney` BIGINT NULL,
  `CreateDate` DATETIME NULL,
  `Price` BIGINT NULL,
  PRIMARY KEY (`OrderDetailID`),
  CONSTRAINT `FK_OrderDetails_Products`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderDetails_Orders`
    FOREIGN KEY (`OrderID`)
    REFERENCES `dbMarkets`.`Orders` (`OrderID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Pages
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Pages` (
  `PageID` INT NOT NULL,
  `PageName` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Contents` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `Thumb` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Published` TINYINT(1) NOT NULL,
  `Title` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Alias` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `CreatedDate` DATETIME NULL,
  `Ordering` INT NULL,
  PRIMARY KEY (`PageID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.QuangCaos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`QuangCaos` (
  `QuangCaoID` INT NOT NULL,
  `SubTitle` VARCHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `Title` VARCHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `ImageBG` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `ImageProduct` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `UrlLink` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Active` TINYINT(1) NOT NULL,
  `CreateDate` DATETIME NULL,
  PRIMARY KEY (`QuangCaoID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Shippers
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Shippers` (
  `ShipperID` INT NOT NULL,
  `ShipperName` VARCHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `Phone` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `Company` VARCHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `ShipDate` DATETIME NULL,
  PRIMARY KEY (`ShipperID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.TinDangs
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`TinDangs` (
  `PostID` INT NOT NULL,
  `Title` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `SContents` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Contents` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `Thumb` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Published` TINYINT(1) NOT NULL,
  `Alias` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `CreatedDate` DATETIME NULL,
  `Author` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `AccountID` INT NULL,
  `Tags` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `CatID` INT NULL,
  `isHot` TINYINT(1) NOT NULL,
  `isNewfeed` TINYINT(1) NOT NULL,
  `MetaKey` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `MetaDesc` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Views` INT NULL,
  PRIMARY KEY (`PostID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.TransactStatus
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`TransactStatus` (
  `TransactStatusID` INT NOT NULL,
  `Status` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Description` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`TransactStatusID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.sysdiagrams
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`sysdiagrams` (
  `name` VARCHAR(160) NOT NULL,
  `principal_id` INT NOT NULL,
  `diagram_id` INT NOT NULL,
  `version` INT NULL,
  `definition` LONGBLOB NULL,
  PRIMARY KEY (`diagram_id`),
  UNIQUE INDEX `UK_principal_name` (`principal_id` ASC, `name` ASC) VISIBLE);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ListProductImage
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ListProductImage` (
  `ListProductImageID` INT NOT NULL,
  `ProductID` INT NULL,
  `ImagePath` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Caption` VARCHAR(200) CHARACTER SET 'utf8mb4' NULL,
  `IsDefault` TINYINT(1) NULL,
  `DateCreated` DATETIME NULL,
  `SortOrder` INT NULL,
  `FileSize` BIGINT NULL,
  PRIMARY KEY (`ListProductImageID`),
  CONSTRAINT `FK_ListProductImage_Products`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Accounts
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Accounts` (
  `AccountID` INT NOT NULL,
  `Password` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Salt` CHAR(10) CHARACTER SET 'utf8mb4' NULL,
  `Active` TINYINT(1) NOT NULL,
  `LastLogin` DATETIME NULL,
  `CreateDate` DATETIME NULL,
  `UserID` INT NULL,
  `UserName` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `RoleID` INT NULL,
  PRIMARY KEY (`AccountID`),
  CONSTRAINT `FK_Accounts_User`
    FOREIGN KEY (`UserID`)
    REFERENCES `dbMarkets`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Roles`
    FOREIGN KEY (`RoleID`)
    REFERENCES `dbMarkets`.`Roles` (`RoleID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Users
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Users` (
  `UserID` INT NOT NULL,
  `BirthDay` DATETIME NULL,
  `Gender` VARCHAR(20) CHARACTER SET 'utf8mb4' NULL,
  `Thumb` VARCHAR(500) NULL,
  `Address` VARCHAR(1500) CHARACTER SET 'utf8mb4' NULL,
  `Email` VARCHAR(100) CHARACTER SET 'utf8mb4' NULL,
  `Phone` CHAR(20) NULL,
  `Status` TINYINT(1) NULL,
  `FullName` VARCHAR(200) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`UserID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Roles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Roles` (
  `RoleID` INT NOT NULL,
  `RoleName` VARCHAR(50) CHARACTER SET 'utf8mb4' NOT NULL,
  `Description` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`RoleID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Specifications
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Specifications` (
  `SpecificationID` INT NOT NULL,
  `ProductID` INT NULL,
  `SpecificationName` VARCHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `Description` VARCHAR(500) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`SpecificationID`),
  CONSTRAINT `FK_Specifications_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.PriceHistory
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`PriceHistory` (
  `PriceID` INT NOT NULL,
  `ProductID` INT NULL,
  `StartDay` DATETIME NULL,
  `EndDay` DATETIME NULL,
  `Price` DOUBLE NULL,
  PRIMARY KEY (`PriceID`),
  CONSTRAINT `FK_PriceHistory_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Promotions
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Promotions` (
  `PromotionsID` INT NOT NULL,
  `PromotionsName` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Description` LONGTEXT NULL,
  PRIMARY KEY (`PromotionsID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.PromotionsDetail
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`PromotionsDetail` (
  `PromotionsDetailID` INT NOT NULL,
  `ProductID` INT NULL,
  `StartDay` DATETIME NULL,
  `EndDay` DATETIME NULL,
  `PromotionsID` INT NULL,
  `Status` TINYINT(1) NULL,
  PRIMARY KEY (`PromotionsDetailID`),
  CONSTRAINT `FK_PromotionsDetail_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_PromotionsDetail_Promotions`
    FOREIGN KEY (`PromotionsID`)
    REFERENCES `dbMarkets`.`Promotions` (`PromotionsID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Sale
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Sale` (
  `SaleID` INT NOT NULL,
  `ProductID` INT NULL,
  `Percentt` DOUBLE NULL,
  `StartDay` DATETIME NULL,
  `EndDay` DATETIME NULL,
  `Status` TINYINT(1) NULL,
  PRIMARY KEY (`SaleID`),
  CONSTRAINT `FK_Sale_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Customers
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Customers` (
  `CustomerID` INT NOT NULL,
  `FullName` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Birthday` DATETIME NULL,
  `Avatar` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Address` VARCHAR(255) CHARACTER SET 'utf8mb4' NULL,
  `Email` CHAR(150) CHARACTER SET 'utf8mb4' NULL,
  `Phone` VARCHAR(12) NULL,
  `LocationID` INT NULL,
  `District` INT NULL,
  `Ward` INT NULL,
  `CreateDate` DATETIME NULL,
  `Password` VARCHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Salt` CHAR(8) CHARACTER SET 'utf8mb4' NULL,
  `LastLogin` DATETIME NULL,
  `Active` TINYINT(1) NOT NULL,
  PRIMARY KEY (`CustomerID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ExportBill
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ExportBill` (
  `ID` INT NOT NULL,
  `BillNumber` CHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `ExportDate` DATE NULL,
  `CustomerID` INT NULL,
  PRIMARY KEY (`ID`),
  CONSTRAINT `FK_ExportBill_Customer`
    FOREIGN KEY (`CustomerID`)
    REFERENCES `dbMarkets`.`Customers` (`CustomerID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Orders
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Orders` (
  `OrderID` INT NOT NULL,
  `CustomerID` INT NULL,
  `OrderDate` DATETIME NULL,
  `ShipDate` DATETIME NULL,
  `TransactStatusID` INT NOT NULL,
  `Deleted` TINYINT(1) NOT NULL,
  `Paid` TINYINT(1) NOT NULL,
  `PaymentDate` DATETIME NULL,
  `TotalMoney` INT NOT NULL,
  `PaymentID` INT NULL,
  `Note` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  `Address` VARCHAR(0) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`OrderID`),
  CONSTRAINT `FK_Orders_Customers`
    FOREIGN KEY (`CustomerID`)
    REFERENCES `dbMarkets`.`Customers` (`CustomerID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Orders_TransactStatus`
    FOREIGN KEY (`TransactStatusID`)
    REFERENCES `dbMarkets`.`TransactStatus` (`TransactStatusID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ImportBill
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ImportBill` (
  `ImportBillID` INT NOT NULL,
  `BillNumber` CHAR(50) CHARACTER SET 'utf8mb4' NULL,
  `Date` DATETIME NULL,
  `UserID` INT NULL,
  `SupplierID` INT NULL,
  PRIMARY KEY (`ImportBillID`),
  CONSTRAINT `FK_ImportBill_Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `dbMarkets`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ImportBill_Supplier`
    FOREIGN KEY (`SupplierID`)
    REFERENCES `dbMarkets`.`Supplier` (`SupplierID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.Supplier
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`Supplier` (
  `SupplierID` INT NOT NULL,
  `SupplierName` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Address` VARCHAR(500) CHARACTER SET 'utf8mb4' NULL,
  `Phone` CHAR(20) NULL,
  `Email` CHAR(50) NULL,
  PRIMARY KEY (`SupplierID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ExportBillDetail
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ExportBillDetail` (
  `ID` INT NOT NULL,
  `ExportBillID` INT NULL,
  `ProductID` INT NULL,
  `Quantity` INT NULL,
  `Discount` DOUBLE NULL,
  `Price` DOUBLE NULL,
  `Status` INT NULL,
  PRIMARY KEY (`ID`),
  CONSTRAINT `FK_ExportBillDetail_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `FK_ExportBillDetail_ExportBill`
    FOREIGN KEY (`ExportBillID`)
    REFERENCES `dbMarkets`.`ExportBill` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.ImportBillDetail
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`ImportBillDetail` (
  `IBDID` INT NOT NULL,
  `ProductID` INT NULL,
  `ImportBillID` INT NULL,
  `Quantity` INT NULL,
  `Price` DOUBLE NULL,
  PRIMARY KEY (`IBDID`),
  CONSTRAINT `FK_ImportBillDetail_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ImportBillDetail_ImportBill`
    FOREIGN KEY (`ImportBillID`)
    REFERENCES `dbMarkets`.`ImportBill` (`ImportBillID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.WareHouse
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`WareHouse` (
  `WareHouseID` INT NOT NULL,
  `WareHouseName` VARCHAR(250) CHARACTER SET 'utf8mb4' NULL,
  `Address` VARCHAR(500) CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`WareHouseID`));

-- ----------------------------------------------------------------------------
-- Table dbMarkets.CheckWH
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`CheckWH` (
  `CheckID` INT NOT NULL,
  `UserID` INT NULL,
  `StartDay` DATETIME NULL,
  `EndDay` DATETIME NULL,
  `Status` INT NULL,
  `WareHouseID` INT NULL,
  `Description` LONGTEXT NULL,
  PRIMARY KEY (`CheckID`),
  CONSTRAINT `FK_CheckWH_Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `dbMarkets`.`Users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_CheckWH_WareHouse`
    FOREIGN KEY (`WareHouseID`)
    REFERENCES `dbMarkets`.`WareHouse` (`WareHouseID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table dbMarkets.WareHouseDetail
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `dbMarkets`.`WareHouseDetail` (
  `WareHouseDetailID` INT NOT NULL,
  `WareHouseID` INT NULL,
  `ProductID` INT NULL,
  `Quantity` INT NULL,
  PRIMARY KEY (`WareHouseDetailID`),
  CONSTRAINT `FK_WareHouseDetail_Product`
    FOREIGN KEY (`ProductID`)
    REFERENCES `dbMarkets`.`Products` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_WareHouseDetail_WareHouse`
    FOREIGN KEY (`WareHouseID`)
    REFERENCES `dbMarkets`.`WareHouse` (`WareHouseID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
SET FOREIGN_KEY_CHECKS = 1;
