using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommand : IRequest<Domain.Rating>
{
    public int Value { get; set; }

    public Guid? UserId { get; set; }

    public Guid ArtId { get; set; }
}