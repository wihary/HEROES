﻿// <auto-generated />
using System;
using AlmaIt.dotnet.Heroes.Server.Data;
using AlmaIt.Dotnet.Heroes.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlmaIt.dotnet.Heroes.Server.Migrations
{
    [DbContext(typeof(HeroesContext))]
    [Migration("20190115122643_AddObjectTags")]
    partial class AddObjectTags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ComicSerieId");

                    b.Property<int>("IssueNumber");

                    b.Property<DateTime>("ParutionDate");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ComicSerieId");

                    b.ToTable("ComicBooks");
                });

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSerieCompleted");

                    b.Property<DateTime>("LastReleaseDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("NextReleaseDate");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("ComicSeries");
                });

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ObjectTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Argb");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ObjectTag");
                });

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicBook", b =>
                {
                    b.HasOne("AlmaIt.dotnet.Heroes.Shared.Models.ComicSeries", "ComicSerie")
                        .WithMany("AssociatedComnicBooksExtended")
                        .HasForeignKey("ComicSerieId");
                });
#pragma warning restore 612, 618
        }
    }
}
