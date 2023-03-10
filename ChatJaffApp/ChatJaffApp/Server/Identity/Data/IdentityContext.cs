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
            SeedUserRoles(builder);

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
                LockoutEnabled = true,
                EmailConfirmed = true,
                AgreedToUserTerms=true,
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
                AgreedToUserTerms=true,
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
                IsBanned = true,
                AgreedToUserTerms=true,
            };

            banned1.PasswordHash = passwordHasher.HashPassword(banned1, "banned1");

            ApplicationUser toBan1 = new ApplicationUser()
            {
                Id = "b8381d75-d110-42f9-85e5-9c92a062a111",
                UserName = "toBan1",
                NormalizedUserName = "TOBAN1",
                Email = "toBan1@gmail.com",
                NormalizedEmail = "TOBAN1@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                IsBanned = false,
                AgreedToUserTerms=true,
            };

            toBan1.PasswordHash = passwordHasher.HashPassword(banned1, "toban1");

            ApplicationUser admin1 = new ApplicationUser()
            {
                Id = "b8381d75-d110-42f9-85e5-9c92a0111123",
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@gmail.com",
                NormalizedEmail = "ADMIN1@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                IsBanned = false,
                AgreedToUserTerms=true,
                
            };

            admin1.PasswordHash = passwordHasher.HashPassword(banned1, "admin1");

            var listOfInitialUsers = new List<ApplicationUser>()
            {
                member1, member2, banned1, toBan1, admin1
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
                },
                new IdentityRole()
                {
                    Id = "AdminId",
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN"
                }
                );
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "MemberId",
                    UserId = "9CEAA7AB-1C67-4ED4-A86A-6BD01DF6C310"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "MemberId",
                    UserId = "b8381d75-d110-42f9-85e5-9c92a062fbc8"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "AdminId",
                    UserId = "b8381d75-d110-42f9-85e5-9c92a0111123"
                }
                );
        }
    }
}
