using LuxuryShop.Application.Common;
using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.Utilities.Exceptions;
using LuxuryShop.ViewModels.Catalog.ProductImages;
using LuxuryShop.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly ITools _tools;

        public ManageProductService(IDatabaseHelper dbHelper, ITools tools)
        {
            _dbHelper = dbHelper;
            _tools = tools;
        }

        public int Create(ProductCreateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_create",
                "@CatID", request.CatID,
                "@Discount", request.Discount,
                "@UnitslnStock", request.UnitslnStock,
                "@Name",request.Name,
                "@Details",request.Details,
                "@Description",request.Description,
                "@SeoDescription",request.SeoDescription,
                "@Title",request.Title,
                "@SeoTitle",request.SeoTitle,
                "@LanguageId",request.LanguageId,
                "@SeoAlias",request.SeoAlias,
                "@Price",request.Price);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new LuxuryShopException(Convert.ToString(result) + msgError);
                }

                //Save Image
                if (request.ThumbnailImage != null)
                {
                    string msgErrorImg = "";
                    try
                    {
                        var resultImg = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgErrorImg, "sp_ListProductImage_create",
                            "@ImagePath", this.SaveFile(request.ThumbnailImage, "Products"),
                            "@FizeSize", request.ThumbnailImage.Length);
                        if ((resultImg != null && !string.IsNullOrEmpty(resultImg.ToString())) || !string.IsNullOrEmpty(msgErrorImg))
                        {
                            throw new LuxuryShopException(Convert.ToString(resultImg) + msgErrorImg);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }



                string msgErrorPr = "";
                try
                {
                    var query = "SELECT TOP(1) ProductID AS Id FROM Products ORDER BY ProductID DESC";
                    DataTable dt = _dbHelper.ExecuteQueryToDataTable(query, out msgErrorPr);
                    if (!string.IsNullOrEmpty(msgErrorPr))
                    {
                        throw new LuxuryShopException(Convert.ToString(msgErrorPr));
                    }
                    List<int> productId = new List<int>();
                    foreach (DataRow row in dt.Rows)
                    {
                        productId.Add((int)row[0]);
                    }
                    return productId[0];
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public int Delete(int productId)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_delete",
                "@productId", productId);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductViewModel> GetAllPaging(int pageIndex, int pageSize, out long total)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_getAlltoPaging",
                    "@page_index", pageIndex,
                    "@page_size", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<ProductViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductViewModel GetById(int productId, string languageId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_get_by_id",
                     "@productId", productId,
                     "@languageId", languageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductViewModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(ProductUpdateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update",
                "@ProductId", request.Id,
                "@LanguageId", request.LanguageId,
                "@Name", request.Name,
                "@SeoAlias", request.SeoAlias,
                "@SeoDescription", request.SeoDescription,
                "@SeoTitle", request.SeoTitle,
                "@Description",request.Description,
                "@Details",request.Details);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePrice(int productId, decimal newPrice)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update_price",
                "@ProductId", productId,
                "@newPrice", newPrice);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStock(int productId, int addedQuantity)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update_stock",
                "@ProductId", productId,
                "@addQuantity",addedQuantity);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string SaveFile(IFormFile file, string folder)
        {
            string filePath = $"{folder}/{file.FileName.Replace("-", "_").Replace("%", "")}";
            var fullPath = _tools.CreatePathFile(filePath);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public int AddImage(int productId, ProductImageCreateRequest request)
        {
            if (request.ImageFile != null)
            {
                string msgErrorImg = "";
                try
                {
                    var resultImg = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgErrorImg, "sp_ListProductImage_AddImage",
                        "@ProductID",productId,
                        "@Caption",request.Caption,
                        "@IsDefault",request.IsDefault,
                        "@DateCreated",DateTime.Now,
                        "@SortOrder",request.SortOrder,
                        "@ImagePath", this.SaveFile(request.ImageFile, "Products"),
                        "@FizeSize", request.ImageFile.Length);
                    if ((resultImg != null && !string.IsNullOrEmpty(resultImg.ToString())) || !string.IsNullOrEmpty(msgErrorImg))
                    {
                        throw new LuxuryShopException(Convert.ToString(resultImg) + msgErrorImg);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            string msgErrorPr = "";
            try
            {
                var query = "SELECT TOP(1) ListProductImageID AS Id FROM ListProductImage ORDER BY ListProductImageID DESC";
                DataTable dt = _dbHelper.ExecuteQueryToDataTable(query, out msgErrorPr);
                if (!string.IsNullOrEmpty(msgErrorPr))
                {
                    throw new LuxuryShopException(Convert.ToString(msgErrorPr));
                }
                List<int> listProductImageID = new List<int>();
                foreach (DataRow row in dt.Rows)
                {
                    listProductImageID.Add((int)row[0]);
                }


                return listProductImageID[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RemoveImage(int imageId)
        {
            string msgError = "";
            try
            {
                var image = this.GetImageById(imageId);
                string filePath = $"{image.ImagePath}";
                var fullPath = _tools.CreatePathFile(filePath);
                File.Delete(fullPath);
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_listProductImage_delete",
                "@imageId", imageId);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateImage(int imageId, ProductImageUpdateReques request)
        {
            string msgError = "";
            try
            {
                if(request.ImageFile != null)
                {
                    var image = this.GetImageById(imageId);
                    string filePath = $"{image.ImagePath}";
                    var fullPath = _tools.CreatePathFile(filePath);
                    File.Delete(fullPath);
                };
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_listproductImage_update",
                "@ListProductImageID", imageId,
                "@ImagePath", this.SaveFile(request.ImageFile, "Products"),
                "@Caption", request.Caption,
                "@IsDefault", request.IsDefault,
                "@SortOrder", request.SortOrder,
                "@FileSize", request.ImageFile.Length);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductImageViewModel GetImageById(int imageId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_listProductImage_get_by_id",
                     "@imageId", imageId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductImageViewModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductImageViewModel> GetListImages(int productId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_listProductImage_getAll",
                     "@ProductId", productId);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductImageViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
