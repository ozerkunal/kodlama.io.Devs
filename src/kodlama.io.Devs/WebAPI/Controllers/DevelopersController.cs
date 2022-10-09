using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Application.Features.Developers.Commands.RegisterDeveloper;
using Application.Features.Developers.Dtos;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto developerForRegisterDto)
        {
            RegisterDeveloperCommand registerDeveloperCommand = new()
            {
                UserForRegisterDto = developerForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisteredDeveloperDto result = await Mediator.Send(registerDeveloperCommand);
            setRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void setRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }

        
    }
}
