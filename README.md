# PolymorphicModelBinder

A polymorphic model binder for ASP.net core. Bind to the type you want! 

## Usage

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

    // Add model binder
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
        @Html.EditorFor(x => x.Pet)
        <button type="submit" class="btn btn-primary btn-lg">Submit</button>
    }

[MVC Sample project](./samples/PolymorphicModelBinder.Samples.Mvc)
