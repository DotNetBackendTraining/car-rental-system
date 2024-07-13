using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Web.Controllers;

public class FindController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}