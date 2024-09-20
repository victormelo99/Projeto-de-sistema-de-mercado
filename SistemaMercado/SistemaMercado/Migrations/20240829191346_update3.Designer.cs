﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SistemaMercado.Data;

#nullable disable

namespace SistemaMercado.Migrations
{
    [DbContext(typeof(SistemaMercadoDbContext))]
    [Migration("20240829191346_update3")]
    partial class update3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SistemaMercado.Models.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Paymentdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Situation")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("SistemaMercado.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Celphone")
                        .HasColumnType("text");

                    b.Property<decimal>("DailyPayment")
                        .HasColumnType("numeric");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<decimal>("HourlyPay")
                        .HasColumnType("decimal(6,2)");

                    b.Property<TimeSpan>("HoursWorked")
                        .HasColumnType("interval");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("MonthlyPayment")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<char>("Sex")
                        .HasColumnType("character(1)");

                    b.Property<long>("WorkedDays")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SistemaMercado.Models.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("SistemaMercado.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal?>("CostValue")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("SaleValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SistemaMercado.Models.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InventoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(6,2)");

                    b.Property<decimal>("UnitPriceAtSale")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("SistemaMercado.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Adress")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("SistemaMercado.Models.Inventory", b =>
                {
                    b.HasOne("SistemaMercado.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SistemaMercado.Models.Product", b =>
                {
                    b.HasOne("SistemaMercado.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("SistemaMercado.Models.Sale", b =>
                {
                    b.HasOne("SistemaMercado.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMercado.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
