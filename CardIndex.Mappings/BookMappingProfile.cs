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
            CreateMap<DbBook, Book>()
                .ForMember(book => book.Authors, expression => expression.MapFrom(book => book.Authors))
                .ForMember(book => book.Genres, expression => expression.MapFrom(book => book.Genres));
        }

        private void MapBookToDbBook()
        {
            CreateMap<Book, DbBook>()
                .ForMember(book => book.Authors, expression => expression.MapFrom(book => book.Authors))
                .ForMember(book => book.Genres, expression => expression.MapFrom(book => book.Genres));
        }
    }
}
