using JuiceTip_API.Data;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static JuiceTip_API.Output.AllProductOutput;
using Microsoft.IdentityModel.Tokens;
using static JuiceTip_API.Output.ProductProgressOutput;
using JuiceTip_API.Factories;

namespace JuiceTip_API.Helper
{
    public class ProductHelper
    {
        private JuiceTipDBContext _dbContext;
        public ProductHelper(JuiceTipDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private Guid GetCategoryId(string targetCategory)
        {
            var category = _dbContext.MsCategory.Where(x => x.Category == targetCategory).FirstOrDefault();

            if(category != null)
            {
                return category.CategoryId;
            }

            var newCategory = new MsCategory
            {
                CategoryId = Guid.NewGuid(),
                Category = targetCategory
            };

            _dbContext.MsCategory.Add(newCategory);
            _dbContext.SaveChanges();

            return newCategory.CategoryId;
        }

        public StatusOutput DeleteProductById(Guid productId)
        {
            var returnValue = new StatusOutput();
            var product = _dbContext.MsProduct.Where(x => x.ProductId == productId).FirstOrDefault();
            
            if (product == null)
            {
                returnValue.statusCode = 404;
                returnValue.message = "Product not found";
                return returnValue;
            }

            _dbContext.MsProduct.Remove(product);
            _dbContext.SaveChanges();

            returnValue.statusCode = 200;
            returnValue.message = "Product deleted";
            return returnValue;
        }

        public AllProductModel GetProductById([FromBody] ProductByIdRequest product) 
        {
            var data = (from products in _dbContext.MsProduct.Where(x => x.ProductId == product.ProductId)
                        join user in _dbContext.MsUser
                        on products.CustomerId equals user.UserId

                        join region in _dbContext.MsRegion
                        on products.RegionId equals region.RegionId

                        join category in _dbContext.MsCategory
                        on products.CategoryId equals category.CategoryId
                        select new AllProductModel
                        {
                            ProductId = products.ProductId,
                            ProductImage = products.ProductImage,
                            ProductName = products.ProductName,
                            ProductDescription = products.ProductDescription,
                            ProductPrice = products.ProductPrice,
                            CategoryId = products.CategoryId,
                            CategoryName = category.Category,
                            RegionId = products.RegionId,
                            RegionName = region.Region,
                            CustomerId = user.UserId,
                            CustomerName = user.FirstName + " " + user.LastName,
                            Notes = products.Notes,
                            CreatedAt = products.CreatedAt,
                            LastUpdatedAt = products.LastUpdatedAt
                        }).FirstOrDefault();

            return data;
        }

        public List<AllProductModel> GetProducts()
        {
            try
            {
                var allData = (from product in _dbContext.MsProduct.Where(x => x.TransactionId == null)
                               join user in _dbContext.MsUser
                               on product.CustomerId equals user.UserId

                               join region in _dbContext.MsRegion
                               on product.RegionId equals region.RegionId

                               join category in _dbContext.MsCategory
                               on product.CategoryId equals category.CategoryId

                               select new AllProductModel
                               {
                                   ProductId = product.ProductId,
                                   ProductImage = product.ProductImage,
                                   ProductName = product.ProductName,
                                   ProductDescription = product.ProductDescription,
                                   ProductPrice = product.ProductPrice,
                                   CategoryId = product.CategoryId,
                                   CategoryName = category.Category,
                                   RegionId = product.RegionId,
                                   RegionName = region.Region,
                                   CustomerId = user.UserId,
                                   CustomerName = user.FirstName + " " + user.LastName,
                                   Notes = product.Notes,
                                   CreatedAt = product.CreatedAt,
                                   LastUpdatedAt = product.LastUpdatedAt
                               }).ToList();
                return allData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MsProduct GetCurrentProduct(ProductRequest product)
        {
            var currentProduct = _dbContext.MsProduct.Where(x => x.ProductId == product.ProductId).FirstOrDefault();

            if (currentProduct != null) return currentProduct;

            return null;
        }

        public MsProduct GetDuplicateProduct(ProductRequest product)
        {
            var duplicateProduct = _dbContext.MsProduct.Where(x => x.ProductName == product.ProductName && x.CustomerId == product.CustomerId).FirstOrDefault();

            if (duplicateProduct != null) return duplicateProduct;

            return null;
        }

        public List<ProductProgressModel> GetAllProductProgress([FromBody] UserRequest user)
        {
            var data = (from product in _dbContext.MsProduct.Where(x => x.CustomerId == user.UserId && x.TransactionId != null)
                        join transaction in _dbContext.TransactionDetail
                        on product.TransactionId equals transaction.TransactionId

                        join region in _dbContext.MsRegion
                        on product.RegionId equals region.RegionId

                        join category in _dbContext.MsCategory
                        on product.CategoryId equals category.CategoryId

                        join usr in _dbContext.MsUser
                        on transaction.JustiperId equals usr.UserId

                        select new ProductProgressModel
                        {
                            ProductId = product.ProductId,
                            ProductImage = product.ProductImage,
                            ProductName = product.ProductName,
                            ProductDescription = product.ProductDescription,
                            ProductPrice = product.ProductPrice,
                            CategoryId = product.CategoryId,
                            CategoryName = category.Category,
                            RegionId = product.RegionId,
                            RegionName = region.Region,
                            Notes = product.Notes,
                            CreatedAt = product.CreatedAt,
                            LastUpdatedAt = product.LastUpdatedAt,
                            JustiperId = usr.UserId,
                            JustiperName = usr.FirstName + " " + usr.LastName,
                            Status = transaction.TransactionStatus
                        }).ToList();

            return data;
        }

        public MsProduct UpsertProduct([FromBody] ProductRequest product)
        {
            try
            {
                var currentProduct = GetCurrentProduct(product);

                if(currentProduct != null)
                {
                    currentProduct.ProductId = product.ProductId;
                    currentProduct.ProductImage = product.ProductImage;
                    currentProduct.ProductName = product.ProductName;
                    currentProduct.ProductDescription = product.ProductDescription;
                    currentProduct.ProductPrice = Math.Round(product.ProductPrice, 7);
                    currentProduct.CategoryId = product.CategoryId;
                    currentProduct.CustomerId = product.CustomerId;
                    currentProduct.RegionId = product.RegionId;
                    currentProduct.Notes = product.Notes;
                    currentProduct.LastUpdatedAt = DateTime.Now;
                    _dbContext.SaveChanges();

                    return currentProduct;
                }
                else
                {
                    var duplicateProduct = GetDuplicateProduct(product);

                    if (duplicateProduct == null)
                    {
                        var productFactory = new ProductFactory();
                        var newProduct = productFactory.CreateProduct(product);

                        _dbContext.MsProduct.Add(newProduct);
                        _dbContext.SaveChanges();

                        return newProduct;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
