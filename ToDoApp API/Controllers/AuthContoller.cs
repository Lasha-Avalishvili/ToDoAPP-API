﻿using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.Auth;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]    /// shegvedzlo ase yofiliyo [Route("Auth")]
public class AuthController : ControllerBase
{
    [HttpGet]
    [HttpPost]
    [Route("Ping")]
    public string Ping()
    {
        
        return "system is working properlyy";
    }


    [HttpPost("login/{email}")]
     
    public string Login(string email)
    {
        // TODO: check user credentials

        var jwtsettings = new JwtSettings();
        jwtsettings.Issuer = "";
        jwtsettings.Audience = "";
        jwtsettings.SecrectKey = "mySecretKey234mkefweifmskemfkej3242r2";  // example secret key(incorrect!!)
        var tokenGenerator = new TokenGenerator(jwtsettings);
        return tokenGenerator.Generate(email);

    }
}

