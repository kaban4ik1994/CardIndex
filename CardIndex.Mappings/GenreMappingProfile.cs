using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;

namespace CardIndex.Mappings
{
    public class GenreMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            MapGenreToDbGenre();
            MapDbGenreToGenre();
        }

        private void MapGenreToDbGenre()
        {
            CreateMap<DbGenre, Genre>();
        }

        private void MapDbGenreToGenre()
        {
            CreateMap<Genre, DbGenre>();
        }

    }
}
