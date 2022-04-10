namespace PolymorphicModelBinder.Tests.Models.Pets
{
    public interface IPet
    {
        public string Discriminator { get; }
        public string Name { get; set; }
    }
}