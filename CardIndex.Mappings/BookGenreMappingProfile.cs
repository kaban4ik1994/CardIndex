using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Mappings
{
    public class BookGenreMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapBookGenreToDbBookDbGenre();
            MapDbBookDbGenreToBookGenre();
        }

        private void MapDbBookDbGenreToBookGenre()
        {
            CreateMap<DbBookDbGenre, BookGenre>();
        }

        private void MapBookGenreToDbBookDbGenre()
        {
            CreateMap<BookGenre, DbBookDbGenre>();
        }
    }
}
