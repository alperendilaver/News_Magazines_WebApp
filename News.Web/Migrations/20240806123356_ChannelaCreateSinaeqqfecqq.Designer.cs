﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using News.Data.Data.Context;

#nullable disable

namespace News.Web.Migrations
{
    [DbContext(typeof(NewsContext))]
    [Migration("20240806123356_ChannelaCreateSinaeqqfecqq")]
    partial class ChannelaCreateSinaeqqfecqq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorCategory", b =>
                {
                    b.Property<int>("AuthorsUserId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("AuthorsUserId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AuthorCategory");
                });

            modelBuilder.Entity("ChannelUser", b =>
                {
                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChannelId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ChannelUser");
                });

            modelBuilder.Entity("News.Data.Data.Models.Author", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("ChannelId")
                        .HasColumnType("int");

                    b.Property<string>("Unvan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("News.Data.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("News.Data.Data.Models.Channel", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChannelId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChannelId");

                    b.HasIndex("AuthorId")
                        .IsUnique();

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("News.Data.Data.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisslikeCount")
                        .HasColumnType("int");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<int>("NewId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReplyCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("NewId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("News.Data.Data.Models.CommentReaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLike")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CommentReactions");
                });

            modelBuilder.Entity("News.Data.Data.Models.CommentReply", b =>
                {
                    b.Property<int>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReplyId"));

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<int>("DisslikeCount")
                        .HasColumnType("int");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentReplies");
                });

            modelBuilder.Entity("News.Data.Data.Models.New", b =>
                {
                    b.Property<int>("NewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisslikeCount")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NewId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("News.Data.Data.Models.NewReaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReactionId"));

                    b.Property<bool>("IsLike")
                        .HasColumnType("bit");

                    b.Property<int>("NewId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReactionId");

                    b.HasIndex("NewId");

                    b.HasIndex("UserId");

                    b.ToTable("NewReactions");
                });

            modelBuilder.Entity("News.Data.Data.Models.NewRequests", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("NewId")
                        .HasColumnType("int");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("NewId");

                    b.ToTable("NewRequests");
                });

            modelBuilder.Entity("News.Data.Data.Models.OldPasswords", b =>
                {
                    b.Property<int>("UserPasswordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserPasswordID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserPasswordID");

                    b.HasIndex("UserId");

                    b.ToTable("OldPasswords");
                });

            modelBuilder.Entity("News.Data.Data.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CnId")
                        .HasColumnType("int");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PostId");

                    b.HasIndex("AuthorId");

                    b.ToTable("ChannelPosts");
                });

            modelBuilder.Entity("News.Data.Data.Models.PostReaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReactionId"));

                    b.Property<bool>("IsLike")
                        .HasColumnType("bit");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReactionId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostReaction");
                });

            modelBuilder.Entity("News.Data.Data.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Reader"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Author"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "SuperAdmin"
                        },
                        new
                        {
                            RoleId = 4,
                            RoleName = "Editor"
                        });
                });

            modelBuilder.Entity("News.Data.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "superadmin@gmail.com",
                            FirstName = "Super",
                            HashedPassword = "8dad25413cafaccbb7cb870bf5df53a7ff054eb8e6eaae14189167ea683b4036",
                            Image = "",
                            IsEmailConfirmed = true,
                            LastName = "Admin",
                            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                            PhoneNumber = "5314021111",
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("News.Data.Data.Models.UserCategoryRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCategoryRequests");
                });

            modelBuilder.Entity("News.Data.Data.Models.UserTokens", b =>
                {
                    b.Property<int>("UserTokensId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTokensId"));

                    b.Property<bool>("IsProceed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TokenType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserTokensId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("News.Data.Data.Models.WarningComment", b =>
                {
                    b.Property<int>("WarnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarnId"));

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReplyId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WarnId");

                    b.HasIndex("CommentId");

                    b.HasIndex("ReplyId");

                    b.HasIndex("UserId");

                    b.ToTable("WarningComments");
                });

            modelBuilder.Entity("AuthorCategory", b =>
                {
                    b.HasOne("News.Data.Data.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChannelUser", b =>
                {
                    b.HasOne("News.Data.Data.Models.Channel", null)
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("News.Data.Data.Models.Author", b =>
                {
                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithOne("Author")
                        .HasForeignKey("News.Data.Data.Models.Author", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.Channel", b =>
                {
                    b.HasOne("News.Data.Data.Models.Author", "Author")
                        .WithOne("Channel")
                        .HasForeignKey("News.Data.Data.Models.Channel", "AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("News.Data.Data.Models.Comment", b =>
                {
                    b.HasOne("News.Data.Data.Models.New", "New")
                        .WithMany("Comments")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("New");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.CommentReaction", b =>
                {
                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("CommentReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.CommentReply", b =>
                {
                    b.HasOne("News.Data.Data.Models.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("CommentReplies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.New", b =>
                {
                    b.HasOne("News.Data.Data.Models.Category", "Category")
                        .WithMany("News")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.Author", "User")
                        .WithMany("News")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.NewReaction", b =>
                {
                    b.HasOne("News.Data.Data.Models.New", "New")
                        .WithMany("Reactions")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("Reactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("New");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.NewRequests", b =>
                {
                    b.HasOne("News.Data.Data.Models.New", "New")
                        .WithMany("NewRequests")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("New");
                });

            modelBuilder.Entity("News.Data.Data.Models.OldPasswords", b =>
                {
                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("UserOldPasswords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.Post", b =>
                {
                    b.HasOne("News.Data.Data.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.Channel", "Channel")
                        .WithMany("Posts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("News.Data.Data.Models.PostReaction", b =>
                {
                    b.HasOne("News.Data.Data.Models.Post", "Post")
                        .WithMany("Reactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("PostReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.User", b =>
                {
                    b.HasOne("News.Data.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("News.Data.Data.Models.UserCategoryRequest", b =>
                {
                    b.HasOne("News.Data.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.UserTokens", b =>
                {
                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.WarningComment", b =>
                {
                    b.HasOne("News.Data.Data.Models.Comment", "Comment")
                        .WithMany("Warnings")
                        .HasForeignKey("CommentId");

                    b.HasOne("News.Data.Data.Models.CommentReply", "CommentReply")
                        .WithMany("Warnings")
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("News.Data.Data.Models.User", "User")
                        .WithMany("Warnings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("CommentReply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("News.Data.Data.Models.Author", b =>
                {
                    b.Navigation("Channel");

                    b.Navigation("News");
                });

            modelBuilder.Entity("News.Data.Data.Models.Category", b =>
                {
                    b.Navigation("News");
                });

            modelBuilder.Entity("News.Data.Data.Models.Channel", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("News.Data.Data.Models.Comment", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("Warnings");
                });

            modelBuilder.Entity("News.Data.Data.Models.CommentReply", b =>
                {
                    b.Navigation("Warnings");
                });

            modelBuilder.Entity("News.Data.Data.Models.New", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("NewRequests");

                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("News.Data.Data.Models.Post", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("News.Data.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("News.Data.Data.Models.User", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("CommentReactions");

                    b.Navigation("CommentReplies");

                    b.Navigation("Comments");

                    b.Navigation("PostReactions");

                    b.Navigation("Reactions");

                    b.Navigation("UserOldPasswords");

                    b.Navigation("UserTokens");

                    b.Navigation("Warnings");
                });
#pragma warning restore 612, 618
        }
    }
}
