using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.GetAll;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetAllUsersQueryHandler(IReviewsPortalDbContext dbContext,
        UserManager<Domain.User> userManager, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersDtos = new List<GetAllUsersDto>();
        var users = await _dbContext.Users.ToListAsync(cancellationToken);
        foreach (var user in users)
        {
            var userDto = _mapper.Map<GetAllUsersDto>(user);
            userDto.Role = await GetUserRole(user);
            usersDtos.Add(userDto);
        }
        return usersDtos;
    }

    private async Task<string> GetUserRole(Domain.User user)
    {
        var role = await _userManager.GetRolesAsync(user);
        return role.FirstOrDefault() ?? Roles.User;
    }
}