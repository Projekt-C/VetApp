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
    }
}
