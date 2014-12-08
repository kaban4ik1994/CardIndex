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
            CreateMap<DbAuthor, Author>();
        }

        private void MapAuthorToDbAuthor()
        {
            CreateMap<Author, DbAuthor>();
        }
    }
}
