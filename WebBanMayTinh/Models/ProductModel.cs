using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanMayTinh.EF;

namespace WebBanMayTinh.Models
{
    public class ProductModel
    {
        public ProductModel() { }
        public int ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Images { get; set; }
        public string MoreImages { get; set; }
        public string Descriptions { get; set; }
        public string Detail { get; set; }
        public bool? Status { get; set; }
        public DateTime? TopHot { get; set; }
        public DateTime? CreatedDated { get; set; }
        public int CategoryID { get; set; }
        public int ProducerID { get; set; }
        public string Producer { get; set; }
        public string ProductCategory { get; set; }
    }
}