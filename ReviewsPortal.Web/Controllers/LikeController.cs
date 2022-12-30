﻿using System.Security.Claims;
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
public class LikeController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LikeController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Set([FromBody] SetLikeDto dto)
    {
        var command = _mapper.Map<SetLikeCommand>(dto);
        command.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _mediator.Send(command);
        return Ok();
    }
}