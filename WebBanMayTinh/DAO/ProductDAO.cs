using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WebBanMayTinh.EF;
using WebBanMayTinh.Models;

namespace WebBanMayTinh.DAO
{
    public class ProductDAO
    {
        QLBanMayTinhDbContext db = null;
        public ProductDAO()
        {
            db = new QLBanMayTinhDbContext();
        }

        /// <summary>
        /// Lấy danh sách sản phẩm nổi bật
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListHotProduct(int top)
        {
            return db.Products.Take(top).ToList();
        }

        /// <summary>
        /// Lấy danh sách Laptop
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> ListLaptop(int top)
        {
            var listLaptop = (from p in db.Products
                              where p.CategoryID == 1
                              select new ProductModel()
                              {
                                  ID = p.ID,
                                  Name = p.Name,
                                  MetaTitle = p.MetaTitle,
                                  Images = p.Images,
                                  MoreImages = p.MoreImages,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Descriptions = p.Descriptions,
                                  Detail = p.Detail,
                                  TopHot = p.TopHot,
                                  CreatedDated = p.CreatedDate,
                                  Status = p.Status,
                                  ProducerID = p.ProducerID,
                                  CategoryID = p.CategoryID
                              });
            return listLaptop.Take(top).ToList();
        }

        /// <summary>
        /// Lấy danh sách PC
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> ListPC(int top)
        {
            var listPC = (from p in db.Products
                              where p.CategoryID == 2
                              select new ProductModel()
                              {
                                  ID = p.ID,
                                  Name = p.Name,
                                  MetaTitle = p.MetaTitle,
                                  Images = p.Images,
                                  MoreImages = p.MoreImages,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Descriptions = p.Descriptions,
                                  Detail = p.Detail,
                                  TopHot = p.TopHot,
                                  CreatedDated = p.CreatedDate,
                                  Status = p.Status,
                                  ProducerID = p.ProducerID,
                                  CategoryID = p.CategoryID
                              });
            return listPC.Take(top).ToList();
        }

        /// <summary>
        /// Lấy danh sách Chuột
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> ListChuot(int top)
        {
            var listChuot = (from p in db.Products
                          where p.CategoryID == 3
                          select new ProductModel()
                          {
                              ID = p.ID,
                              Name = p.Name,
                              MetaTitle = p.MetaTitle,
                              Images = p.Images,
                              MoreImages = p.MoreImages,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              Descriptions = p.Descriptions,
                              Detail = p.Detail,
                              TopHot = p.TopHot,
                              CreatedDated = p.CreatedDate,
                              Status = p.Status,
                              ProducerID = p.ProducerID,
                              CategoryID = p.CategoryID
                          });
            return listChuot.Take(top).ToList();
        }

        /// <summary>
        /// Lấy danh sách Bàn phím
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> ListBanPhim(int top)
        {
            var listBanPhim = (from p in db.Products
                          where p.CategoryID == 4
                          select new ProductModel()
                          {
                              ID = p.ID,
                              Name = p.Name,
                              MetaTitle = p.MetaTitle,
                              Images = p.Images,
                              MoreImages = p.MoreImages,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              Descriptions = p.Descriptions,
                              Detail = p.Detail,
                              TopHot = p.TopHot,
                              CreatedDated = p.CreatedDate,
                              Status = p.Status,
                              ProducerID = p.ProducerID,
                              CategoryID = p.CategoryID
                          });
            return listBanPhim.Take(top).ToList();
        }

        /// <summary>
        /// Lấy danh sách Tai nghe
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> ListTaiNghe(int top)
        {
            var listTaiNghe = (from p in db.Products
                          where p.CategoryID == 5
                          select new ProductModel()
                          {
                              ID = p.ID,
                              Name = p.Name,
                              MetaTitle = p.MetaTitle,
                              Images = p.Images,
                              MoreImages = p.MoreImages,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              Descriptions = p.Descriptions,
                              Detail = p.Detail,
                              TopHot = p.TopHot,
                              CreatedDated = p.CreatedDate,
                              Status = p.Status,
                              ProducerID = p.ProducerID,
                              CategoryID = p.CategoryID
                          });
            return listTaiNghe.Take(top).ToList();
        }
        /// <summary>
        /// Lấy sản phẩm theo id danh mục(có phân trang)
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryID(int categoryID, ref int totalRecord, int pageIndex, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model =  db.Products.Where(x => x.CategoryID == categoryID).OrderBy(x => x.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        /// <summary>
        /// Chi tiết sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel ProductDetail(int id)
        {
            var detail =
                from p in db.Products
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Images = p.Images,
                    MoreImages = p.MoreImages,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Descriptions = p.Descriptions,
                    Detail = p.Detail,
                    CategoryID = p.CategoryID,
                    Producer = p.Producer.Name,
                    ProductCategory = p.ProductCategory.Name,
                };
            var model = detail.SingleOrDefault(x => x.ID == id);
            return model;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm có liên quan
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> ProductRelated(int top)
        {
            var product = ViewDetail(top);
            var related = (
                from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate)
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Images = p.Images,
                    MetaTitle = p.MetaTitle,
                    Detail = p.Detail,
                    Quantity = p.Quantity,
                    MoreImages = p.MoreImages,
                    CreatedDated = p.CreatedDate,
                    ProductCategory = p.CategoryID.ToString(),
                    Price = p.Price
                }).ToList();

            var model = related.Where(x => x.ID != top && x.ProductCategory == product.CategoryID.ToString()).Take(10);
            return model;
        }

        /// <summary>
        /// Lấy list sản phẩm gợi ý ở form tìm kiếm
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ProductModel> GetProductNew(int top)
        {
            var products = (
                from p in db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate)
                select new ProductModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Images = p.Images,
                    Price = p.Price,
                    Quantity = p.Quantity,
                });

            return products.Take(top).ToList();
        }
    }
}