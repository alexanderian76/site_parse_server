using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using site_parse_server.Models;
using site_parse_server.Models.Authorization;
using site_parse_server.Services.Interfaces;

namespace site_parse_server.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IRequestService _requestService;

    public HomeController(ILogger<HomeController> logger, IUserService userService, IRequestService requestService)
    {
        _logger = logger;
        _userService = userService;
        _requestService = requestService;
    }

    public IActionResult Index()
    {
        _logger.Log(LogLevel.Information, "Index page enter");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    [Route("/example")]
    public async Task<IActionResult> GetExample(string fileName)
    {
        var ms = new MemoryStream();
        var file = System.IO.File.OpenRead(fileName);
        file.CopyTo(ms);
        file.Close();

        ms.Position = 0;
        return Ok(ms);
    }


    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> SendRequest([FromBody] Request request)
    {
        await _requestService.Create(request);
        return Ok("Request created");
    }

    [Authorize]
    [HttpPost]
    [Route("getTasks")]
    public async Task<IActionResult> GetTasks([FromBody] User user)
    {
        var response = await _userService.GetUserTasks(user.Login);
         
        if (response.StatusCode == global::StatusCode.OK)
            return Ok(response);
        else return BadRequest(response);
    }


    [Authorize]
    [HttpPost]
    [Route("addTask")]
    public async Task<IActionResult> AddTask([FromBody] User user)
    {
        var response = await _userService.AddTask(user);

        if (response.StatusCode == global::StatusCode.OK)
            return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
      /*  var request = HttpContext.Request;
        var ms = new MemoryStream();
        var bytes = new Byte[] { };
        request.BodyReader.AsStream().CopyTo(ms);
        bytes = ms.ToArray();
        
        var str = Encoding.Default.GetString(bytes);*/
        //var user = JsonSerializer.Deserialize<User>(str);
        var response = await _userService.Create(new User() { Login = user.Login, Password = user.Password, Tasks = new List<ParseTask>() { new ParseTask() { Description = "Test task" } } });
        //ms.Close();
        if (response.StatusCode == global::StatusCode.OK)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };
            // if (data.Data.Where(u => u.Login == userName).Count() > 0)
            {
                // return new BadRequestObjectResult("User exists");
            }
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            //await HttpContext.Response.BodyWriter.WriteAsync(Encoding.Default.GetBytes(new JwtSecurityTokenHandler().WriteToken(jwt)));
            
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
            //return Ok(response);
        else return BadRequest(response);
        
    }
    

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
       /* var request = HttpContext.Request;
        var ms = new MemoryStream();
        var bytes = new Byte[] { };
        request.BodyReader.AsStream().CopyTo(ms);
        bytes = ms.ToArray();

        var str = Encoding.Default.GetString(bytes);
        var user = JsonSerializer.Deserialize<User>(str);*/
        var isValidUser = await _userService.CheckUser(user.Login, user.Password);
       // ms.Close();
        if (isValidUser)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };
            // if (data.Data.Where(u => u.Login == userName).Count() > 0)
            {
                // return new BadRequestObjectResult("User exists");
            }
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            //await HttpContext.Response.BodyWriter.WriteAsync(Encoding.Default.GetBytes(new JwtSecurityTokenHandler().WriteToken(jwt)));
            
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
        return BadRequest("Wrong login or password");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

