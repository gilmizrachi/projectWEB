﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projectWEB.Data;

namespace projectWEB.Migrations
{
    [DbContext(typeof(projectWEBContext))]
    [Migration("20201116073144_Categories")]
    partial class Categories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("projectWEB.Data.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlsoTryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemDevision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ItemImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AlsoTryId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("projectWEB.Models.AlsoTry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PriceLimits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("S_Phrase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("V_ItemNo")
                        .HasColumnType("int");

                    b.Property<int?>("registeredUsersid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("registeredUsersid");

                    b.ToTable("AlsoTry");
                });

            modelBuilder.Entity("projectWEB.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("projectWEB.Models.RegisteredUsers", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreditCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("MemberType")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("id");

                    b.ToTable("RegisteredUsers");
                });

            modelBuilder.Entity("projectWEB.Models.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int?>("registeredUsersid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("registeredUsersid");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("projectWEB.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("SumPrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("TranscationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("projectWEB.Data.Item", b =>
                {
                    b.HasOne("projectWEB.Models.AlsoTry", null)
                        .WithMany("V_Items")
                        .HasForeignKey("AlsoTryId");

                    b.HasOne("projectWEB.Models.Transaction", null)
                        .WithMany("Cart")
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("projectWEB.Models.AlsoTry", b =>
                {
                    b.HasOne("projectWEB.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");

                    b.HasOne("projectWEB.Models.RegisteredUsers", "registeredUsers")
                        .WithMany()
                        .HasForeignKey("registeredUsersid");
                });

            modelBuilder.Entity("projectWEB.Models.Reviews", b =>
                {
                    b.HasOne("projectWEB.Data.Item", "Item")
                        .WithMany("Comments")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projectWEB.Models.RegisteredUsers", "registeredUsers")
                        .WithMany("Reviews")
                        .HasForeignKey("registeredUsersid");
                });

            modelBuilder.Entity("projectWEB.Models.Transaction", b =>
                {
                    b.HasOne("projectWEB.Models.RegisteredUsers", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
