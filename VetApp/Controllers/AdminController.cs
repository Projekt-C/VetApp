using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VetApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VetApp.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult AddPet()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddPet([Bind("Id, Name, Species, Breed, IsTaken, DateOfBirth, Reservations")] Pet pet)
        {
            try
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();
                return View("Wynik", pet);
            }
            catch
            {
                return View(pet);
            }
        }
        public IActionResult Wynik(Pet pet)
        {
            return View(pet);
        }

        private readonly PetDbContext _context;

        public AdminController(PetDbContext context)
        {
            _context = context;
        }
    }
}
