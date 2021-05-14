using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            try
            {
                if (!context.ProductBrand.Any())
                {
                    var BrandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrand.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductType.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypeData = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    foreach (var item in productTypeData)
                    {
                        context.ProductType.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}