using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;
using ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;
using ReviewsPortal.Application.CommandsQueries.User.Queries.ExternalLoginCallback;
using ReviewsPortal.Application.CommandsQueries.User.Queries.GetAll;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Login;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Domain;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly SignInManager<Domain.User> _signInManager;

    public UserController(IMapper mapper, IMediator mediator,
        SignInManager<User> signInManager) : base(mapper, mediator)
    {
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> SigningOn([FromBody] UserRegistrationDto dto)
    {
        var command = Mapper.Map<UserRegistrationCommand>(dto);
        await Mediator.Send(command);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var query = Mapper.Map<UserLoginQuery>(dto);
        await Mediator.Send(query);
        return Ok();
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("external-login")]
    public async Task<IActionResult> ExternalLogin(string provider)
    {
        var query = new ExternalLoginQuery()
        {
            Provider = provider,
            RedirectUrl = "/external-login-callback"
        };
        var properties = await Mediator.Send(query);
        return Challenge(properties, provider);
    }

    [AllowAnonymous]
    [HttpGet("external-login-callback")]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var query = new ExternalLoginCallbackQuery();
        await Mediator.Send(query);
        return Ok();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetAllUsersDto>>> GetAll()
    {
        var query = new GetAllUsersQuery();
        var users = await Mediator.Send(query);
        return users.ToList();
    }

    [AllowAnonymous]
    [HttpGet("check-authentication")]
    public ActionResult<bool> CheckAuthentication()
    {
        return Ok(User.Identity!.IsAuthenticated);
    }

    [AllowAnonymous]
    [HttpGet("get-role")]
    public ActionResult<UserRoleDto> GetRole()
    {
        var role = new UserRoleDto(Role);
        return Ok(role);
    }
}