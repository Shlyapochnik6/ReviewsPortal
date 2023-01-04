using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;
using ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;
using ReviewsPortal.Application.CommandsQueries.User.Queries.ExternalLoginCallback;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Login;
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
        var command = _mapper.Map<UserRegistrationCommand>(dto);
        await _mediator.Send(command);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var query = _mapper.Map<UserLoginQuery>(dto);
        await _mediator.Send(query);
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
        var properties = await _mediator.Send(query);
        return Challenge(properties, provider);
    }

    [AllowAnonymous]
    [HttpGet("external-login-callback")]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var query = new ExternalLoginCallbackQuery();
        await _mediator.Send(query);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("check-authentication")]
    public ActionResult<bool> CheckAuthentication()
    {
        return Ok(User.Identity!.IsAuthenticated);
    }
}