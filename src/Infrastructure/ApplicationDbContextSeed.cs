using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!dbContext.Baskets.Any())
                {
                    dbContext.Baskets.Add(new Basket());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ProductTypes.Any())
                {
                    dbContext.ProductTypes.AddRange(GetDefaultProductTypes());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Products.Any())
                {
                    dbContext.Products.AddRange(GetDefaultProducts());
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                log.LogError(ex.Message);
            }
            
        }

        private static IEnumerable<ProductType> GetDefaultProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType { Type = "TV" },
                new ProductType { Type = "Camera" },
                new ProductType { Type = "Laptops" },
                new ProductType { Type = "Phones" },
                new ProductType { Type = "Audio" }
            };
        }

        private static IEnumerable<Product> GetDefaultProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Samsung MU6400 40-Inch SMART Ultra HD TV", ProductTypeId = 1 },
                new Product { Name = "Panasonic TX-24E302B 720p HD Ready 24-Inch LED TV with Freeview HD", ProductTypeId = 1 },
                new Product { Name = "Sony Bravia (49-Inch) Premium Full HD HDR TV (X-Reality PRO, Triluminos Display)", ProductTypeId = 1 },
                new Product { Name = "Sony DSCW800B.CEH Digital Compact Camera (20.1 MP, 5x Zoom, 2.7 LCD, 720p HD, 23 mm Sony G Lens)", ProductTypeId = 2 },
                new Product { Name = "Canon PowerShot SX620 HS Digital Camera", ProductTypeId = 2 },
                new Product { Name = "Nikon D3400 + AF-P 18-55VR Digital SLR Camera & Lens Kit", ProductTypeId = 2 },
                new Product { Name = "HP EliteBook 840 G1 14-inch Ultrabook (Intel Core i5 4th Gen, 8GB Memory, 256GB SSD, WiFi, WebCam, Windows 10 Professional 64-bit)", ProductTypeId = 3 },
                new Product { Name = "Microsoft Surface Pro 12.3-Inch PixelSense Tablet PC (Silver) with Black Type Cover - (Intel 7th Gen Core m3-7Y30 2.6GHz, 4GB RAM, 128GB SSD, Intel HD Graphics 615, 2017 Model, Windows 10 Pro)", ProductTypeId = 3 },
                new Product { Name = "Dell Inspiron 17 5000 17.3-Inch HD+ Laptop - (Intel Pentium 4415U Processor, 4 GB RAM + 1 TB HDD, Windows 10 Home) D2NK1", ProductTypeId = 3 },
                new Product { Name = "Samsung 3768985 Galaxy J3 SIM-Free Smartphone - Black", ProductTypeId = 4 },
                new Product { Name = "Apple iPhone X 64 GB SIM-Free Smartphone - Space Grey", ProductTypeId = 4 },
                new Product { Name = "Nokia 7 Plus Sim-Free Smartphone - Black/Copper", ProductTypeId = 4 },
                new Product { Name = "Panasonic SC-PM250BEBS DAB Micro Hi-Fi System", ProductTypeId = 5 },
                new Product { Name = "Sony CMT-X3CD Micro Hi-Fi System with CD/Bluetooth/NFC", ProductTypeId = 5 },
                new Product { Name = "Pioneer X-CM35-R 30 W CD Micro System with Bluetooth, NFC, FM Tuner and USB Input", ProductTypeId = 5 }
            };
        }
    }
}
