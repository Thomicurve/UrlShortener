﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShortener;

#nullable disable

namespace UrlShortener.Migrations
{
    [DbContext(typeof(UrlShortenerContext))]
    [Migration("20231017233752_UrlCategoryAndCounterAdded")]
    partial class UrlCategoryAndCounterAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("UrlShortener.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("UrlShortener.Url", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LongUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VisitCounter")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Url", (string)null);
                });

            modelBuilder.Entity("UrlShortener.Url", b =>
                {
                    b.HasOne("UrlShortener.Category", "Category")
                        .WithMany("Urls")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("UrlShortener.Category", b =>
                {
                    b.Navigation("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}