using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp.Models
{
    public class Reservation
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime Time { get; set; }

        [Column(TypeName = "int")]
        public int PetId { get; set; }

        public Pet Pet { get; set; }


        [Column(TypeName = "int")]
        public int UsersId { get; set; }

        public Users Users { get; set; }


    }
}
