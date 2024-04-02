﻿// <auto-generated />
using System;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog2023.Migrations.ApplicationDbContextAddressMigrations
{
    [DbContext(typeof(ApplicationDbContextAddress))]
    [Migration("20231201190340_addAddress")]
    partial class addAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Blog2023.Data.Models.As_addr_obj", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer");

                    b.Property<int?>("Isactual")
                        .HasColumnType("integer");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Objectguid")
                        .HasColumnType("uuid");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint");

                    b.Property<int?>("Opertypeid")
                        .HasColumnType("integer");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date");

                    b.Property<string>("Typename")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("as_addr_objs");
                });

            modelBuilder.Entity("Blog2023.Data.Models.as_adm_hierarchy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Areacode")
                        .HasColumnType("text");

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint");

                    b.Property<string>("Citycode")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint");

                    b.Property<long?>("Objectid")
                        .HasColumnType("bigint");

                    b.Property<long?>("Parentobjid")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Placecode")
                        .HasColumnType("text");

                    b.Property<string>("Plancode")
                        .HasColumnType("text");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint");

                    b.Property<string>("Regioncode")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date");

                    b.Property<string>("Streetcode")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("as_adm_hierarchys");
                });

            modelBuilder.Entity("Blog2023.Data.Models.as_houses", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Addnum1")
                        .HasColumnType("text");

                    b.Property<string>("Addnum2")
                        .HasColumnType("text");

                    b.Property<int?>("Addtype1")
                        .HasColumnType("integer");

                    b.Property<int?>("Addtype2")
                        .HasColumnType("integer");

                    b.Property<long?>("Changeid")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date");

                    b.Property<string>("Housenum")
                        .HasColumnType("text");

                    b.Property<int?>("Housetype")
                        .HasColumnType("integer");

                    b.Property<int?>("Isactive")
                        .HasColumnType("integer");

                    b.Property<int?>("Isactual")
                        .HasColumnType("integer");

                    b.Property<long?>("Nextid")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Objectguid")
                        .HasColumnType("uuid");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint");

                    b.Property<int?>("Opertypeid")
                        .HasColumnType("integer");

                    b.Property<long?>("Previd")
                        .HasColumnType("bigint");

                    b.Property<DateOnly?>("Startdate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("Updatedate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("as_housess");
                });
#pragma warning restore 612, 618
        }
    }
}
