using Microsoft.EntityFrameworkCore;
namespace VetApp.Models
{
    public class PetDbContext: DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options): base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
