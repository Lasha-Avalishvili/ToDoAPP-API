using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp_API.Db.Entities;

namespace ToDoApp_API.Db;


public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

    protected override void OnModelCreating(ModelBuilder builder) // this function is to name Db tables
    {
        base.OnModelCreating(builder);  
        
        builder.Entity<UserEntity>().ToTable("users");
        builder.Entity<RoleEntity>().ToTable("Roles");
        builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
    }
}

