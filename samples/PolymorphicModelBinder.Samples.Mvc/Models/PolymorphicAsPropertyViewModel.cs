namespace PolymorphicModelBinder.Samples.Mvc.Models;

public class PolymorphicAsPropertyViewModel
{
    public PolymorphicAsPropertyViewModel()
    {
        
    }

    public PolymorphicAsPropertyViewModel(Pet pet)
    {
        Pet = pet;
    }
    public Pet? Pet { get; set; }
}