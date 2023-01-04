using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Delete;

public class DeleteReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }

    public DeleteReviewCommand(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}