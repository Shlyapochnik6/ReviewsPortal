using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Common.Exceptions;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserRegistrationCommandHandler(IMapper mapper, IReviewsPortalDbContext dbContext, 
        UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await GetExistingUser(request, cancellationToken);
        if (existingUser) throw new ExistingUserException();
        await RegisterUser(request);
        return Unit.Value;
    }

    private async Task<bool> GetExistingUser(UserRegistrationCommand request, 
        CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.AnyAsync(u =>
            u.Email == request.Email || u.UserName == request.Name, cancellationToken);
        return existingUser;
    }

    private async Task RegisterUser(UserRegistrationCommand request)
    {
        var user = _mapper.Map<Domain.User>(request);
        await _userManager.CreateAsync(user, request.Password);
        await _signInManager.SignInAsync(user, request.Remember);
    }
}