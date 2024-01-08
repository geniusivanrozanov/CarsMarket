﻿// <auto-generated />
using System;
using FavoriteFilters.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FavoriteFilters.Infrastructure.Migrations
{
    [DbContext(typeof(FiltersContext))]
    partial class FiltersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FavoriteFilters.Domain.Entities.FilterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Currency")
                        .HasColumnType("integer");

                    b.Property<Guid?>("GenerationId")
                        .HasColumnType("uuid");

                    b.Property<int?>("MaxMileage")
                        .HasColumnType("integer");

                    b.Property<double?>("MaxPrice")
                        .HasColumnType("double precision");

                    b.Property<int?>("MaxYear")
                        .HasColumnType("integer");

                    b.Property<int?>("MinMileage")
                        .HasColumnType("integer");

                    b.Property<double?>("MinPrice")
                        .HasColumnType("double precision");

                    b.Property<int?>("MinYear")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ModelId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Filters");
                });
#pragma warning restore 612, 618
        }
    }
}
