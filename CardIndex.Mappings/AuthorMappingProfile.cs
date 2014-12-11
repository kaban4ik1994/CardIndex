using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Mappings
{
    public class AuthorMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapAuthorToDbAuthor();
            MapDbAuthorToAuthor();
        }

        private void MapDbAuthorToAuthor()
        {
            CreateMap<DbAuthor, Author>()
                .ForMember(author => author.Books, expression => expression.MapFrom(author => author.Books))
                .ForMember(author => author.AuthorId, expression => expression.MapFrom(author => author.Id));
        }

        private void MapAuthorToDbAuthor()
        {
            CreateMap<Author, DbAuthor>()
                .ForMember(author => author.Books, expression => expression.MapFrom(author => author.Books))
                .ForMember(author => author.Id, expression => expression.MapFrom(author => author.AuthorId));
        }
    }
}
