﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedSalesModel.DBModels;

public partial class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Salesperson> Salespeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8923B4112");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF64DDB9D3C");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Discount__Produc__4222D4EF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDD77E9633");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CommissionPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Manufacturer)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Style)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C41F51F22950");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Sales__CustomerI__3F466844");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales__ProductID__3D5E1FD2");

            entity.HasOne(d => d.Salesperson).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SalespersonId)
                .HasConstraintName("FK__Sales__Salespers__3E52440B");
        });

        modelBuilder.Entity<Salesperson>(entity =>
        {
            entity.HasKey(e => e.SalespersonId).HasName("PK__Salesper__C20105680D89859E");

            entity.ToTable("Salesperson");

            entity.Property(e => e.SalespersonId).HasColumnName("SalespersonID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Salespers__Manag__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}