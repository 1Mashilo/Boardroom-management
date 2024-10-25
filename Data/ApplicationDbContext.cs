using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace boardroom_management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Boardroom> Boardrooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles with provided IDs
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe",  // Admin role ID
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "3e382f61-b407-4e4c-b769-50cd04578451",  // User role ID
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            // Seed an admin user with a new unique ID
            var adminUserId = "4c5b1c3d-f5d6-4e2b-a3f0-314c1be8a7f5"; // Example GUID for admin user

            var adminUser = new ApplicationUser
            {
                Id = adminUserId,  // New unique ID for admin user
                UserName = "admin@boardroom.com",
                NormalizedUserName = "ADMIN@BOARDROOM.COM",
                Email = "admin@boardroom.com",
                NormalizedEmail = "ADMIN@BOARDROOM.COM",
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
            };
            
            // Set password for the admin user
            var hasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "AdminPassword123!");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign the Admin role to the admin user
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "208a99c0-04b3-41b2-8557-8e2a3f2bfcbe",  // Admin role ID
                    UserId = adminUserId  // Updated admin user ID
                }
            );
        }
    }
}
