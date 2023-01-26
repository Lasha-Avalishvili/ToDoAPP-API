using Microsoft.AspNetCore.Mvc;
using ToDoApp_API.Auth;

namespace ToDoApp_API.Controllers;

[ApiController]
[Route("[controller]")]    /// shegvedzlo ase yofiliyo [Route("Auth")]
public class AuthController : ControllerBase
{
    [HttpGet]
    [Route("Ping")]
    public string Ping()
    {
        return "system is working properlyy";
    }


    [HttpPost]
    [Route("Authenticate")]
    public string Login(string email)
    {
        // TODO: check user credentials

        var jwtsettings = new JwtSettings();
        jwtsettings.Issuer = "";
        jwtsettings.Audience = "";
        jwtsettings.SecrectKey = "";
        var tokenGenerator = new TokenGenerator(jwtsettings);
        return tokenGenerator.Generate(email);

    }
}

