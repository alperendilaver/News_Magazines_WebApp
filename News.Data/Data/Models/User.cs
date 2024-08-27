using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace News.Data.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set;} = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; } 
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;
        public List<UserTokens> UserTokens { get; set; } =new List<UserTokens>();

        public Author? Author { get; set; } // Kullanıcının yazar olup olmadığını belirler

        public ICollection<OldPasswords> UserOldPasswords{ get; set; } = new List<OldPasswords>();

        public List<Comment>? Comments { get; set; }
        public List<CommentReply>? CommentReplies { get; set; }

        public List<WarningComment>?Warnings { get; set; }

        public List<NewReaction>? Reactions { get; set; }

        public List<CommentReaction>? CommentReactions { get; set; }

        public List<Channel>? Channels { get; set; }

        public List<PostReaction>? PostReactions { get; set; }

        public List<ChannelRequest>? ChannelRequests { get; set; }

        public List<Notify>? Notifies { get; set; }
    }
}