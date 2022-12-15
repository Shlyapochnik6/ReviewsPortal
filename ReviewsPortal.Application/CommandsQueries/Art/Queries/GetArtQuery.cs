using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Art.Queries;

public class GetArtQuery : IRequest<Domain.Art>
{
    public Guid? ArtId { get; set; }
}