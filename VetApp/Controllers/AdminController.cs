﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
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
                pet.ImageUrl = Kutas(pet.Breed);
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
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                return View("Wynik", reservation);
                }
            catch
            { 
                return View("AddPet");
            }
        }

        [HttpGet("status")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string Kutas(string breed)
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

