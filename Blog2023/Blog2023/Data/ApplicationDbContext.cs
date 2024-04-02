using Blog.Data.Models;
using Blog2023.Data.DTO;
using Blog2023.Data.Models;
using Blog2023.Migrations;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TagModel> Tag { get; set; }
        public DbSet<AuthorModel> Author { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<CommunityModel> Community { get; set; }
        public DbSet<MyCommunityModel> MyCommunity { get; set; }

        public DbSet<PostModel> Posts { get; set; }

        public DbSet<TagPost> TagPosts { get; set; }

        public DbSet<CommentModel> CommentModels { get; set; }

        public DbSet<CommunityUserModel> CommunityUserModels { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }

        public async Task<List<TagModel>> GetTagsFromDatabase()
        {
            return await Tag.ToListAsync();
        }


        public async Task<List<AuthorModel>> GetAuthorFromDatabase()
        {
            return await Author.ToListAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<TagModel>().HasKey(x => x.Id);
            modelBuilder.Entity<Token>().HasKey(x => x.InvalidToken);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.FullName).IsUnique();
            modelBuilder.Entity<AuthorModel>().HasKey(x => x.id);
            modelBuilder.Entity<CommunityModel>().HasKey(x => x.Id);

        }
    }
}


