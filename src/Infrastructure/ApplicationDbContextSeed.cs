using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {
            try
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = "marian_test@yahoo.com",
                    Email = "marian_test@yahoo.com"
                };

                await userManager.CreateAsync(defaultUser, "Pass@word1");

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
                new ProductType("TV"),
                new ProductType("Camera"),
                new ProductType("Laptops"),
                new ProductType("Phones"),
                new ProductType("Audio")
            };
        }

        private static IEnumerable<Product> GetDefaultProducts()
        {
            return new List<Product>
            {
                new Product("Samsung MU6400 40-Inch SMART Ultra HD TV", 1),
                new Product("Panasonic TX-24E302B 720p HD Ready 24-Inch LED TV with Freeview HD", 1),
                new Product("Sony Bravia (49-Inch) Premium Full HD HDR TV (X-Reality PRO, Triluminos Display)", 1),
                new Product("Sony DSCW800B.CEH Digital Compact Camera (20.1 MP, 5x Zoom, 2.7 LCD, 720p HD, 23 mm Sony G Lens)", 2),
                new Product("Canon PowerShot SX620 HS Digital Camera", 2),
                new Product("Nikon D3400 + AF-P 18-55VR Digital SLR Camera & Lens Kit", 2),
                new Product("HP EliteBook 840 G1 14-inch Ultrabook (Intel Core i5 4th Gen, 8GB Memory, 256GB SSD, WiFi, WebCam, Windows 10 Professional 64-bit)", 3),
                new Product("Microsoft Surface Pro 12.3-Inch PixelSense Tablet PC (Silver) with Black Type Cover - (Intel 7th Gen Core m3-7Y30 2.6GHz, 4GB RAM, 128GB SSD, Intel HD Graphics 615, 2017 Model, Windows 10 Pro)", 3),
                new Product("Dell Inspiron 17 5000 17.3-Inch HD+ Laptop - (Intel Pentium 4415U Processor, 4 GB RAM + 1 TB HDD, Windows 10 Home) D2NK1", 3),
                new Product("Samsung 3768985 Galaxy J3 SIM-Free Smartphone - Black", 4),
                new Product("Apple iPhone X 64 GB SIM-Free Smartphone - Space Grey", 4),
                new Product("Nokia 7 Plus Sim-Free Smartphone - Black/Copper", 4),
                new Product("Panasonic SC-PM250BEBS DAB Micro Hi-Fi System", 5),
                new Product("Sony CMT-X3CD Micro Hi-Fi System with CD/Bluetooth/NFC", 5),
                new Product("Pioneer X-CM35-R 30 W CD Micro System with Bluetooth, NFC, FM Tuner and USB Input", 5)            };
        }
    }
}
