using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp.Models
{
    public class Reservation
    {


        public int Id { get; set; }

    
        public DateTime Date { get; set; }

 
        public DateTime Time { get; set; }

        public int PetId { get; set; }

        public Pet Pet { get; set; }

        public int UsersId { get; set; }

        public Users Users { get; set; }


    }
}
