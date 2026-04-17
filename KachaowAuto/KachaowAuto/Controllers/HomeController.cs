using KachaowAuto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KachaowAuto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Users", "Admin"); 
                }

                if (User.IsInRole("Mechanic"))
                {
                    return RedirectToAction("Dashboard", "Mechanic");
                }

                if (User.IsInRole("Client"))
                {
                    return RedirectToAction("Dashboard", "Client");
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
