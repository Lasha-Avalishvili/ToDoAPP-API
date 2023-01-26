using Microsoft.AspNetCore.Mvc;

namespace ToDoApp_API.Controllers;

[ApiController]
[Route("[controller]")]
public class HowOldAreYou : ControllerBase
    {
    [HttpGet("trialcontroller")]

    public string Ping(int number)
    {
        int hours = number * 8760;

        return $"თუკი {number} წლის ხარ, ეს იმას ნიშნავს რომ ამ სამყაროში {hours} საათი გაქვს გატარებული";
    }


}

