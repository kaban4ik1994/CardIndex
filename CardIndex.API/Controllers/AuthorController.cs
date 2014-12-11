﻿using System.Linq;
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
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IHttpActionResult Get(int offset = 0, int limit = -1)
        {
            var count = _authorService.GetCount();
            var itemsPerPage = limit == -1 ? ConfigHelper.ItemPerPage : limit;
            var dbAuthors = _authorService.GetAuthors().Skip(offset < 0 ? 0 : offset).Take(itemsPerPage).ToList();
            var authors = dbAuthors.Select(Mapper.Map<Author>).ToList();
            return Json(new { authors, count, itemsPerPage }, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Author author)
        {
            var dbAuthor = Mapper.Map<DbAuthor>(author);
            _authorService.CreateAuthor(dbAuthor);
            return Json(new { AuthorId = dbAuthor.Id });
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            _authorService.DeleteAuthor(id);
            return Ok();
        }
    }
}
