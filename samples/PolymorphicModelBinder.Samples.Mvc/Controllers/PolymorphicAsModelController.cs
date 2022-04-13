using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers;

public class PolymorphicAsModelController : Controller
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
        return View(pet);
    }

    [HttpPost]
    public IActionResult Index(Pet pet) => View(pet);
}