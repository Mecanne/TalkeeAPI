﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalkeeAPI.Models;

namespace TalkeeAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190617122325_Update_Followers_Follows_KEYS")]
    partial class Update_Followers_Follows_KEYS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TalkeeAPI.Models.Followers", b =>
                {
                    b.Property<int>("FollowID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FollowerID");

                    b.Property<int>("UserID");

                    b.HasKey("FollowID");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("TalkeeAPI.Models.Follows", b =>
                {
                    b.Property<int>("FollowID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FollowedID");

                    b.Property<int>("UserID");

                    b.HasKey("FollowID");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("TalkeeAPI.Models.PostModel", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<int>("UserID");

                    b.Property<int>("likes");

                    b.Property<string>("type");

                    b.HasKey("PostID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("TalkeeAPI.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.Property<string>("urlImagen")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("https://picsum.photos/400");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
