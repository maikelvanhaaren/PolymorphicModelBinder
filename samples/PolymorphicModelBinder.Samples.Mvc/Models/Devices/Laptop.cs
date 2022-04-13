namespace PolymorphicModelBinder.Samples.Mvc.Models.Devices
{
    public class Laptop : IDevice
    {
        public string Discriminator => nameof(Laptop);
        public string Brand { get; set; }
    }
}