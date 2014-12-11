using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using CardIndex.API.Models;
using CardIndex.Entities;
using CardIndex.Helpers;
using CardIndex.Models;
using CardIndex.Services.Interface;
using Newtonsoft.Json;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IHttpActionResult Get(int offset = 0, int limit = -1, [FromUri]SortingOptions sortingOptions = null)
        {
            sortingOptions = sortingOptions ?? new SortingOptions { SortColumn = -1, SortDirection = false };
            var count = _bookService.GetCount();
            var itemsPerPage = limit == -1 ? ConfigHelper.ItemPerPage : limit;
            var dbBooks = _bookService.GetSortedBooks(sortingOptions.SortDirection, sortingOptions.SortColumn).Skip(offset < 0 ? 0 : offset).Take(itemsPerPage).ToList();
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
