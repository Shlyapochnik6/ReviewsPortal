using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Rating.Commands.Set;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/ratings")]
public class RatingController : BaseController
{
    public RatingController(IMapper mapper, IMediator mediator) 
        : base(mapper, mediator) { }

    [HttpPost]
    public async Task<ActionResult> Set([FromBody] SetRatingDto dto)
    {
        var command = _mapper.Map<SetRatingCommand>(dto);
        command.UserId = UserId;
        await _mediator.Send(command);
        return Ok();
    }
}