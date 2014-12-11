using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using CardIndex.Entities;
using CardIndex.Helpers;
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

        public IHttpActionResult Get(int offset = 0, int limit = -1)
        {
            var count = _genreService.GetCount();
            var itemsPerPage = limit == -1 ? ConfigHelper.ItemPerPage : limit;
            var dbGenres = _genreService.GetGenres().Skip(offset < 0 ? 0 : offset).Take(itemsPerPage).ToList();
            var genres = dbGenres.Select(Mapper.Map<Genre>).ToList();
            return Json(new { genres, count, itemsPerPage }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Genre genre)
        {
            var dbGenre = Mapper.Map<DbGenre>(genre);
            _genreService.CreateGenre(dbGenre);
            return Json(new { GenreId = dbGenre.Id });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _genreService.DeleteGenre(id);
            return Ok();
        }
    }
}
