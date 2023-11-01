﻿// <auto-generated />
using System;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DALTestsDB.Migrations
{
    [DbContext(typeof(TestDBContext))]
    [Migration("20231101090413_M8")]
    partial class M8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("DALTestsDB.Test", b =>
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
                });

            modelBuilder.Entity("DALTestsDB.TestAssigned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("ActiveTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestAssigned");
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
                            CreatedAt = new DateTime(2023, 11, 1, 11, 4, 12, 908, DateTimeKind.Local).AddTicks(2085),
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
                            CreatedAt = new DateTime(2023, 11, 1, 11, 4, 12, 908, DateTimeKind.Local).AddTicks(2124),
                            FirstName = "User",
                            IsArchived = false,
                            LastName = "User",
                            Login = "user",
                            Password = "user",
                            Role = 1
                        });
                });

            modelBuilder.Entity("DALTestsDB.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.Property<int>("UserTestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("UserTestId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("DALTestsDB.UserTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMissed")
                        .HasColumnType("bit");

                    b.Property<double?>("TaskGrade")
                        .HasColumnType("float");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserTestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserTestId");

                    b.ToTable("UserTask");
                });

            modelBuilder.Entity("DALTestsDB.UserTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMissed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PassageDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Result")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TestAssignedUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestAssignedUserId");

                    b.ToTable("UserTest");
                });

            modelBuilder.Entity("TestLib.Abstractions.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Answer");

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

                    b.Property<int?>("TestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BodyId");

                    b.HasIndex("TestId");

                    b.ToTable("Task");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TestLib.Abstractions.TaskBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaskBody");

                    b.UseTptMappingStrategy();
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
                });

            modelBuilder.Entity("TestLib.Classes.Answers.TextAnswer", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.Answer");

                    b.ToTable("TextAnswer");
                });

            modelBuilder.Entity("TestLib.Classes.Tasks.ChooseFromListTask", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.Task");

                    b.ToTable("ChooseFromListTask");
                });

            modelBuilder.Entity("TestLib.Classes.Bodies.TextTaskBody", b =>
                {
                    b.HasBaseType("TestLib.Abstractions.TaskBody");

                    b.ToTable("TextTaskBody");
                });

            modelBuilder.Entity("DALTestsDB.TestAssigned", b =>
                {
                    b.HasOne("DALTestsDB.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("DALTestsDB.TestAssignedUser", b =>
                {
                    b.HasOne("DALTestsDB.TestAssigned", "TestAssigned")
                        .WithMany("UserTests")
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

            modelBuilder.Entity("DALTestsDB.UserAnswer", b =>
                {
                    b.HasOne("TestLib.Abstractions.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.UserTask", "UserTest")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("UserTest");
                });

            modelBuilder.Entity("DALTestsDB.UserTask", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.UserTest", "UserTest")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("UserTest");
                });

            modelBuilder.Entity("DALTestsDB.UserTest", b =>
                {
                    b.HasOne("DALTestsDB.TestAssignedUser", "TestAssignedUser")
                        .WithMany()
                        .HasForeignKey("TestAssignedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestAssignedUser");
                });

            modelBuilder.Entity("TestLib.Abstractions.Answer", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", null)
                        .WithMany("Answers")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("TestLib.Abstractions.Task", b =>
                {
                    b.HasOne("TestLib.Abstractions.TaskBody", "Body")
                        .WithMany()
                        .HasForeignKey("BodyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.Test", null)
                        .WithMany("Tasks")
                        .HasForeignKey("TestId");

                    b.Navigation("Body");
                });

            modelBuilder.Entity("TestLib.Interfaces.UserGroup", b =>
                {
                    b.HasOne("DALTestsDB.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DALTestsDB.User", "User")
                        .WithMany("UserGroups")
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

            modelBuilder.Entity("TestLib.Classes.Tasks.ChooseFromListTask", b =>
                {
                    b.HasOne("TestLib.Abstractions.Task", null)
                        .WithOne()
                        .HasForeignKey("TestLib.Classes.Tasks.ChooseFromListTask", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestLib.Classes.Bodies.TextTaskBody", b =>
                {
                    b.HasOne("TestLib.Abstractions.TaskBody", null)
                        .WithOne()
                        .HasForeignKey("TestLib.Classes.Bodies.TextTaskBody", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DALTestsDB.Group", b =>
                {
                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("DALTestsDB.Test", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DALTestsDB.TestAssigned", b =>
                {
                    b.Navigation("UserTests");
                });

            modelBuilder.Entity("DALTestsDB.User", b =>
                {
                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("DALTestsDB.UserTask", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("DALTestsDB.UserTest", b =>
                {
                    b.Navigation("UserTasks");
                });

            modelBuilder.Entity("TestLib.Abstractions.Task", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
