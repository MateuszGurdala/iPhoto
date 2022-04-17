﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iPhoto.DataBase;

#nullable disable

namespace iPhoto.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220417185457_ChangedPlacePhotoEntities")]
    partial class ChangedPlacePhotoEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("iPhoto.DataBase.AlbumEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLocal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhotoCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("Tags")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("#none");

                    b.HasKey("Id");

                    b.ToTable("AlbumEntities");
                });

            modelBuilder.Entity("iPhoto.DataBase.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResolutionHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResolutionWidth")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Size")
                        .HasColumnType("REAL");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ImageEntities");
                });

            modelBuilder.Entity("iPhoto.DataBase.PhotoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("DateTaken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaceEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Tags")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("#none");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlbumEntityId");

                    b.HasIndex("ImageEntityId")
                        .IsUnique();

                    b.HasIndex("PlaceEntityId");

                    b.ToTable("PhotoEntities");
                });

            modelBuilder.Entity("iPhoto.DataBase.PlaceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PlaceEntities");
                });

            modelBuilder.Entity("iPhoto.DataBase.PhotoEntity", b =>
                {
                    b.HasOne("iPhoto.DataBase.AlbumEntity", "AlbumEntity")
                        .WithMany("PhotoEntities")
                        .HasForeignKey("AlbumEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iPhoto.DataBase.ImageEntity", "ImageEntity")
                        .WithOne("PhotoEntity")
                        .HasForeignKey("iPhoto.DataBase.PhotoEntity", "ImageEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iPhoto.DataBase.PlaceEntity", "PlaceEntity")
                        .WithMany("PhotoEntities")
                        .HasForeignKey("PlaceEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlbumEntity");

                    b.Navigation("ImageEntity");

                    b.Navigation("PlaceEntity");
                });

            modelBuilder.Entity("iPhoto.DataBase.AlbumEntity", b =>
                {
                    b.Navigation("PhotoEntities");
                });

            modelBuilder.Entity("iPhoto.DataBase.ImageEntity", b =>
                {
                    b.Navigation("PhotoEntity")
                        .IsRequired();
                });

            modelBuilder.Entity("iPhoto.DataBase.PlaceEntity", b =>
                {
                    b.Navigation("PhotoEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
