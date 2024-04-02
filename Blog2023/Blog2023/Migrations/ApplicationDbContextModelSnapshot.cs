﻿// <auto-generated />
using System;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog2023.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Blog.Data.Models.AuthorModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<int>("Posts")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("Blog.Data.Models.CommunityModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubscribersCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Community");
                });

            modelBuilder.Entity("Blog.Data.Models.MyCommunityModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("MyCommunity");
                });

            modelBuilder.Entity("Blog.Data.Models.PostModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommunityModelId")
                        .HasColumnType("uuid");

                    b.Property<string>("CommunityName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasLike")
                        .HasColumnType("boolean");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CommunityModelId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blog.Data.Models.TagModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateTime")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PostModelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Blog.Data.Models.TagPost", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdPost")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdTeg")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.ToTable("TagPosts");
                });

            modelBuilder.Entity("Blog.Data.Models.Token", b =>
                {
                    b.Property<string>("InvalidToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("InvalidToken");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Blog.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Blog2023.Data.DTO.AdministratorDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CommunityModelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CommunityModelId");

                    b.ToTable("AdministratorDTO");
                });

            modelBuilder.Entity("Blog2023.Data.Models.CommentModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PostModelId")
                        .HasColumnType("uuid");

                    b.Property<int>("SubComments")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("CommentModels");
                });

            modelBuilder.Entity("Blog2023.Data.Models.CommunityUserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("CommunityUserModels");
                });

            modelBuilder.Entity("Blog2023.Data.Models.UserLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserLikes");
                });

            modelBuilder.Entity("Blog.Data.Models.PostModel", b =>
                {
                    b.HasOne("Blog.Data.Models.CommunityModel", null)
                        .WithMany("Posts")
                        .HasForeignKey("CommunityModelId");
                });

            modelBuilder.Entity("Blog.Data.Models.TagModel", b =>
                {
                    b.HasOne("Blog.Data.Models.PostModel", null)
                        .WithMany("Tags")
                        .HasForeignKey("PostModelId");
                });

            modelBuilder.Entity("Blog2023.Data.DTO.AdministratorDTO", b =>
                {
                    b.HasOne("Blog.Data.Models.CommunityModel", null)
                        .WithMany("Administrators")
                        .HasForeignKey("CommunityModelId");
                });

            modelBuilder.Entity("Blog2023.Data.Models.CommentModel", b =>
                {
                    b.HasOne("Blog.Data.Models.PostModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostModelId");
                });

            modelBuilder.Entity("Blog.Data.Models.CommunityModel", b =>
                {
                    b.Navigation("Administrators");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Blog.Data.Models.PostModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
