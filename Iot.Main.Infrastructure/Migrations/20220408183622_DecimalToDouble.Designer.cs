﻿// <auto-generated />
using System;
using Iot.Main.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Iot.Main.Infrastructure.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20220408183622_DecimalToDouble")]
    partial class DecimalToDouble
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlertConstraint", b =>
                {
                    b.Property<int>("AlertsId")
                        .HasColumnType("int");

                    b.Property<int>("ConstraintsId")
                        .HasColumnType("int");

                    b.HasKey("AlertsId", "ConstraintsId");

                    b.HasIndex("ConstraintsId");

                    b.ToTable("AlertConstraint");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.AlertModel.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.CompanyModel.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.ConstraintModel.Constraint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<double?>("MaxHumidity")
                        .HasColumnType("float");

                    b.Property<double?>("MaxPressure")
                        .HasColumnType("float");

                    b.Property<double?>("MaxTemp")
                        .HasColumnType("float");

                    b.Property<double?>("MinHumidity")
                        .HasColumnType("float");

                    b.Property<double?>("MinPressure")
                        .HasColumnType("float");

                    b.Property<double?>("MinTemp")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Constraints");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.DeviceModel.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("ConstraintId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ConstraintId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.RoleModel.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.UserModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AlertConstraint", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.AlertModel.Alert", null)
                        .WithMany()
                        .HasForeignKey("AlertsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iot.Main.Domain.Models.ConstraintModel.Constraint", null)
                        .WithMany()
                        .HasForeignKey("ConstraintsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.AlertModel.Alert", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.CompanyModel.Company", "Company")
                        .WithMany("Alerts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.ConstraintModel.Constraint", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.CompanyModel.Company", "Company")
                        .WithMany("Constraints")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.DeviceModel.Device", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.CompanyModel.Company", "Company")
                        .WithMany("Devices")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iot.Main.Domain.Models.ConstraintModel.Constraint", "Constraint")
                        .WithMany("Devices")
                        .HasForeignKey("ConstraintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Constraint");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.RoleModel.Role", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.CompanyModel.Company", "Company")
                        .WithMany("Roles")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.UserModel.User", b =>
                {
                    b.HasOne("Iot.Main.Domain.Models.CompanyModel.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Iot.Main.Domain.Models.RoleModel.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.CompanyModel.Company", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Constraints");

                    b.Navigation("Devices");

                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Iot.Main.Domain.Models.ConstraintModel.Constraint", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
