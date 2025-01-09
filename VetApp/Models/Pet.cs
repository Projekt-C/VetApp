namespace VetApp.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string Breed { get; set; }
        public bool IsTaken { get; set; }   
        public DateOnly DateOfBirth { get; set; } 
        public string ImageUrl { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
