using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetAll;

public class TagDto : IMapWith<Domain.Tag>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Tag, TagDto>()
            .ForMember(t => t.Name,
                c => c.MapFrom(t => t.TagName));
    }
}