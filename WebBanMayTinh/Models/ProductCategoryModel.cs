using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanMayTinh.Models
{
    public class ProductCategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Icon { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}