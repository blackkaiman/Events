﻿// <auto-generated />
using System;
using EvoEvents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvoEvents.Migrations.Migrations
{
    [DbContext(typeof(EvoEventsContext))]
    [Migration("20220817120429_UpdateEventsWithDateInfo")]
    partial class UpdateEventsWithDateInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EvoEvents.Data.Models.Addresses.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Addresses.CityLookup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CityLookups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sibiu"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cluj"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bistrita"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Frankfurt"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Milano"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Roma"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Hamburg"
                        });
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Addresses.CountryLookup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CountryLookups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Romania"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Germania"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Italia"
                        });
                });

            modelBuilder.Entity("EvoEvents.Data.Models.ApplicationVersions.ApplicationVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApplicationVersions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Create Skeleton",
                            Version = "1.0.1"
                        });
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Events.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<int>("MaxNoAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ToDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Events.EventTypeLookup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypeLookups");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "None"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Movie"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Concert"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Talk"
                        });
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Reservations.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccompanyingPersonEmail")
                        .HasMaxLength(74)
                        .HasColumnType("nvarchar(74)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(74)
                        .HasColumnType("nvarchar(74)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Users.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Addresses.Address", b =>
                {
                    b.HasOne("EvoEvents.Data.Models.Addresses.CityLookup", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvoEvents.Data.Models.Addresses.CountryLookup", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvoEvents.Data.Models.Events.Event", null)
                        .WithOne("Address")
                        .HasForeignKey("EvoEvents.Data.Models.Addresses.Address", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Events.Event", b =>
                {
                    b.HasOne("EvoEvents.Data.Models.Events.EventTypeLookup", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Reservations.Reservation", b =>
                {
                    b.HasOne("EvoEvents.Data.Models.Events.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvoEvents.Data.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Users.UserDetail", b =>
                {
                    b.HasOne("EvoEvents.Data.Models.Users.User", null)
                        .WithOne("Information")
                        .HasForeignKey("EvoEvents.Data.Models.Users.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Events.Event", b =>
                {
                    b.Navigation("Address");
                });

            modelBuilder.Entity("EvoEvents.Data.Models.Users.User", b =>
                {
                    b.Navigation("Information");
                });
#pragma warning restore 612, 618
        }
    }
}
