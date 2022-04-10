namespace PolymorphicModelBinder.Tests.Models.Pets;

public class Cat : IPet
{
    public string Discriminator => nameof(Cat);
    public string Name { get; set; } = string.Empty;
}