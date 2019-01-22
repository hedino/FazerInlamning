namespace Fazzer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fazzer.Models.FazerDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Fazzer.Models.FazerDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Categories.AddOrUpdate(c => c.CategoryId,
                new Models.Category { CategoryId = 1, Name = "Chocolate" },
                new Models.Category { CategoryId = 2, Name = "Bread" },
                new Models.Category { CategoryId = 3, Name = "Candy" },
                new Models.Category { CategoryId = 4, Name = "Meat" },
                new Models.Category { CategoryId = 5, Name = "Cheese" }

                );


            context.Products.AddOrUpdate(p => p.ProductId,
                new Models.Product { ProductId = 1, CategoryId = 1, Name = "Milk Chocolate", Description = "40% kakao", Price = 13 },
                new Models.Product { ProductId = 2, CategoryId = 1, Name = "Dark Chocolate", Description = "70% kakao", Price = 15 },
                new Models.Product { ProductId = 3, CategoryId = 2, Name = "Rye Bread", Description = "Full rye", Price = 23 },
                new Models.Product { ProductId = 4, CategoryId = 2, Name = "Baguette", Description = "Frech Baguette", Price = 23 },
                new Models.Product { ProductId = 5, CategoryId = 3, Name = "Licorice", Description = "Finish Licorice", Price = 10 },
                new Models.Product { ProductId = 6, CategoryId = 3, Name = "Rasberry hardcandy", Description = "Hardcandy with taste of rasberry", Price = 12 },
                new Models.Product { ProductId = 7, CategoryId = 4, Name = "Chicken", Description = "Chicken", Price = 13 },
                new Models.Product { ProductId = 8, CategoryId = 4, Name = "Beef", Description = "Beef", Price = 13 },
                new Models.Product { ProductId = 9, CategoryId = 5, Name = "Blue cheese", Description = "Blue cheese", Price = 13 },
                new Models.Product { ProductId = 10, CategoryId = 5, Name = "Brie", Description = "Brie", Price = 13 }
                );
        }
    }
}
