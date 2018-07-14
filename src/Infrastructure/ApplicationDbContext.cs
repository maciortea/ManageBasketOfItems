using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Basket>(ConfigureBasket);
            builder.Entity<Pounds>(ConfigurePounds);
            builder.Entity<BasketItem>(ConfigureBasketItem);
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<ProductType>(ConfigureProductType);
        }

        private void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(p => p.Id);

            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigurePounds(EntityTypeBuilder<Pounds> builder)
        {
            builder.Property(p => p.Value).UsePropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureBasketItem(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.OwnsOne(i => i.UnitPriceInPounds);

            builder.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);
        }

        private void ConfigureProductType(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
