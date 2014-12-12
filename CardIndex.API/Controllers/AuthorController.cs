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
    public class AuthorController : ODataController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Get()
        {
            var authors = _authorService.GetAuthors().AsQueryable();
            return Ok(authors);
        }

        public IHttpActionResult Put([FromODataUri]int key, [FromBody]DbAuthor author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _authorService.UpdateAuthor(author);
            return Updated(author);
        }

        public IHttpActionResult Post([FromBody]DbAuthor author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _authorService.CreateAuthor(author);
            return Created(author);
        }

        public IHttpActionResult Delete([FromODataUri]int key)
        {
            _authorService.DeleteAuthor(key);
            return Ok();
        }
    }
}
