using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Commands.Set;

public class SetRatingCommand : IRequest
{
    public int Value { get; set; }

    public Guid? UserId { get; set; }

    public Guid ReviewId { get; set; }
}