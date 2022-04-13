# PolymorphicModelBinder

A polymorphic model binder for ASP.net Core. Bind to the type you want! 

## Getting Started

PolymorphicModelBinder can be installed using the Nuget package manager or the dotnet CLI.

```
dotnet add package PolymorphicModelBinder
```

## Usage
```csharp
// Models
public abstract class Pet
{
    public string Name { get; set; }
}
public class Dog : Pet
{
    public bool CanBark { get; set; }
}
public class Cat : Pet
{
    public bool CanMeow { get; set; }
}

// Add polymorphic model binder
builder.Services.AddPolymorphicModelBinder(options =>
{
    options.Add<Pet>(polymorphicBuilder =>
    {
        polymorphicBuilder.AddFromTypeInValue<Dog>();
        polymorphicBuilder.AddFromTypeInValue<Cat>();
    });
});

// Controller
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
        return View(new SampleViewModel(pet));
    }
    
    [HttpPost]
    public IActionResult Index(SampleViewModel viewModel) => View(viewModel);
}

// View
@using PolymorphicModelBinder.Html
@model SampleViewModel

@using (Html.BeginForm(FormMethod.Post))
{
    @Html.PolymorphicTypeInValueFor(x => x.Pet)
    @Html.PolymorphicEditorFor(x => x.Pet)
    <button type="submit" class="btn btn-primary btn-lg">Submit</button>
}
```

## Samples

- [Polymorphic model as root model](./samples/PolymorphicModelBinder.Samples.Mvc/Controllers/PolymorphicAsModelController.cs)
- [Polymorphic model as property](./samples/PolymorphicModelBinder.Samples.Mvc/Controllers/PolymorphicAsPropertyController.cs)
- [Dynamic polymorphic list](./samples/PolymorphicModelBinder.Samples.Mvc/Controllers/DynamicPolymorphicListController.cs)
- [Polymorphic model with discriminator field](./samples/PolymorphicModelBinder.Samples.Mvc/Controllers/DiscriminatorController.cs)

## Roadmap

If you want to see a new feature feel free to create a new issue. Here are some features which are planned when there is enough enthusiasm for this feature.

- [x] Basic implementation with a type in value and discriminator.
- [ ] Allow to use `@Html.PolymorphicEditorFor()` for `IEnumerable` models.
- [ ] Probably better documentation for usages.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
