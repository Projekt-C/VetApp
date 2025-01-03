using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } 

        [Column(TypeName = "nvarchar(100)")]
        public string Species { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Breed { get; set; }

        [Column(TypeName = "bool")]
        public bool IsTaken { get; set; }   
    
        [Column(TypeName = "date")]
        public string DateOfBirth { get; set; } 

        public ICollection<Reservation> Reservations { get; set; }
    }
}
