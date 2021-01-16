using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanMayTinh.Common;
using WebBanMayTinh.DAO;
using WebBanMayTinh.EF;
using WebBanMayTinh.Models;

namespace WebBanMayTinh.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Home
        public ActionResult Index()
        {
            var productDao = new ProductDAO();
            ViewBag.HotProduct = productDao.ListHotProduct(20);
            return View();
        }

        //Thanh menu
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new ProductCategoryDAO().ListByGroupId();
            return PartialView(model);
        }

        /// <summary>
        /// Trả về LoginAndRegister.cshtml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoginAndRegister()
        {
            return View();
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var pass = Encryptor.MD5Hash(password);
            var model = new UserDAO().Login(username, pass);
            var user = new UserDAO().GetByUsername(username);
            if (model == 1)
            {
                Session["Usernamemember"] = user.Name;
                Session["UsernameMemberID"] = user.ID;
                return RedirectToAction("Index", "Home");
            }
            else if (model == 0)
            {
                TempData["Error"] = "Tài khoản không tồn tại";
                return View("LoginAndRegister");
            }
            else if (model == -1)
            {
                TempData["Error"] = "Tài khoản đang bị khóa";
                return View("LoginAndRegister");
            }
            else if (model == -2)
            {
                TempData["Error"] = "Đăng nhập không đúng(Sai tên đăng nhập/mật khẩu)";
                return View("LoginAndRegister");
            }
            else
            {
                TempData["Error"] = "Đăng nhập thất bại";
                return View("LoginAndRegister");
            }
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginAndRegister(UserModels model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (dao.CheckUserName(model.Username))
                {
                    TempData["Error"] = "Tên đăng nhập đã tồn tại";
                    return View("LoginAndRegister");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    TempData["Error"] = "Email đã tồn tại";
                    return View("LoginAndRegister");
                }
                else
                {
                    var user = new User();
                    user.Username = model.Username;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Phone = model.Phone;
                    user.UserAddress = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    user.GroupUserID = 2;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new UserModels();
                    }
                    else
                    {
                        TempData["Error"] = "Đăng ký thất bại";
                        return View("LoginAndRegister");
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["UsernameMember"] = null;
            TempData["Error"] = null;
            TempData["Success"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}