using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using AutoMapper;
using CardIndex.Entities;
using CardIndex.Helpers;
using CardIndex.Models;
using CardIndex.Services.Interface;
using Newtonsoft.Json;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ODataController
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly long _pageSize;

        public BookController(IBookService bookService, IGenreService genreService, IAuthorService authorService)
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
            _pageSize = ConfigHelper.ItemPerPage;
        }

      //  [EnableQuery(PageSize = 2)]
        public IHttpActionResult Get(int offset = 0, int limit = -1)
        {
            var count = _bookService.GetCount();
            var itemsPerPage = limit == -1 ? ConfigHelper.ItemPerPage : limit;
            var dbBooks = _bookService.GetBooks().Skip(offset < 0 ? 0 : offset).Take(itemsPerPage).ToList();
            var books = dbBooks.Select(Mapper.Map<Book>).ToList();
            return Json(new { books, count, itemsPerPage }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            var dbBook = _bookService.GetBookById(id);
            if (dbBook == null) return BadRequest("Book not found!");
            var book = Mapper.Map<Book>(dbBook);
            return Json(book, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Book book)
        {
            var dbBook = Mapper.Map<DbBook>(book);
            _bookService.UpdateBook(dbBook);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Book book)
        {
            var dbBook = Mapper.Map<DbBook>(book);
            _bookService.CreateBook(dbBook);
            return Json(new { dbBook.Id });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
    }
}
