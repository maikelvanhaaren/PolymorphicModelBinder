namespace PolymorphicModelBinder.Samples.Mvc.Models;

public class SampleViewModel
{
    public Pet Pet { get; set; }
}

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