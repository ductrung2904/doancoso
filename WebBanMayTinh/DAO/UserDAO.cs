using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanMayTinh.EF;

namespace WebBanMayTinh.DAO
{
    public class UserDAO
    {
        QLBanMayTinhDbContext db = null;
        public UserDAO()
        {
            db = new QLBanMayTinhDbContext();
        }

        /// <summary>
        /// Thêm User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Lấy theo username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.Username == username);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string username, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                //Tài khoản này không tồn tại
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    //Tài khoản đang bị khóa
                    return -1;
                }
                else
                {
                    if (result.GroupUserID == 2)
                    {
                        if (result.Password == password)
                        {
                            //Đăng nhập thành công
                            return 1;
                        }
                        else
                            //Sai tài khoản, mật khẩu
                            return -2;
                    }
                    else
                    {
                        return -3;
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUserName(string username)
        {
            return db.Users.Count(x => x.Username == username) > 0;
        }

        /// <summary>
        /// Kiểm tra Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}