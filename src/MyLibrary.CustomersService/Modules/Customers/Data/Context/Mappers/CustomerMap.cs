using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLibrary.CustomersService.Modules.Customers.Entities;
using MyLibrary.CustomersService.Modules.Customers.ValueObjects;

namespace MyLibrary.CustomersService.Modules.Customers.Data.Context.Mappers
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.Phone)
                .HasConversion(p => p.ToString(), p => new Phone(p));

            builder.OwnsOne(x => x.Address, addressBuilder =>
            {
                addressBuilder.Property(x => x.State)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(x => x.City)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(x => x.Street)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(x => x.Number)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(x => x.ZipCode)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
