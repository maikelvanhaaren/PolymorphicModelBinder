using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers;

public class HomeController : Controller
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

        return View(new SampleViewModel()
        {
            Pet = pet
        });
    }

    [HttpPost]
    public IActionResult Index(SampleViewModel viewModel)
    {
        return View(viewModel);
    }
}