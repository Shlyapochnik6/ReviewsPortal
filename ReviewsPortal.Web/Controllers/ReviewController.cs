using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Delete;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Update;
using ReviewsPortal.Application.CommandsQueries.Review.Queries;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAllByUserId;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetDto;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetMostRated;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetUpdated;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.SortSelection;
using ReviewsPortal.Web.Models;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/reviews")]
public class ReviewController : BaseController
{
    public ReviewController(IMapper mapper, IMediator mediator) 
        : base(mapper, mediator) { }
    
    [HttpGet]
    public async Task<ActionResult<GetReviewDto>> Get(Guid reviewId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetReviewDtoQuery(reviewId, userId);
        var reviewDto = await _mediator.Send(query);
        return Ok(reviewDto);
    }

    [HttpGet("get-by-user")]
    public async Task<ActionResult<IEnumerable<GetAllUserReviewsDto>>> GetByCurrentUser()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetAllReviewsByUserIdQuery(userId);
        var userReviews = await _mediator.Send(query);
        return Ok(userReviews);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetAllReviewsDto>>> GetByRating(string? sorting, string? tag)
    {
        var query = new SortSelectionQuery(sorting, tag);
        var reviews = await _mediator.Send(query);
        return Ok(reviews);
    }

    [HttpGet("get-updated/{reviewId:guid}")]
    public async Task<ActionResult<GetUpdatedReviewDto>> GetUpdatedReview(Guid reviewId)
    {
        var query = new GetUpdatedReviewQuery(reviewId);
        var reviewDto = await _mediator.Send(query);
        return Ok(reviewDto);
    }

    [HttpPost, DisableRequestSizeLimit]
    public async Task<IActionResult> Create([FromForm] CreateReviewDto dto)
    {
        var command = _mapper.Map<CreateReviewCommand>(dto);
        command.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var reviewId = await _mediator.Send(command);
        return Created("api/reviews", reviewId);
    }
    
    [HttpPut, DisableRequestSizeLimit]
    public async Task<ActionResult> Update([FromForm] UpdateReviewDto dto)
    {
        var command = _mapper.Map<UpdateReviewCommand>(dto);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{reviewId:guid}")]
    public async Task<ActionResult> Delete(Guid reviewId)
    {
        var command = new DeleteReviewCommand(reviewId);
        await _mediator.Send(command);
        return Ok();
    }

}