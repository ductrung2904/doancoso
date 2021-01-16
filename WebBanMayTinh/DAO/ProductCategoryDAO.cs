using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanMayTinh.EF;

namespace WebBanMayTinh.DAO
{
    public class ProductCategoryDAO
    {
        QLBanMayTinhDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new QLBanMayTinhDbContext();
        }

        /// <summary>
        /// Lấy list loại sản phẩm theo id
        /// </summary>
        /// <returns></returns>
        public List<ProductCategory> ListByGroupId()
        {
            return db.ProductCategories.Where(x => x.Status == true).ToList();
        }

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }

        /// <summary>
        /// Hiển thị list sản phẩm được tìm kiếm theo từ khóa
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<ProductCategory> GetAll(string searchString)
        {
            IQueryable<ProductCategory> model = db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model;
        }
    }
}