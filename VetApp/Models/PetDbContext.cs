using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetApp.Areas.Identity.Data;
namespace VetApp.Models
{
    public class PetDbContext: IdentityDbContext<User>
    {

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public PetDbContext(DbContextOptions<PetDbContext> options): base(options)
        {
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
}
