using PolymorphicModelBinder;
using PolymorphicModelBinder.Samples.Mvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPolymorphicModelBinder(options =>
{
    options.Add<Pet>(polymorphicBuilder =>
    {
        polymorphicBuilder.AddFromTypeInValue<Dog>();
        polymorphicBuilder.AddFromTypeInValue<Cat>();
    });

    options.Add<IDevice>(polymorphicBuilder =>
    {
        polymorphicBuilder.AddFromDiscriminator<Laptop>();
        polymorphicBuilder.AddFromDiscriminator<SmartPhone>();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();