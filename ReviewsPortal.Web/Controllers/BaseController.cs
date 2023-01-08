using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMapper Mapper;
    protected readonly IMediator Mediator;

    public BaseController(IMapper mapper, IMediator mediator)
    {
        Mapper = mapper;
        Mediator = mediator;
    }

    protected Guid UserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : Guid.Empty;
    protected string Role => User.FindFirstValue(ClaimTypes.Role)!;
}