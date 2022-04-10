using PolymorphicModelBinder;
using PolymorphicModelBinder.Samples.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPolymorphicModelBinder(options =>
{
    options.Add<IPet>(polymorphicBuilder =>
    {
        polymorphicBuilder.AddFromDiscriminator<Dog>();
        polymorphicBuilder.AddFromDiscriminator<Cat>();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();