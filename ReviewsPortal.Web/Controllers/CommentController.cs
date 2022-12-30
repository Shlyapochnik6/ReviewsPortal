﻿using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Comment.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Comment.Queries.GetAll;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/comments")]
public class CommentController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CommentController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("{reviewId:guid}")]
    public async Task<ActionResult<IEnumerable<GetAllCommentsDto>>> GetAll(Guid reviewId)
    {
        var query = new GetAllCommentsQuery() { ReviewId = reviewId };
        var comments = await _mediator.Send(query);
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCommentDto dto)
    {
        var query = _mapper.Map<CreateCommentCommand>(dto);
        query.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var commentId = await _mediator.Send(query);
        return Ok(commentId);
    }
}