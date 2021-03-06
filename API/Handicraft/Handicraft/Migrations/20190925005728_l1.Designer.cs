﻿// <auto-generated />
using System;
using Handicraft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Handicraft.Migrations
{
    [DbContext(typeof(HandicraftContext))]
    [Migration("20190925005728_l1")]
    partial class l1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Handicraft.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Create");

                    b.Property<string>("Introduction");

                    b.Property<string>("Name");

                    b.Property<string>("Price");

                    b.Property<Guid?>("ShoppingCarId");

                    b.Property<string>("Tips");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCarId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Handicraft.Models.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BannerImage");

                    b.Property<DateTime>("Create");

                    b.Property<string>("DetailImage");

                    b.Property<Guid>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Handicraft.Models.ShoppingCar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("Count");

                    b.Property<DateTime>("Create");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCar");
                });

            modelBuilder.Entity("Handicraft.Models.UserCollect", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Create");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserCollect");
                });

            modelBuilder.Entity("Handicraft.Models.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime>("Create");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("OpenId");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Handicraft.Models.Product", b =>
                {
                    b.HasOne("Handicraft.Models.ShoppingCar")
                        .WithMany("Product")
                        .HasForeignKey("ShoppingCarId");
                });

            modelBuilder.Entity("Handicraft.Models.ProductImage", b =>
                {
                    b.HasOne("Handicraft.Models.Product", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Handicraft.Models.ShoppingCar", b =>
                {
                    b.HasOne("Handicraft.Models.UserInfo", "UserInfo")
                        .WithOne("ShoppingCar")
                        .HasForeignKey("Handicraft.Models.ShoppingCar", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Handicraft.Models.UserCollect", b =>
                {
                    b.HasOne("Handicraft.Models.UserInfo", "UserInfo")
                        .WithMany("UserCollect")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
