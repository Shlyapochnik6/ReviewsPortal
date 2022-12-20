using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Category.Queries.GetList;

public class CategoryDto : IMapWith<Domain.Category>
{
    public string CategoryName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Category, CategoryDto>()
            .ForMember(c => c.CategoryName, opt =>
                opt.MapFrom(c => c.CategoryName));
    }
}