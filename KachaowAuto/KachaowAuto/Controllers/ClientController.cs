using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        public IActionResult Dashboard()
        {
            return View("~/Views/Dashboard/Client.cshtml");
        }
    }
}
