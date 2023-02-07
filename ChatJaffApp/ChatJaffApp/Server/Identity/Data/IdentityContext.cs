using ChatJaffApp.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatJaffApp.Server.Identity.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);
            SeedRoles(builder);

        }
        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser member1 = new ApplicationUser()
            {
                Id = "9CEAA7AB-1C67-4ED4-A86A-6BD01DF6C310",
                UserName = "member1",
                NormalizedUserName = "MEMBER1",
                Email = "member1@gmail.com",
                NormalizedEmail = "MEMBER1@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
            };
            member1.PasswordHash = passwordHasher.HashPassword(member1, "member1");

            ApplicationUser member2 = new ApplicationUser()
            {
                Id = "b8381d75-d110-42f9-85e5-9c92a062fbc8",
                UserName = "member2",
                NormalizedUserName = "MEMBER2",
                Email = "member2@gmail.com",
                NormalizedEmail = "MEMBER2@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
            };

            member2.PasswordHash = passwordHasher.HashPassword(member2, "member2");

            ApplicationUser banned1 = new ApplicationUser()
            {
                Id = "b8381d75-d110-42f9-85e5-9c92a062abc1",
                UserName = "banned1",
                NormalizedUserName = "BANNED1",
                Email = "banned1@gmail.com",
                NormalizedEmail = "BANNED1@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                IsBanned = true
            };

            banned1.PasswordHash = passwordHasher.HashPassword(banned1, "banned1");

            var listOfInitialUsers = new List<ApplicationUser>()
            {
                member1, member2, banned1
            };
            builder.Entity<ApplicationUser>().HasData(listOfInitialUsers);
        }

        private void SeedRoles(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "MemberId",
                    Name = "Member",
                    ConcurrencyStamp = "1",
                    NormalizedName = "MEMBER"
                },
                new IdentityRole()
                {
                    Id = "ModeratorId",
                    Name = "Moderator",
                    ConcurrencyStamp = "1",
                    NormalizedName = "MODERATOR"
                }
                );
        }
    }
}
