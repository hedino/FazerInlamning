using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fazzer.ViewModels
{
    public class ProductIndexViewModel
    {
        public string SearchProduct { get; set; }

        public ProductIndexViewModel()
        {
            Products = new List<ProductListViewModel>();
        }
        public class ProductListViewModel
        {
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

        }
        public List<ProductListViewModel> Products { get; set; }
    }
}