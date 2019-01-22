using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fazzer.ViewModels
{
    public class ViewViewModel
    {
        public class CategoryViewViewModel
        {
            public string Name { get; set; }
        }

        public class ProductViewViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}