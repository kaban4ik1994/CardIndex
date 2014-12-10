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
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var dbBooks = _bookService.GetBooks().ToList();
            var books = dbBooks.Select(Mapper.Map<Book>).ToList();
            return Json(books, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
        public IHttpActionResult Put(Book book)
        {
            var dbBook = Mapper.Map<DbBook>(book);
            _bookService.UpdateBook(dbBook);
            return Json(Mapper.Map<Genre>(dbBook), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        public IHttpActionResult Post(Book book)
        {
            var dbBook = Mapper.Map<DbBook>(book);
            _bookService.CreateBook(dbBook);
            return Json(Mapper.Map<Genre>(dbBook), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
    }
}
