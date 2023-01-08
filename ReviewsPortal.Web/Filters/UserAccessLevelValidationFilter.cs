using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Application.Common.Exceptions;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Web.Filters;

public class UserAccessLevelValidationFilter : Attribute, IAsyncActionFilter
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMediator _mediator;

    public UserAccessLevelValidationFilter(SignInManager<User> signInManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = GetUserId(context);
        if (userId is null)
        {
            await next();
            return;
        }
        try
        {
            var user = await GetUser(userId!.Value);
            await CheckUserAccessLevel(user);
        }
        catch (UnfoundException e)
        {
            await next();
        }
        await next();
    }
    
    private async Task<User> GetUser(Guid userId)
    {
        var query = new GetUserQuery(userId);
        return await _mediator.Send(query);
    }

    private async Task CheckUserAccessLevel(User user)
    {
        if (user.AccessLevel == UserAccessStatuses.Blocked)
        {
            await _signInManager.SignOutAsync();
            throw new AccessDeniedException($"The user with id {user.Id} has been blocked!");
        }
    }

    private Guid? GetUserId(ActionContext context)
    {
        var claim = context.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null)
            return null;
        return Guid.Parse(claim);
    }
}