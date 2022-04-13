using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Mvc.Models;
using PolymorphicModelBinder.Samples.Mvc.Models.Devices;

namespace PolymorphicModelBinder.Samples.Mvc.Controllers;

public class DynamicPolymorphicListController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new DynamicPolymorphicListViewModel()
        {
            Pets = new List<Pet>()
            {
                new Cat()
                {
                    Name = "Garfield",
                    CanMeow = true
                },
                new Dog()
                {
                    Name = "Noeska",
                    CanBark = true,
                }
            }
        });
    }
    
    [HttpPost]
    public IActionResult Index(DynamicPolymorphicListViewModel viewModel)
    {
        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult AddEntry(DynamicPolymorphicListViewModel viewModel, string type)
    {
        viewModel.Pets.Add(type switch
        {
            "Dog" => new Dog() { Name = "New dog" },
            "Cat" => new Cat() { Name = "New cat" },
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        });
        return PartialView(viewModel);
    }
}