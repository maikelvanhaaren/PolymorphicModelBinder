using System.Collections;
using PolymorphicModelBinder.Samples.Mvc.Models.Devices;

namespace PolymorphicModelBinder.Samples.Mvc.Models;

public class DynamicPolymorphicListViewModel
{
    public DynamicPolymorphicListViewModel()
    {
        Pets = new List<Pet>();
    }
    
    public IList<Pet> Pets { get; set; }
}