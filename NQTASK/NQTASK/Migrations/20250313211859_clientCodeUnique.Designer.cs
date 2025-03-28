﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NQTASK.Models;

#nullable disable

namespace NQTASK.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250313211859_clientCodeUnique")]
    partial class clientCodeUnique
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NQTASK.Models.Account", b =>
                {
                    b.Property<int>("Acc_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Acc_ID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Acc_ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NQTASK.Models.Client", b =>
                {
                    b.Property<int>("Cl_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cl_ID"));

                    b.Property<int>("Account_Id")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatetimeofRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cl_ID");

                    b.HasIndex("Account_Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NQTASK.Models.Invoice", b =>
                {
                    b.Property<int>("In_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("In_Id"));

                    b.Property<DateTime>("OutDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("cl_Id")
                        .HasColumnType("int");

                    b.HasKey("In_Id");

                    b.HasIndex("cl_Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("NQTASK.Models.Invoice_Product", b =>
                {
                    b.Property<int>("Pr_ID")
                        .HasColumnType("int");

                    b.Property<int>("In_ID")
                        .HasColumnType("int");

                    b.HasKey("Pr_ID", "In_ID");

                    b.HasIndex("In_ID");

                    b.ToTable("Invoice_Product");
                });

            modelBuilder.Entity("NQTASK.Models.Product", b =>
                {
                    b.Property<int>("Pr_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pr_ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Pr_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NQTASK.Models.Client", b =>
                {
                    b.HasOne("NQTASK.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NQTASK.Models.Invoice", b =>
                {
                    b.HasOne("NQTASK.Models.Client", "Client")
                        .WithMany("Invoices")
                        .HasForeignKey("cl_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("NQTASK.Models.Invoice_Product", b =>
                {
                    b.HasOne("NQTASK.Models.Invoice", "invoice")
                        .WithMany("InvoicesProducts")
                        .HasForeignKey("In_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NQTASK.Models.Product", "product")
                        .WithMany("InvoicesProducts")
                        .HasForeignKey("Pr_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("invoice");

                    b.Navigation("product");
                });

            modelBuilder.Entity("NQTASK.Models.Client", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("NQTASK.Models.Invoice", b =>
                {
                    b.Navigation("InvoicesProducts");
                });

            modelBuilder.Entity("NQTASK.Models.Product", b =>
                {
                    b.Navigation("InvoicesProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
