﻿// <auto-generated />
using System;
using EFCoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMvc.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200928014945_AddingDepartment")]
    partial class AddingDepartment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreMvc.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnName("Department")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Office Email")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnName("Sex")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("FullName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal?>("Salary")
                        .IsRequired()
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            Department = "Accountant",
                            Email = "wonkulahp@yahoo.com",
                            Gender = "Female",
                            Name = "Precious K Wonkulah",
                            Salary = 7000m
                        },
                        new
                        {
                            EmployeeId = 2,
                            Department = "HR",
                            Email = "dacious@outlook.com",
                            Gender = "Female",
                            Name = "Dacious R Wonkulah",
                            Salary = 5000m
                        },
                        new
                        {
                            EmployeeId = 3,
                            Department = "IT",
                            Email = "john@hotmail.com",
                            Gender = "Male",
                            Name = "John Brown",
                            Salary = 5000m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
