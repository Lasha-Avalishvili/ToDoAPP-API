using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.Auth;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]  
public class AuthController : ControllerBase 
{
    private TokenGenerator _tokenGenerator; 

    public AuthController(TokenGenerator tokenGenerator)
    {
        _tokenGenerator= tokenGenerator;
    }

    [HttpGet]
    [Route("Ping")]
    public string Ping()
    {
        return "system is working properlyy";
    }

           // TODO
           // Register
           // reseting password: RequestPasswordReser, ResetPassword


    [HttpPost("login/{email}")]
     
    public string Login(string email)
    {
        return _tokenGenerator.Generate(email);
    }
}

