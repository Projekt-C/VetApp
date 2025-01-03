using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp.Models
{
    public class Users
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column(TypeName = "bool")]
        public bool IsAdmin { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Phone { get; set; }


        public ICollection<Reservation> Reservations { get; set; }

        
    }
}
