using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using VetApp.Areas.Identity.Data;
using VetApp.Migrations;
using VetApp.Models;

namespace VetApp.Controllers
{
    public class AdminController : Controller
    {
        
        public class UrlResponse
        {
           public string message { get; set; }
            public string status { get; set; }
        }

        public class AllPetResponse
        {
            public Dictionary<string, List<string>> message { get; set; }
            public string status { get; set; }
        }


        private readonly ILogger<AdminController> _logger;


        private readonly PetDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AdminController(PetDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var pets = _context.Pets.ToList();
            return View(pets);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddPet()
        {
            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://dog.ceo/api/breeds/list/all");
            }
            var data = JsonConvert.DeserializeObject<AllPetResponse>(json);
            ViewBag.Breeds = data.message.Keys;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPet([Bind("Id, Name, Breed, IsTaken, DateOfBirth, Description, ImageUrl, Reservations")] Pet pet)
        {
            try
            {
                pet.ImageUrl = GenerateImage(pet.Breed);
                _context.Pets.Add(pet);
                _context.SaveChanges();
                ViewBag.Result = "Dodano nowego pupila";
                return View("Wynik", pet);
            }
            catch
            {
                ViewBag.Result = "Nie udało się dodać nowego pupila";
                return View(pet);
            }
        }
        public IActionResult Wynik(Pet pet)
        {
            return View(pet);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeletePet()
        {
            return View();
        }

        [HttpPost("Admin/DeletePet/{petId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePet(int petId)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.Id == petId);
            _context.Pets.Remove(pet);
            _context.SaveChanges();
            ViewBag.Result = "Usunięto pupila";
            return View("Wynik");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditPet()
        {
            return View();
        }
        [HttpPost("Admin/EditPet/{petId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditPet([Bind("Id, Name, Breed, IsTaken, DateOfBirth, Description, ImageUrl, Reservations")] Pet pet, int petId)
        {
            var oldPet = _context.Pets.Find(petId);
            oldPet.Name = pet.Name;
            oldPet.Description = pet.Description;
            _context.SaveChanges();
            ViewBag.Result = "Zaktualizowano dane pupila";
            return View("Wynik");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Reserve()
        {
           return View();
        }

        [HttpPost("Admin/Reserve/{petId}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Reserve([Bind("Id, Date, Time, PetId, UserId")] Reservation reservation, [FromRoute] int petId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                reservation.PetId = petId;
                reservation.UserId = userId;
                _userManager.AddToRoleAsync(_context.Users.Find(userId), "Taken");
                _context.SaveChanges();
                _context.Pets.Find(petId).IsTaken = true;
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                ViewBag.Result = "Rezerwacja zakończona pomyślnie";
                return View("Wynik");
                }
            catch
            { 
                ViewBag.Result = "Rezerwacja nie powiodła się";
                return View("Wynik");
            }
        }

        [HttpGet("status")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

     

        public string GenerateImage(string breed)
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

