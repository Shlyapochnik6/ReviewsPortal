using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Like.Commands.Set;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/likes")]
public class LikeController : BaseController
{
    public LikeController(IMapper mapper, IMediator mediator) 
        : base(mapper, mediator) { }

    [HttpPost]
    public async Task<ActionResult> Set([FromBody] SetLikeDto dto)
    {
        var command = Mapper.Map<SetLikeCommand>(dto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return Ok();
    }
}