using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}