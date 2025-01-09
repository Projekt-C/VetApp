using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetApp.Areas.Identity.Data;

namespace VetApp.Data;

public class UserDbContext : IdentityDbContext<User>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).HasMaxLength(255);
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.Password).HasMaxLength(255);
        builder.Property(x => x.IsAdmin).HasMaxLength(255);
        builder.Property(x => x.Phone).HasMaxLength(255);
        builder.Property(x => x.Reservations).HasMaxLength(255);
    }

}
