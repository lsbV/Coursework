﻿// <auto-generated />
using System;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DALTestsDB.Migrations
{
    [DbContext(typeof(TestDBContext))]
    partial class TestDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DALTestsDB.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Group");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Admins group",
                            IsArchived = false,
                            Name = "Admins"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Users group",
                            IsArchived = false,
                            Name = "Users"
                        });
                });

            modelBuilder.Entity("DALTestsDB.TestAssigned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeToTake")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestAssigned");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndAt = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsArchived = false,
                            StartAt = new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TestId = 1,
                            TimeToTake = new TimeSpan(0, 1, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("DALTestsDB.TestAssignedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TestAssignedId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestAssignedId");

                    b.HasIndex("UserId");

                    b.ToTable("TestAssignedUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TestAssignedId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            TestAssignedId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            TestAssignedId = 1,
                            UserId = 4
                        });
                });

            modelBuilder.Entity("DALTestsDB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2000, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            IsArchived = false,
                            LastName = "Admin",
                            Login = "admin",
                            Password = "admin",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "User",
                            IsArchived = false,
                            LastName = "User",
                            Login = "user",
                            Password = "user",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2000, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "User1",
                            IsArchived = true,
                            LastName = "User1",
                            Login = "user1",
                            Password = "user1",
                            Role = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2000, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "User2",
                            IsArchived = false,
                            LastName = "User2",
                            Login = "user2",
                            Password = "user2",
                            Role = 1
                        });
                });

            modelBuilder.Entity("TestLib.Abstractions.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Answer");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TestLib.Abstractions.Body", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("Body");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TestLib.Abstractions.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BodyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Task");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TestLib.Classes.Test.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoForTestTaker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<double>("PassingPercent")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Test");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Admin",
                            CreatedAt = new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Test1",
                            InfoForTestTaker = "Test1",
                            IsArchived = false,
                            PassingPercent = 50.0,
                            Title = "Test1"
                        });
                });

            modelBuilder.Entity("TestLib.Interfaces.UserGroup", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroup");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            GroupId = 1
                        },
                        new
                        {
                            UserId = 2,
                            GroupId = 2
                        },
                        new
                        {
                            UserId = 3,
                            GroupId = 2
                        },
                        new
                        {
                            UserId = 4,
                            GroupId = 2
                        });
                });

            modelBuilder.Entity("TestLib.Classes.Answers.TextAnswer", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.Answer");

                    b.ToTable("TextAnswer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsCorrect = false,
                            TaskId = 1,
                            Text = "1"
                        },
                        new
                        {
                            Id = 2,
                            IsCorrect = false,
                            TaskId = 1,
                            Text = "2"
                        },
                        new
                        {
                            Id = 3,
                            IsCorrect = false,
                            TaskId = 1,
                            Text = "3"
                        },
                        new
                        {
                            Id = 4,
                            IsCorrect = true,
                            TaskId = 1,
                            Text = "4"
                        });
                });

            modelBuilder.Entity("TestLib.Classes.Bodies.TextBody", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.Body");

                    b.ToTable("TextBody");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TaskId = 1,
                            Text = "2+2 ="
                        });
                });

            modelBuilder.Entity("TestLib.Classes.Tasks.ChooseFromListTask", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.Task");

                    b.ToTable("ChooseFromListTask");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BodyId = 1,
                            Description = "ChooseFromListTask",
                            Point = 10.0,
                            TestId = 1
                        });
                });

            modelBuilder.Entity("DALTestsDB.TestAssigned", b =>
                {
                    b.HasOne("TestLib.Classes.Test.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("DALTestsDB.TestAssignedUser", b =>
                {
                    b.HasOne("DALTestsDB.TestAssigned", "TestAssigned")
                        .WithMany()
                        .HasForeignKey("TestAssignedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestAssigned");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestLib.Abstractions.Answer", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", "Task")
                        .WithMany("Answers")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TestLib.Abstractions.Body", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", "Task")
                        .WithOne("Body")
                        .HasForeignKey("TestLib.Abstractions.Body", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TestLib.Abstractions.Task", b =>
                {
                    b.HasOne("TestLib.Classes.Test.Test", "Test")
                        .WithMany("Tasks")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("TestLib.Interfaces.UserGroup", b =>
                {
                    b.HasOne("DALTestsDB.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestLib.Classes.Answers.TextAnswer", b =>
                {
                    b.HasOne("TestLib.Abstractions.Answer", null)
                        .WithOne()
                        .HasForeignKey("TestLib.Classes.Answers.TextAnswer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestLib.Classes.Bodies.TextBody", b =>
                {
                    b.HasOne("TestLib.Abstractions.Body", null)
                        .WithOne()
                        .HasForeignKey("TestLib.Classes.Bodies.TextBody", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestLib.Classes.Tasks.ChooseFromListTask", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", null)
                        .WithOne()
                        .HasForeignKey("TestLib.Classes.Tasks.ChooseFromListTask", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestLib.Abstractions.Task", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Body")
                        .IsRequired();
                });

            modelBuilder.Entity("TestLib.Classes.Test.Test", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
