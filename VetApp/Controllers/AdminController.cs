using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using VetApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VetApp.Controllers
{
    public class AdminController : Controller
    {
        
        public class UrlResponse
        {
           public string message { get; set; }
            public string status { get; set; }
        }


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
        public IActionResult AddPet([Bind("Id, Name, Breed, IsTaken, DateOfBirth, Description, ImageUrl, Reservations")] Pet pet)
        {
            try
            {
                pet.ImageUrl = kutas(pet.Breed);
                //pet.ImageUrl = $"https://dog.ceo/api/breed/{pet.Breed.ToLower()}/images/random";
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

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public IActionResult Reserve(Pet pet)
        {
           return View(pet);
        }

        [HttpGet("status")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string kutas(string breed)
        {
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString($"https://dog.ceo/api/breed/{breed.ToLower()}/images/random");
                var data = JsonConvert.DeserializeObject<UrlResponse>(s);
                return data.message;
            }
        }
    }
}

