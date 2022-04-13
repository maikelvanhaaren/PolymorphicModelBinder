namespace PolymorphicModelBinder.Samples.Mvc.Models.Devices
{
    public class SmartPhone : IDevice
    {
        public string Discriminator => nameof(SmartPhone);
        public string Brand { get; set; }
    }
}