using System;
using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models.Devices;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers
{
    public class DiscriminatorController : Controller
    {
        public IActionResult Index(string type = "Laptop")
        {
            IDevice device = type switch
            {
                "Laptop" => new Laptop(),
                "SmartPhone" => new SmartPhone(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
            return View(device);
        }

        [HttpPost]
        public IActionResult Index(IDevice device) => View(device);
    }
}