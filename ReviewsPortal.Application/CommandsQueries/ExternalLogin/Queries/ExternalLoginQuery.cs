using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;

public class ExternalLoginQuery : IRequest<AuthenticationProperties>
{
    public string? Provider { get; set; }
    
    public string? RedirectUrl { get; set; }
}