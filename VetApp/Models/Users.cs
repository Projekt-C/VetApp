using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp.Models
{
    public class Users
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public string Phone { get; set; }


        public ICollection<Reservation> Reservations { get; set; }

        
    }
}
