namespace PolymorphicModelBinder.Tests.Models.Pets
{
    public class Dog : IPet
    {
        public string Discriminator => nameof(Dog);
        public string Name { get; set; } = string.Empty;
    }
}