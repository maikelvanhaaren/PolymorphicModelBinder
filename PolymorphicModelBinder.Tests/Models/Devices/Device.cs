namespace PolymorphicModelBinder.Tests.Models.Devices
{
    public abstract class Device : IDevice
    {
        public string Brand { get; set; } = "Unknown";
    }
}