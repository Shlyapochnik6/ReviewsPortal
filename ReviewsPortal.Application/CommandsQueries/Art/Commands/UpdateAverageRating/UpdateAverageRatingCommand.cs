using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Art.Commands.UpdateAverageRating;

public class UpdateAverageRatingCommand : IRequest
{
    public Guid ArtId { get; set; }
}