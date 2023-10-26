﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    [DbContext(typeof(BlancheDbContext))]
    partial class BlancheDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Common.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Domain.Formulas.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FormulaId")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FormulaId");

                    b.HasIndex("ImageId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("Domain.Formulas.Formula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Formulas");
                });

            modelBuilder.Entity("Domain.Formulas.Equipment", b =>
                {
                    b.HasOne("Domain.Formulas.Formula", null)
                        .WithMany("Equipment")
                        .HasForeignKey("FormulaId");

                    b.HasOne("Domain.Common.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                            b1.ToTable("Equipments");

                            b1.WithOwner()
                                .HasForeignKey("EquipmentId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Image");
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

                            b1.ToTable("Formulas");

                            b1.WithOwner()
                                .HasForeignKey("FormulaId");
                        });

                    b.Navigation("Description")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Formulas.Formula", b =>
                {
                    b.Navigation("Equipment");
                });
#pragma warning restore 612, 618
        }
    }
}
