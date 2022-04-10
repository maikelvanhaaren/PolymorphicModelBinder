namespace PolymorphicModelBinder.Samples.Mvc.Models;

public interface IDevice
{
    string Discriminator { get; }
    public string Brand { get; set; }
}