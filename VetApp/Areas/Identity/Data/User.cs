using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VetApp.Models;

namespace VetApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public int Id { get; set; }


    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public string Phone { get; set; }


    public ICollection<Reservation> Reservations { get; set; }
}

