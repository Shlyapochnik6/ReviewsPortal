using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Review.Queries;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAllByUserId;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetMostRated;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.SortSelection;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/reviews")]
public class ReviewController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReviewController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("get-by-user")]
    public async Task<ActionResult<IEnumerable<GetAllUserReviewsDto>>> GetByCurrentUser()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetAllReviewsByUserIdQuery()
        {
            UserId = userId
        };
        var userReviews = await _mediator.Send(query);
        return Ok(userReviews);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetAllReviewsDto>>> GetByRating(string? sorting, string? tag)
    {
        var query = new SortSelectionQuery()
        {
            Sorting = sorting,
            Tag = tag
        };
        var reviews = await _mediator.Send(query);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateReviewDto dto)
    {
        var command = _mapper.Map<CreateReviewCommand>(dto);
        command.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var reviewId = await _mediator.Send(command);
        return Created("api/reviews", reviewId);
    }
    
}