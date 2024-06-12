﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(HazarDbContext))]
    [Migration("20240612131338_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid?>("Anahtar")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Durum")
                        .HasColumnType("smallint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("GuncellenmeTarih")
                        .HasColumnType("datetime2");

                    b.Property<long?>("GuncelleyenKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OlusturanKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("OlusturulmaTarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("SilenKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("SilinmeTarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid?>("Anahtar")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Durum")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("GuncellenmeTarih")
                        .HasColumnType("datetime2");

                    b.Property<long?>("GuncelleyenKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OlusturanKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("OlusturulmaTarih")
                        .HasColumnType("datetime2");

                    b.Property<long?>("SilenKullaniciId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("SilinmeTarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });
#pragma warning restore 612, 618
        }
    }
}
