﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReactApp.Server.Data;

#nullable disable

namespace ReactApp.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250115201008_UsingDateOnly")]
    partial class UsingDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationUserProjectRole", b =>
                {
                    b.Property<string>("ProjectRolesId")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("ProjectRolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ApplicationUserProjectRole");
                });

            modelBuilder.Entity("ReactApp.Server.Data.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ReactApp.Server.Data.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateOnly>("ActualEnd")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ActualStart")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<DateOnly>("ProjectedEnd")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ProjectedStart")
                        .HasColumnType("date");

                    b.Property<float>("TotalWorkHours")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ReactApp.Server.ProjectRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("ApplicationUserProjectRole", b =>
                {
                    b.HasOne("ReactApp.Server.ProjectRole", null)
                        .WithMany()
                        .HasForeignKey("ProjectRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReactApp.Server.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReactApp.Server.Data.Project", b =>
                {
                    b.HasOne("ReactApp.Server.Data.AppUser", "Owner")
                        .WithMany("OwnedProjects")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ReactApp.Server.Data.AppUser", b =>
                {
                    b.Navigation("OwnedProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
