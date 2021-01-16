using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanMayTinh.EF;
using WebBanMayTinh.DAO;
using System.Xml.Linq;

namespace WebBanMayTinh.Controllers
{
    public class ProductController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }

        //Danh sách Laptop
        [ChildActionOnly]
        public PartialViewResult NewLaptop()
        {
            ProductDAO db = new ProductDAO();
            var model = db.ListLaptop(8);
            return PartialView(model);
        }

        //Danh sách PC
        [ChildActionOnly]
        public PartialViewResult NewPC()
        {
            ProductDAO db = new ProductDAO();
            var model = db.ListPC(10);
            return PartialView(model);
        }

        //Danh sách Chuột
        [ChildActionOnly]
        public PartialViewResult NewChuot()
        {
            ProductDAO db = new ProductDAO();
            var model = db.ListChuot(6);
            return PartialView(model);
        }

        //Danh sách Bàn phím
        [ChildActionOnly]
        public PartialViewResult NewBanPhim()
        {
            ProductDAO db = new ProductDAO();
            var model = db.ListBanPhim(6);
            return PartialView(model);
        }

        //Danh sách Tai nghe
        [ChildActionOnly]
        public PartialViewResult NewTaiNghe()
        {
            ProductDAO db = new ProductDAO();
            var model = db.ListTaiNghe(6);
            return PartialView(model);
        }

        //Danh sách Sản phẩm liên quan
        [ChildActionOnly]
        public ActionResult ProductRelated(int id)
        {
            ProductDAO db = new ProductDAO();
            var model = db.ProductRelated(id);
            return PartialView(model);
        }

        /// <summary>
        /// Trang sản phẩm theo danh mục(có phân trang)
        /// </summary>
        /// <param name="cateId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult ProductCategory(int cateId, int page = 1, int pageSize = 15)
        {
            var category = new ProductCategoryDAO().ViewDetail(cateId);
            ViewBag.ProductCategory = category;
            int totalRecord = 0;
            var model = new ProductDAO().ListByCategoryID(cateId, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize)) + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        //Trang chi tiết sản phẩm
        public ActionResult ProductDetail(int id)
        {
            var model = new ProductDAO().ProductDetail(id);
            List<string> listImages = new List<string>();
            string images = model.MoreImages;
            if (images != null)
            {
                XElement xImages = XElement.Parse(images);
                foreach(XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
            }
            ViewBag.listImages = listImages;
            return View(model);
        }
    }
}