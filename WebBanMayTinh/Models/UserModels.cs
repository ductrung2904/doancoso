using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanMayTinh.Models
{
    public class UserModels
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Bạn chưa nhập Tên đăng nhập")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
        public string Password { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Bạn chưa nhập Họ tên")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public int GroupUserID { get; set; }
    }
}