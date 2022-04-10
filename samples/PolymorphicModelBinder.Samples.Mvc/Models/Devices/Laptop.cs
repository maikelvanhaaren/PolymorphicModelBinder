namespace PolymorphicModelBinder.Samples.Mvc.Models;

class Laptop : IDevice
{
    public string Discriminator => nameof(Laptop);
    public string Brand { get; set; }
}