using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetAll;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Route("api/tags")]
public class TagController : BaseController
{
    public TagController(IMapper mapper, IMediator mediator) 
        : base(mapper, mediator) { }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
    {
        var query = new GetAllTagsQuery();
        var tags = await _mediator.Send(query);
        return Ok(tags);
    }
}