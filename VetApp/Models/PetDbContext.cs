using Microsoft.EntityFrameworkCore;
namespace VetApp.Models
{
    public class PetDbContext: DbContext
    {

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public PetDbContext(DbContextOptions<PetDbContext> options): base(options)
        {
        }


    }
}
