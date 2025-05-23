﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreManagement_Project.Data;

#nullable disable

namespace StoreManagement_Project.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250420113151_M1")]
    partial class M1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StoreManagement_Project.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ItemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackSizeId")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("UOMId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PackSizeId");

                    b.HasIndex("SubCategoryId");

                    b.HasIndex("UOMId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PackSize", b =>
                {
                    b.Property<int>("PackSizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackSizeId"));

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PackSizeId");

                    b.ToTable("PackSizes");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseOrderId"));

                    b.Property<string>("DeliveryPoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PONo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseOrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PurchaseOrderItem", b =>
                {
                    b.Property<int>("PurchaseOrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseOrderItemId"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PurchaseOrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UOMId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseOrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseOrderId");

                    b.HasIndex("UOMId");

                    b.ToTable("PurchaseOrderItems");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.UOM", b =>
                {
                    b.Property<int>("UOMId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UOMId"));

                    b.Property<string>("UOMName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UOMId");

                    b.ToTable("UOMs");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Item", b =>
                {
                    b.HasOne("StoreManagement_Project.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement_Project.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement_Project.Models.PackSize", "PackSize")
                        .WithMany()
                        .HasForeignKey("PackSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement_Project.Models.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement_Project.Models.UOM", "UOM")
                        .WithMany()
                        .HasForeignKey("UOMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("PackSize");

                    b.Navigation("SubCategory");

                    b.Navigation("UOM");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PurchaseOrder", b =>
                {
                    b.HasOne("StoreManagement_Project.Models.Supplier", "Supplier")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PurchaseOrderItem", b =>
                {
                    b.HasOne("StoreManagement_Project.Models.Item", "Item")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement_Project.Models.PurchaseOrder", null)
                        .WithMany("PurchaseOrderItem")
                        .HasForeignKey("PurchaseOrderId");

                    b.HasOne("StoreManagement_Project.Models.UOM", "UOM")
                        .WithMany()
                        .HasForeignKey("UOMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("UOM");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.SubCategory", b =>
                {
                    b.HasOne("StoreManagement_Project.Models.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Item", b =>
                {
                    b.Navigation("PurchaseOrderItems");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderItem");
                });

            modelBuilder.Entity("StoreManagement_Project.Models.Supplier", b =>
                {
                    b.Navigation("PurchaseOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
