using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoApp.API.Auth;
using ToDoApp_API.Db.Entities;
using ToDoApp_API.Models.Requests;
using ToDoApp_API.Repositories;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]  
public class AuthController : ControllerBase 
{
    private TokenGenerator _tokenGenerator; 
    private UserManager<UserEntity> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ISendEmailRequestRepository _sendEmailRequestRepository;

    public AuthController(UserManager<UserEntity> userManager, TokenGenerator tokenGenerator, IConfiguration configuration,
            ISendEmailRequestRepository sendEmailRequestRepository)
    {
        _tokenGenerator= tokenGenerator;
        _userManager= userManager;
        _configuration = configuration;
        _sendEmailRequestRepository = sendEmailRequestRepository;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
    {
       var entity = new UserEntity();
       entity.Email = request.Email;
       entity.UserName = request.Email;
       entity.EmailConfirmed = true;

       var result = await _userManager.CreateAsync(entity, request.Password);


        if (!result.Succeeded) 
        {
            var firstError = result.Errors.First();
            return BadRequest(firstError.Description);
        }

        return Ok();   
    }


    [HttpPost("login")]
     
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {

        var user =await _userManager.FindByEmailAsync(request.Email);

        if(user == null)
        {
            return NotFound("User not found");
        }

        var isCorrectPassword =await _userManager.CheckPasswordAsync(user, request.Password);

        if(!isCorrectPassword)
        {
            return BadRequest("Invalid email or password");
        }

        return Ok(_tokenGenerator.Generate(user.Id.ToString()));
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var sendEmailRequestEntity = new SendEmailRequestEntity();
        sendEmailRequestEntity.ToAddress = request.Email;
        sendEmailRequestEntity.Status = SendEmailRequestStatus.New;
        sendEmailRequestEntity.CreatedAt = DateTime.Now;

        var url = _configuration["PasswordResetUrl"]!
            .Replace("{userId}", user.Id.ToString())
            .Replace("{token}", token);
        var resetUrl = $"<a href=\"{url}\">Reset password</a>";
        sendEmailRequestEntity.Body = $"Hello, your password reset link is: {resetUrl}";

        _sendEmailRequestRepository.Insert(sendEmailRequestEntity);
        await _sendEmailRequestRepository.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return NotFound("User not found");
        }
        var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!resetResult.Succeeded)
        {
            var firstError = resetResult.Errors.First();
            return StatusCode(500, firstError.Description);
        }

        return Ok();
    }


}

