using System;
using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models;
using PolymorphicModelBinder.Samples.Mvc.Models.Pets;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers
{
    public class PolymorphicAsPropertyController : Controller
    {
        [HttpGet]
        public IActionResult Index(string type = "Dog")
        {
            Pet pet = type switch
            {
                "Dog" => new Dog(),
                "Cat" => new Cat(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
            return View(new PolymorphicAsPropertyViewModel(pet));
        }

        [HttpPost]
        public IActionResult Index(PolymorphicAsPropertyViewModel viewModel) => View(viewModel);
    }
}