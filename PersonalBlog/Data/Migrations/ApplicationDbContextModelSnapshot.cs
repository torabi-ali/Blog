﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalBlog.Data;

namespace PersonalBlog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PersonalBlog.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("PersonalBlog.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128);

                    b.Property<bool>("Gender");

                    b.Property<bool>("IsSuspended");

                    b.Property<string>("LastName")
                        .HasMaxLength(128);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PersonalBlog.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternativeName")
                        .HasMaxLength(64);

                    b.Property<DateTime?>("DeleteDateTime");

                    b.Property<int?>("DeleteUserId");

                    b.Property<string>("FocusKeyword")
                        .HasMaxLength(32);

                    b.Property<string>("ImagePath")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("InsertDateTime");

                    b.Property<int?>("InsertUserId");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(512);

                    b.Property<string>("MetaKeywords")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("ParentCategoryId");

                    b.Property<string>("Summary")
                        .HasMaxLength(1024);

                    b.Property<string>("Text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateDateTime");

                    b.Property<int?>("UpdateUserId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("VisitCount");

                    b.HasKey("Id");

                    b.HasIndex("DeleteUserId");

                    b.HasIndex("InsertUserId");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("PersonalBlog.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DeleteDateTime");

                    b.Property<int?>("DeleteUserId");

                    b.Property<string>("FocusKeyword")
                        .HasMaxLength(32);

                    b.Property<string>("ImagePath")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("InsertDateTime");

                    b.Property<int?>("InsertUserId");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsEnable");

                    b.Property<bool>("IsPin");

                    b.Property<int?>("LocationId");

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(512);

                    b.Property<string>("MetaKeywords")
                        .HasMaxLength(256);

                    b.Property<string>("SourceName")
                        .HasMaxLength(32);

                    b.Property<string>("SourceUrl")
                        .HasMaxLength(256);

                    b.Property<string>("Summary")
                        .HasMaxLength(1024);

                    b.Property<string>("Text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateDateTime");

                    b.Property<int?>("UpdateUserId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("VisitCount");

                    b.HasKey("Id");

                    b.HasIndex("DeleteUserId");

                    b.HasIndex("InsertUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("PersonalBlog.Models.PostCategory", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("CategoryId");

                    b.HasKey("PostId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PostCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PersonalBlog.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PersonalBlog.Models.Category", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationUser", "DeleteUser")
                        .WithMany()
                        .HasForeignKey("DeleteUserId");

                    b.HasOne("PersonalBlog.Models.ApplicationUser", "InsertUser")
                        .WithMany()
                        .HasForeignKey("InsertUserId");

                    b.HasOne("PersonalBlog.Models.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.HasOne("PersonalBlog.Models.ApplicationUser", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId");
                });

            modelBuilder.Entity("PersonalBlog.Models.Post", b =>
                {
                    b.HasOne("PersonalBlog.Models.ApplicationUser", "DeleteUser")
                        .WithMany()
                        .HasForeignKey("DeleteUserId");

                    b.HasOne("PersonalBlog.Models.ApplicationUser", "InsertUser")
                        .WithMany()
                        .HasForeignKey("InsertUserId");

                    b.HasOne("PersonalBlog.Models.ApplicationUser", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId");
                });

            modelBuilder.Entity("PersonalBlog.Models.PostCategory", b =>
                {
                    b.HasOne("PersonalBlog.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PersonalBlog.Models.Post", "Post")
                        .WithMany("Categories")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}