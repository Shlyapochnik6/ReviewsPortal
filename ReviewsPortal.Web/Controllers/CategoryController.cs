﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Application.CommandsQueries.Category.Queries.GetList;

namespace ReviewsPortal.Web.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var query = new GetCategoriesQuery();
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }
}