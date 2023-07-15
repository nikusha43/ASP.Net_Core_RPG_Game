﻿// <auto-generated />
using System;
using DotNetRPG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetRPG.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsId")
                        .HasColumnType("int");

                    b.HasKey("CharactersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("CharacterSkill");
                });

            modelBuilder.Entity("DotNetRPG.Model.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Defeats")
                        .HasColumnType("int");

                    b.Property<int>("Defence")
                        .HasColumnType("int");

                    b.Property<int>("Fights")
                        .HasColumnType("int");

                    b.Property<int>("Hitpoint")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Victorys")
                        .HasColumnType("int");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("characters");
                });

            modelBuilder.Entity("DotNetRPG.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CharacterId = 0,
                            Damage = 1400,
                            Name = "Rasengan"
                        },
                        new
                        {
                            Id = 2,
                            CharacterId = 0,
                            Damage = 1000,
                            Name = "kamehameha"
                        },
                        new
                        {
                            Id = 3,
                            CharacterId = 0,
                            Damage = 300,
                            Name = "clone"
                        },
                        new
                        {
                            Id = 4,
                            CharacterId = 0,
                            Damage = 900,
                            Name = "Fire Ball"
                        },
                        new
                        {
                            Id = 5,
                            CharacterId = 0,
                            Damage = 1500,
                            Name = "RasenShuriken"
                        },
                        new
                        {
                            Id = 6,
                            CharacterId = 0,
                            Damage = 0,
                            Name = "Windwall"
                        });
                });

            modelBuilder.Entity("DotNetRPG.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DotNetRPG.Model.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("weapons");
                });

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.HasOne("DotNetRPG.Model.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotNetRPG.Model.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotNetRPG.Model.Character", b =>
                {
                    b.HasOne("DotNetRPG.Model.User", "user")
                        .WithMany("characters")
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("DotNetRPG.Model.Weapon", b =>
                {
                    b.HasOne("DotNetRPG.Model.Character", "Character")
                        .WithOne("weapon")
                        .HasForeignKey("DotNetRPG.Model.Weapon", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DotNetRPG.Model.Character", b =>
                {
                    b.Navigation("weapon")
                        .IsRequired();
                });

            modelBuilder.Entity("DotNetRPG.Model.User", b =>
                {
                    b.Navigation("characters");
                });
#pragma warning restore 612, 618
        }
    }
}