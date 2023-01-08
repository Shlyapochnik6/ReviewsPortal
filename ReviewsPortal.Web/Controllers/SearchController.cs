using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Review.Queries;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Search;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : BaseController
{
    public SearchController(IMapper mapper, IMediator mediator) 
        : base(mapper, mediator) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllReviewsDto>>> Get(string search)
    {
        var query = new SearchReviewsQuery(search);
        var reviews = await Mediator.Send(query);
        return reviews.ToList();
    }
}