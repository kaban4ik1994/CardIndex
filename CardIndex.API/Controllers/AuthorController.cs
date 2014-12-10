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
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var dbAuthors = _authorService.GetAuthors().ToList();
            var authors = dbAuthors.Select(Mapper.Map<Author>).ToList();
            return Json(authors, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            var dbAuthor = _authorService.GetAuthorById(id);
            if (dbAuthor == null) return BadRequest("Author not found!");
            var author = Mapper.Map<Author>(dbAuthor);
            return Json(author, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Author author)
        {
            var dbAuthor = Mapper.Map<DbAuthor>(author);
            _authorService.UpdateAuthor(dbAuthor);
            return Json(Mapper.Map<Author>(dbAuthor), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Author author)
        {
            var dbAuthor = Mapper.Map<DbAuthor>(author);
            _authorService.CreateAuthor(dbAuthor);
            return Json(Mapper.Map<Author>(dbAuthor), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _authorService.DeleteAuthor(id);
            return Ok();
        }
    }
}
