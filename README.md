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