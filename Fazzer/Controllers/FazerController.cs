using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fazzer.Controllers
{
    public class FazerController : Controller
    {

        public void SortProducts(string sort, ViewModels.ProductIndexViewModel model)
        {
            model.SortName = String.IsNullOrEmpty(sort) ? "namedes" : "";
            model.SortPrice = sort == "price" ? "pricedes" : "price";
            switch (sort)
            {
                case "namedes":
                    model.Products = model.Products.OrderByDescending(s => s.Name).ToList();
                    break;
                case "price":
                    model.Products = model.Products.OrderBy(s => s.Price).ToList();
                    break;
                case "pricedes":
                    model.Products = model.Products.OrderByDescending(s => s.Price).ToList();
                    break;
                default:
                    model.Products = model.Products.OrderBy(s => s.Name).ToList();
                    break;
            }
        }
        // GET: Category
        public ActionResult CategoryIndex()
        {
            var model = new ViewModels.CategoryIndexViewModel();
            using (var db = new Models.FazerDB())
            {
                model.Categories.AddRange(db.Categories.Select(c => new ViewModels.CategoryIndexViewModel.CategoryListViewModel
                {
                    Name = c.Name,
                    CategoryId = c.CategoryId
                }));

            }

            return View(model);
        }

        public ActionResult ProductIndex(string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            using (var db = new Models.FazerDB())
            {
                model.Products.AddRange(db.Products.Select(p => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    ProductId = p.ProductId,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Price = p.Price
                    
                }));
            }
            SortProducts(sort, model);
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchProduct(string searchProduct, string sort)
        {
            using (var db = new Models.FazerDB())
            {

                var model = new ViewModels.ProductIndexViewModel
                {
                   SearchProduct = searchProduct
                };
                model.Products.AddRange(db.Products.Select(p => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    ProductId = p.ProductId
                }).ToList().Where(c => c.Name.ToLower().Contains(model.SearchProduct.ToLower()) || c.Description.ToLower().Contains(model.SearchProduct.ToLower())
                    ));

                SortProducts(sort, model);
                return View("SearchProduct", model);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult CategoryCreate()
        {
            var model = new ViewModels.CategoryCreateViewModel();
            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult CategoryCreate(ViewModels.CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.FazerDB())
            {

                var category = new Models.Category
                {

                    Name = model.Name

                };

                db.Categories.Add(category);
                db.SaveChanges();

            }

            return RedirectToAction("CategoryIndex");
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult CategoryEdit(int id)
        {
            using (var db = new Models.FazerDB())
            {

                var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                var model = new ViewModels.CategoryEditViewModel
                {
                    Name = category.Name,
                    Id = category.CategoryId
                };
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult CategoryEdit(ViewModels.CategoryEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new Models.FazerDB())
            {

                var category = db.Categories.FirstOrDefault(c => c.CategoryId == model.Id);
                category.Name = model.Name;

                db.SaveChanges();
                return RedirectToAction("CategoryIndex");

            }

        }

        //SetUpAvailableCategories För EDIT
        void SetupAvailableCatagories(ViewModels.ProductCreateViewModel model)
        {
            model.AvailableCategory = new List<SelectListItem>
            {
                 new SelectListItem {Value = null , Text ="..Choose a catagory.."},


            };
            using (var db = new Models.FazerDB())
            {
                foreach (var cat in db.Categories)
                {
                    model.AvailableCategory.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.Name });
                }
            }


        }



        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult ProductCreate()
        {
            var model = new ViewModels.ProductCreateViewModel();
            SetupAvailableCatagories(model);

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult ProductCreate(ViewModels.ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.FazerDB())
            {
                var pro = new Models.Product

                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId
                };
                db.Products.Add(pro);
                db.SaveChanges();
            }
            return RedirectToAction("ProductIndex", new { id = model.CategoryId });
        }

        //SetUpAvailableCategories För EDIT
        void SetupAvailableCatagories(ViewModels.ProductEditViewModel model)
        {
            model.AvailableCategory = new List<SelectListItem>
            {
                 new SelectListItem {Value = null , Text ="..Choose a catagory.."},


            };
            using (var db = new Models.FazerDB())
            {
                foreach (var cat in db.Categories)
                {
                    model.AvailableCategory.Add(new SelectListItem { Value = cat.CategoryId.ToString(), Text = cat.Name });
                }
            }


        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult ProductEdit(int id)
        {
            using (var db = new Models.FazerDB())
            {
                var products = db.Products.FirstOrDefault(p => p.ProductId == id);
                var model = new ViewModels.ProductEditViewModel
                {
                    Name = products.Name,
                    Description = products.Description,
                    Price = products.Price,
                    CategoryId = products.CategoryId,
                    ProductId = products.ProductId,


                };
                SetupAvailableCatagories(model);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult ProductEdit(ViewModels.ProductEditViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);
            }
            using (var db = new Models.FazerDB())
            {
                var prod = db.Products.FirstOrDefault(r => r.ProductId == model.ProductId);
                prod.Name = model.Name;
                prod.Description = model.Description;
                prod.Price = model.Price;
                prod.CategoryId = model.CategoryId;
                db.SaveChanges();
            }

            return RedirectToAction("ProductIndex", new { id = model.CategoryId });
        }

        public ActionResult CategoryView(int id, string sort)
        {
            var model = new ViewModels.ProductIndexViewModel();
            using (var db = new Models.FazerDB())
            {
                model.Products.AddRange(db.Products.Select(p => new ViewModels.ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    ProductId = p.ProductId,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Price = p.Price

                }).Where(p => p.CategoryId == id));

                model.CategoryId = id;
                SortProducts(sort, model);
                return View(model);
            }
        }

        public ActionResult ProductView(int id, string sort)
        {
            using (var db = new Models.FazerDB())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                var model = new ViewModels.ViewViewModel.ProductViewViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };

                return View(model);
            }
        }
    }
}