using Backend.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/provider")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;
    
}