using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetApp.Areas.Identity.Data;

namespace VetApp.Models
{
    public class Reservation
    {


        public int Id { get; set; }

    
        public DateOnly Date { get; set; }

 
        public DateOnly Time { get; set; }

        public int PetId { get; set; }

        public Pet Pet { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }


    }
}
