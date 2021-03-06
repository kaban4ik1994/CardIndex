﻿using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Query;
using CardIndex.Entities;
using CardIndex.Services.Interface;

namespace CardIndex.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ODataController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 5)]
        public IHttpActionResult Get()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        public IHttpActionResult Put([FromODataUri]int key, [FromBody]DbBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _bookService.UpdateBook(book);
            return Updated(book);
        }

        public IHttpActionResult Post([FromBody]DbBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _bookService.CreateBook(book);
            return Created(book);
        }

        public IHttpActionResult Delete([FromODataUri]int key)
        {
            _bookService.DeleteBook(key);
            return Ok();
        }
    }
}
