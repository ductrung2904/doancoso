using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanMayTinh.Common;
using WebBanMayTinh.DAO;
using WebBanMayTinh.EF;
using WebBanMayTinh.Models;

namespace WebBanMayTinh.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        /// <summary>
        /// Xóa tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        /// <summary>
        /// Xóa 1 sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        /// <summary>
        /// Cập nhật giỏ hàng
        /// </summary>
        /// <param name="cartModel"></param>
        /// <returns></returns>
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDAO().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cartitem
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string shipPhone, string shipAddress, string shipEmail)
        {
            var order = new Order();
            order.ShipCreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipAddress = shipAddress;
            order.ShipPhone = shipPhone;
            order.ShipEmail = shipEmail;
            order.Status = true;

            var id = new OrderDAO().Insert(order);
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);
                    Session[CartSession] = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return Redirect("/hoan-thanh");
        }

        /// <summary>
        /// Thanh toán thành công
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            return View();
        }
    }
}