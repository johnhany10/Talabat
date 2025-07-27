using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _dbContext)
        {

            if (_dbContext.ProductBrand.Count() == 0)
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count() > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(Brand);
                    }
                    await _dbContext.SaveChangesAsync();
                } 
            }

            if (_dbContext.productCategories.Count() == 0)
            {
                var CategoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                if (Categories?.Count() > 0)
                {
                    foreach (var Category in Categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(Category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Products.Count() == 0)
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products?.Count() > 0)
                {
                    foreach (var Product in Products)
                    {
                        _dbContext.Set<Product>().Add(Product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

        }


    }
}
