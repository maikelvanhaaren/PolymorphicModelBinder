namespace PolymorphicModelBinder.Samples.Mvc.Models.Devices;

public interface IDevice
{
    string Discriminator { get; }
    public string Brand { get; set; }
}