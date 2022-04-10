using Microsoft.AspNetCore.Mvc;
using PolymorphicModelBinder.Samples.Api.Models;

namespace PolymorphicModelBinder.Samples.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpPost]
    public IPet Get([FromBody]IPet pet)
    {
        return pet;
    }
}