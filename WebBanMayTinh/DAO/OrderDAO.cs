using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanMayTinh.EF;

namespace WebBanMayTinh.DAO
{
    public class OrderDAO
    {
        QLBanMayTinhDbContext db = null;
        public OrderDAO()
        {
            db = new QLBanMayTinhDbContext();
        }

        /// <summary>
        /// Thêm đơn hàng
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}