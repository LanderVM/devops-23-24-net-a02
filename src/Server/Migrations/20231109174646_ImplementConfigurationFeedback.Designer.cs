﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    [DbContext(typeof(BlancheDbContext))]
    [Migration("20231109174646_ImplementConfigurationFeedback")]
    partial class ImplementConfigurationFeedback
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Customers.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BillingAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("EmailId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("EmailId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Domain.Formulas.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<decimal>("Price")
                        .HasPrecision(2)
                        .HasColumnType("decimal(2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.ToTable("Equipment", (string)null);
                });

            modelBuilder.Entity("Domain.Formulas.Formula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(2)
                        .HasColumnType("decimal(2)");

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.ToTable("Formula", (string)null);
                });

            modelBuilder.Entity("Domain.Quotations.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventLocationId")
                        .HasColumnType("int");

                    b.Property<int>("FormulaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<int>("OrderedById")
                        .HasColumnType("int");

                    b.Property<decimal>("OriginalFormulaPricePerDay")
                        .HasPrecision(2)
                        .HasColumnType("decimal(2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("EventLocationId");

                    b.HasIndex("FormulaId");

                    b.HasIndex("OrderedById");

                    b.ToTable("Quotation", (string)null);
                });

            modelBuilder.Entity("Domain.Quotations.QuotationLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AmountOrdered")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EquipmentOrderedId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("OriginalEquipmentPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("QuotationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentOrderedId");

                    b.HasIndex("QuotationId");

                    b.ToTable("QuotationLine");
                });

            modelBuilder.Entity("EquipmentFormula", b =>
                {
                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("FormulasId")
                        .HasColumnType("int");

                    b.HasKey("EquipmentId", "FormulasId");

                    b.HasIndex("FormulasId");

                    b.ToTable("FormulaEquipment", (string)null);
                });

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.HasOne("Domain.Customers.Address", "BillingAddress")
                        .WithMany("BillingAddresses")
                        .HasForeignKey("BillingAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Customers.Email", "Email")
                        .WithMany()
                        .HasForeignKey("EmailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Customers.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("BillingAddress");

                    b.Navigation("Email");

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Formulas.Equipment", b =>
                {
                    b.OwnsOne("Domain.Formulas.Description", "Description", b1 =>
                        {
                            b1.Property<int>("EquipmentId")
                                .HasColumnType("int");

                            b1.Property<string>("Subtext")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("EquipmentId");

                            b1.ToTable("Equipment");

                            b1.WithOwner()
                                .HasForeignKey("EquipmentId");
                        });

                    b.Navigation("Description")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Formulas.Formula", b =>
                {
                    b.OwnsOne("Domain.Formulas.Description", "Description", b1 =>
                        {
                            b1.Property<int>("FormulaId")
                                .HasColumnType("int");

                            b1.Property<string>("Subtext")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("FormulaId");

                            b1.ToTable("Formula");

                            b1.WithOwner()
                                .HasForeignKey("FormulaId");
                        });

                    b.Navigation("Description")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Quotations.Quotation", b =>
                {
                    b.HasOne("Domain.Customers.Address", "EventLocation")
                        .WithMany("EventLocations")
                        .HasForeignKey("EventLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Formulas.Formula", "Formula")
                        .WithMany("OrderedIn")
                        .HasForeignKey("FormulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Customers.Customer", "OrderedBy")
                        .WithMany("Quotations")
                        .HasForeignKey("OrderedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventLocation");

                    b.Navigation("Formula");

                    b.Navigation("OrderedBy");
                });

            modelBuilder.Entity("Domain.Quotations.QuotationLine", b =>
                {
                    b.HasOne("Domain.Formulas.Equipment", "EquipmentOrdered")
                        .WithMany()
                        .HasForeignKey("EquipmentOrderedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Quotations.Quotation", "Quotation")
                        .WithMany("QuotationLines")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentOrdered");

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("EquipmentFormula", b =>
                {
                    b.HasOne("Domain.Formulas.Equipment", null)
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Formulas.Formula", null)
                        .WithMany()
                        .HasForeignKey("FormulasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Customers.Address", b =>
                {
                    b.Navigation("BillingAddresses");

                    b.Navigation("EventLocations");
                });

            modelBuilder.Entity("Domain.Customers.Customer", b =>
                {
                    b.Navigation("Quotations");
                });

            modelBuilder.Entity("Domain.Formulas.Formula", b =>
                {
                    b.Navigation("OrderedIn");
                });

            modelBuilder.Entity("Domain.Quotations.Quotation", b =>
                {
                    b.Navigation("QuotationLines");
                });
#pragma warning restore 612, 618
        }
    }
}
