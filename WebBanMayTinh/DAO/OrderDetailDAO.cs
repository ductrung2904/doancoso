using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanMayTinh.EF;

namespace WebBanMayTinh.DAO
{
    public class OrderDetailDAO
    {
        QLBanMayTinhDbContext db = null;
        public OrderDetailDAO()
        {
            db = new QLBanMayTinhDbContext();
        }

        /// <summary>
        /// Chi tiết đơn hàng
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}