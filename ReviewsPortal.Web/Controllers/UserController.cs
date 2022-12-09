using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;
using ReviewsPortal.Domain;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserController(IMapper mapper, IMediator mediator, 
        SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _mediator = mediator;
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
}