﻿// <auto-generated />
using System;
using AlmaIt.Dotnet.Heroes.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    [DbContext(typeof(HeroesContext))]
    partial class HeroesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicBookTags", b =>
                {
                    b.Property<int>("ComicBookId");

                    b.Property<int>("TagId");

                    b.Property<int>("Id");

                    b.HasKey("ComicBookId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ComicBookTags");
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

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicSeriesTags", b =>
                {
                    b.Property<int>("ComicSerieId");

                    b.Property<int>("TagId");

                    b.Property<int>("Id");

                    b.HasKey("ComicSerieId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ComicSerieTags");
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

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicBookTags", b =>
                {
                    b.HasOne("AlmaIt.dotnet.Heroes.Shared.Models.ComicBook", "ComicBook")
                        .WithMany("Tags")
                        .HasForeignKey("ComicBookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlmaIt.dotnet.Heroes.Shared.Models.ObjectTag", "Tag")
                        .WithMany("ComicBookTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlmaIt.dotnet.Heroes.Shared.Models.ComicSeriesTags", b =>
                {
                    b.HasOne("AlmaIt.dotnet.Heroes.Shared.Models.ComicSeries", "ComicSerie")
                        .WithMany("Tags")
                        .HasForeignKey("ComicSerieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlmaIt.dotnet.Heroes.Shared.Models.ObjectTag", "Tag")
                        .WithMany("ComicSerieTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
