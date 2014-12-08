using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Mappings
{
    public class BookMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapBookToDbBook();
            MapDbBookToBook();
        }

        private void MapDbBookToBook()
        {
            CreateMap<DbBook, Book>();
        }

        private void MapBookToDbBook()
        {
            CreateMap<Book, DbBook>();
        }
    }
}
