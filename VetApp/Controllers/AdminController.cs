using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VetApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VetApp.Controllers
{
    public class AdminController : Controller
    {


        private readonly ILogger<AdminController> _logger;


        private readonly PetDbContext _context;

        public AdminController(PetDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pets = _context.Pets.ToList();
            return View(pets);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddPet()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPet([Bind("Id, Name, Species, Breed, IsTaken, DateOfBirth, Description, Reservations")] Pet pet)
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


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
