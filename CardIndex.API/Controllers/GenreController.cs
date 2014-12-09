using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using CardIndex.Entities;
using CardIndex.Models;
using CardIndex.Services.Interface;
using Newtonsoft.Json;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenreController : ApiController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var dbGenres = _genreService.GetGenres().ToList();
            var genres = dbGenres.Select(Mapper.Map<Genre>).ToList();
            return Json(genres, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            var dbGenre = _genreService.GetGenreById(id);
            if (dbGenre == null) return BadRequest("Genre not found!");
            var genre = Mapper.Map<Genre>(dbGenre);
            return Json(genre, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPut]
        public IHttpActionResult Put(Genre genre)
        {
            var dbGenre = Mapper.Map<DbGenre>(genre);
            _genreService.UpdateGenre(dbGenre);
            return Json(Mapper.Map<Genre>(dbGenre), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        public IHttpActionResult Delete(long id)
        {
            _genreService.DeleteGenre(id);
            return Ok();
        }
    }
}
