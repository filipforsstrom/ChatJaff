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
                Id = "m1",
                UserName = "member1",
                NormalizedUserName = "MEMBER1",
                Email = "member1@gmail.com",
                NormalizedEmail = "MEMBER1@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
            };
            member1.PasswordHash = passwordHasher.HashPassword(member1, "member1");

            builder.Entity<ApplicationUser>().HasData(member1);
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
