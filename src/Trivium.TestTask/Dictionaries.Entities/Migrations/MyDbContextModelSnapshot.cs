﻿// <auto-generated />
using Dictionaries.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dictionaries.Entities.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Dictionaries.Entities.Factory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("region");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("factory", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Завод 1",
                            Region = "Пермский край",
                            Year = 1968
                        },
                        new
                        {
                            Id = 2,
                            Name = "Завод 2",
                            Region = "Татарстан",
                            Year = 2001
                        },
                        new
                        {
                            Id = 3,
                            Name = "Завод 3",
                            Region = "Пермский край",
                            Year = 1998
                        });
                });

            modelBuilder.Entity("Dictionaries.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "G-001",
                            Name = "Гвозди"
                        },
                        new
                        {
                            Id = 2,
                            Code = "SH-20",
                            Name = "Шурупы"
                        },
                        new
                        {
                            Id = 3,
                            Code = "SA-19",
                            Name = "Саморезы"
                        },
                        new
                        {
                            Id = 4,
                            Code = "B-100",
                            Name = "Болты"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
