namespace PolymorphicModelBinder.Samples.Mvc.Models;

class SmartPhone : IDevice
{
    public string Discriminator => nameof(SmartPhone);
    public string Brand { get; set; }
}