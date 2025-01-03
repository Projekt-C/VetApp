using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPet(Pet pet)
        {
            return View("Wynik", pet);
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
