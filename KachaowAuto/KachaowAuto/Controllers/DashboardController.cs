using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Client")]
        public IActionResult Client() => View();

        [Authorize(Roles = "Mechanic")]
        public IActionResult Mechanic() => View();
    }
}
