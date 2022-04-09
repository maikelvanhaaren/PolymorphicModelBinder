namespace PolymorphicModelBinder.Samples.Mvc.Models;

public class SampleViewModel
{
    public SampleViewModel()
    {
        
    }

    public SampleViewModel(Pet pet)
    {
        Pet = pet;
    }
    public Pet Pet { get; set; }
}