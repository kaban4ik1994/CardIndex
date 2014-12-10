using System.Collections;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using AutoMapper;
using CardIndex.Data;
using CardIndex.Data.DBInteractions.Concrete;
using CardIndex.Data.Repositories.Interface;
using CardIndex.Entities;
using CardIndex.Models;
using CardIndex.Services.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenreController : ApiController
    {
        private readonly IGenreService _genreService;
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreService genreService, IGenreRepository genreRepository)
        {
            _genreService = genreService;
            _genreRepository = genreRepository;
        }

        [Queryable]
        public IQueryable<Genre> Get()
        {
            var dbGenres = _genreService.GetGenres().ToList();
            var genres = dbGenres.Select(Mapper.Map<Genre>).AsQueryable();
            return genres;
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
        public IHttpActionResult Put([FromBody]Genre genre)
        {
            var dbGenre = Mapper.Map<DbGenre>(genre);
            _genreService.UpdateGenre(dbGenre);
            return Json(Mapper.Map<Genre>(dbGenre), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Genre genre)
        {
            var dbGenre = Mapper.Map<DbGenre>(genre);
            _genreService.CreateGenre(dbGenre);
            return Json(Mapper.Map<Genre>(dbGenre), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _genreService.DeleteGenre(id);
            return Ok();
        }
    }
}
