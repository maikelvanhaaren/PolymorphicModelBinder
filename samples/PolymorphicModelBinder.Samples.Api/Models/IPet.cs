namespace PolymorphicModelBinder.Samples.Api.Models
{
    public interface IPet
    {
        public string Discriminator { get; }
        public string Name { get; set; }
    }
}