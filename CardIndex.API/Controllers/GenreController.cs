using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Query;
using CardIndex.Entities;
using CardIndex.Services.Interface;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenreController : ODataController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Get()
        {
            var genres = _genreService.GetGenres().AsQueryable();
            return Ok(genres);
        }

        public IHttpActionResult Put([FromODataUri]int key, [FromBody]DbGenre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _genreService.UpdateGenre(genre);
            return Updated(genre);
        }

        public IHttpActionResult Post([FromBody]DbGenre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _genreService.CreateGenre(genre);
            return Created(genre);
        }

        public IHttpActionResult Delete([FromODataUri]int key)
        {
            _genreService.DeleteGenre(key);
            return Ok();
        }
    }
}
