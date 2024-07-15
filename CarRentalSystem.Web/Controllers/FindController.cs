using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Web.Controllers;

[Authorize]
public class FindController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}