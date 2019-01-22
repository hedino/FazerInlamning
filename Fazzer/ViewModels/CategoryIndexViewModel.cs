using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fazzer.ViewModels
{
    public class CategoryIndexViewModel
    {
        public string SearchCategory { get; set; }

        public CategoryIndexViewModel()
        {
            Categories = new List<CategoryListViewModel>();
        }
        public class CategoryListViewModel
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }

        }
        public List<CategoryListViewModel> Categories { get; set; }
    }
}