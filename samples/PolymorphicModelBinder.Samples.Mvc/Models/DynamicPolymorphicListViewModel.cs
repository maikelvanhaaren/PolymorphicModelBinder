using System.Collections;
using System.Collections.Generic;
using PolymorphicModelBinder.Samples.Mvc.Models.Devices;
using PolymorphicModelBinder.Samples.Mvc.Models.Pets;

namespace PolymorphicModelBinder.Samples.Mvc.Models
{
    public class DynamicPolymorphicListViewModel
    {
        public DynamicPolymorphicListViewModel()
        {
            Pets = new List<Pet>();
        }
    
        public IList<Pet> Pets { get; set; }
    }
}