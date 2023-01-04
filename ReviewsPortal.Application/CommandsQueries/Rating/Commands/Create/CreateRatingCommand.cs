using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommand : IRequest<Domain.Rating>
{
    public int Value { get; set; }

    public Guid? UserId { get; set; }

    public Guid ArtId { get; set; }

    public CreateRatingCommand(int value, Guid? userId,
        Guid artId)
    {
        Value = value;
        UserId = userId;
        ArtId = artId;
    }
}