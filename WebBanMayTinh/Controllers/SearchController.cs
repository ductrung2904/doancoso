using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanMayTinh.DAO;

namespace WebBanMayTinh.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [ChildActionOnly]
        public ActionResult SearchForm()
        {
            return PartialView("_SearchFormPartial");
        }

        //Tìm kiếm sản phẩm theo tên
        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            var db = new ProductDAO().GetProductNew(100);
            var model = db.Where(x => x.Name.Contains(term));
            var productlist = (from p in model orderby p.ID descending select new
            {
                    p.ID, p.Name, p.Price, p.Images, p.Quantity, p.MetaTitle
            }).Take(5);
            return Json(productlist, JsonRequestBehavior.AllowGet);
        }
    }
}