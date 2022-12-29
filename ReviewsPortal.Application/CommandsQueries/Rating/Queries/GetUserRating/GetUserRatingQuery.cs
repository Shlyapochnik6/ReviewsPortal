using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Queries.GetUserRating;

public class GetUserRatingQuery : IRequest<Domain.Rating>
{
    public Guid ArtId { get; set; }

    public Guid? UserId { get; set; }
}