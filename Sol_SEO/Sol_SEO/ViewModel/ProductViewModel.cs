using Sol_SEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_SEO.ViewModel
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }

       public IEnumerable<ProductModel> ListProducts { get; set; }
    }
}
