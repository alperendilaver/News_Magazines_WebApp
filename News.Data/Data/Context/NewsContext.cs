using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using News.Data.Data.Models;

namespace News.Data.Data.Context
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role{RoleId=1, RoleName="Reader"},
                    new Role{RoleId=2, RoleName="Author"},
                    new Role{RoleId=3, RoleName="SuperAdmin"},
                    new Role{RoleId=4, RoleName="Editor"}
                );    
            modelBuilder.Entity<User>()
                .HasData(
                    new User{UserId = 1, Email = "superadmin@gmail.com", FirstName="Super", LastName="Admin", IsEmailConfirmed=true, RoleId=3, PhoneNumber="5314021111", HashedPassword="8dad25413cafaccbb7cb870bf5df53a7ff054eb8e6eaae14189167ea683b4036", NormalizedEmail="SUPERADMIN@GMAIL.COM"}
                    
                    // new User{UserId = 2, Email = "editor@gmail.com", FirstName="Editor", LastName="Editor", IsEmailConfirmed=true, RoleId=4, PhoneNumber="5314021112", HashedPassword="36f23c82189796c52a78f62a0340168568825ebde94d0cb8bab19094847d461b", NormalizedEmail="EDITOR@GMAIL.COM"}
                );

            modelBuilder.Entity<New>()
                .HasOne(n => n.Category)
                .WithMany(c => c.News)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // OnDelete kuralı ayarlandı

            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<UserTokens>()
                .HasKey(ut => ut.UserTokensId); // TokenId özelliğini birincil anahtar olarak belirt

            modelBuilder.Entity<UserTokens>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTokens)
                .HasForeignKey(ut => ut.UserId); // User ile UserTokens arasında bir ilişki tanımla
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.Author)
                .WithOne(a => a.User)
                .HasForeignKey<Author>(a => a.UserId);
        
            modelBuilder.Entity<NewRequests>()
                .HasOne(a => a.New)
                .WithMany(u => u.NewRequests)
                .HasForeignKey(nr => nr.NewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OldPasswords>()
                .HasOne(a => a.User)
                .WithMany(x => x.UserOldPasswords)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CommentReply>()
                .HasOne(a => a.Comment)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade); // Döngüleri ve çoklu yolları önlemek için

            modelBuilder.Entity<CommentReply>()
                .HasOne(x => x.User)
                .WithMany(x => x.CommentReplies)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Döngüleri ve çoklu yolları önlemek için

            modelBuilder.Entity<CommentReaction>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.CommentReactions)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasOne(a => a.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.New)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NewId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WarningComment>()
                .HasOne(c => c.CommentReply)
                .WithMany(x => x.Warnings)
                .HasForeignKey(c => c.ReplyId)
                .OnDelete(DeleteBehavior.Cascade); // Delete işlemlerinde referanslı verileri korur
            modelBuilder.Entity<Data.Models.NewReaction>()
                .HasOne(c => c.New)
                .WithMany(x=>x.Reactions)
                .HasForeignKey(c => c.NewId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Data.Models.Channel>()
                .HasMany(x => x.Members)
                .WithMany(x=>x.Channels)
                .UsingEntity(
                    "ChannelUser",
                    r=>r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.UserId)).OnDelete(DeleteBehavior.NoAction),
                    l=>l.HasOne(typeof(Channel)).WithMany().HasForeignKey("ChannelId").HasPrincipalKey(nameof(Channel.ChannelId)).OnDelete(DeleteBehavior.Restrict),
                    j=>j.HasKey("ChannelId","UserId"));
            // modelBuilder.Entity<Channel>()
            //     .HasMany(x=>x.Posts)
            //     .WithOne(x=>x.Channel)
            //     .HasForeignKey(x=>x.ChannelId)
            //     .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Post>()
                .HasOne(x=>x.Channel)
                .WithMany(x=>x.Posts)
                .HasForeignKey(x=>x.ChannelId)
                .OnDelete(DeleteBehavior.NoAction);
              modelBuilder.Entity<PostReaction>()
                  .HasOne(x=>x.User)
                  .WithMany(x=>x.PostReactions)
                  .HasForeignKey(a=>a.UserId)
                  .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Reactions)
                .WithOne(pr => pr.Post)
                .HasForeignKey(pr => pr.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(x=>x.ChannelRequests)
                .WithOne(xc =>xc.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(x=>x.Notifies)
                .WithOne(xc =>xc.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<New> News => Set<New>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserTokens> UserTokens => Set<UserTokens>();
        public DbSet<UserCategoryRequest> UserCategoryRequests => Set<UserCategoryRequest>();
        public DbSet<NewRequests> NewRequests => Set<NewRequests>();
        public DbSet<Notify> Notifies => Set<Notify>();
        public DbSet<CommentReaction> CommentReactions => Set<CommentReaction>(); 
        public DbSet<CommentReply> CommentReplies => Set<CommentReply>();
        public DbSet<WarningComment> WarningComments => Set<WarningComment>();
    
        public DbSet<NewReaction> NewReactions => Set<NewReaction>();

        public DbSet<Channel> Channels=> Set<Channel>();
        public DbSet<Post> ChannelPosts=> Set<Post>();

        public DbSet<PostReaction> PostReactions => Set<PostReaction>();

        public DbSet<ChannelRequest> ChannelRequests => Set<ChannelRequest>();
        //public DbSet<ChannelUser> ChannelUser => Set<ChannelUser>();
    }
}
