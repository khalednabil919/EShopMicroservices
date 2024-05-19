using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                OrderId => OrderId.Value,
                dbId => OrderId.of(dbId)
                );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();


            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .IsRequired();

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                                   .HasMaxLength(50)
                                   .IsRequired();

                    addressBuilder.Property(a => a.LastName)
                                   .HasMaxLength(50)
                                   .IsRequired();

                    addressBuilder.Property(a => a.EmailName)
                                   .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                                   .HasMaxLength(50)
                                   .IsRequired();

                    addressBuilder.Property(a => a.Country)
                                   .HasMaxLength(50)
                                   .IsRequired();

                    addressBuilder.Property(a => a.ZipCode)
                                   .HasMaxLength(50)
                                   .IsRequired();
                    addressBuilder.Property(a => a.State)
                                   .HasMaxLength(50)
                                   .IsRequired();

                });


            builder.ComplexProperty(
              o => o.BillingAddress, addressBuilder =>
              {
                  addressBuilder.Property(a => a.FirstName)
                                 .HasMaxLength(50)
                                 .IsRequired();

                  addressBuilder.Property(a => a.LastName)
                                 .HasMaxLength(50)
                                 .IsRequired();

                  addressBuilder.Property(a => a.EmailName)
                                 .HasMaxLength(50);

                  addressBuilder.Property(a => a.AddressLine)
                                 .HasMaxLength(50)
                                 .IsRequired();

                  addressBuilder.Property(a => a.Country)
                                 .HasMaxLength(50)
                                 .IsRequired();

                  addressBuilder.Property(a => a.ZipCode)
                                 .HasMaxLength(50)
                                 .IsRequired();
                  addressBuilder.Property(a => a.State)
                                 .HasMaxLength(50)
                                 .IsRequired();

              });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(50);

                paymentBuilder.Property(p => p.CVV)
                .HasMaxLength(50);

                paymentBuilder.Property(p => p.PaymentMethod);

            });


            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
                );

            builder.Property(x => x.TotalPrice);
        }
    }
}

