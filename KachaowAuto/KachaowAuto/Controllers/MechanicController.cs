using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Mechanic")]
    public class MechanicController : Controller
    {
        public IActionResult Dashboard()
        {
            return View("~/Views/Dashboard/Mechanic.cshtml");
        }
    }
}
