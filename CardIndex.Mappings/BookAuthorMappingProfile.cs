using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Mappings
{
    public class BookAuthorMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapBookAuthorToDbBookDbAuthor();
            MapDbBookDbAuthorToBooAuthor();
        }

        private void MapDbBookDbAuthorToBooAuthor()
        {
            CreateMap<DbBookDbAuthor, BookAuthor>();
        }

        private void MapBookAuthorToDbBookDbAuthor()
        {
            CreateMap<BookAuthor, DbBookDbAuthor>();
        }
    }
}
