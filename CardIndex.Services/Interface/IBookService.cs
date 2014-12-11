﻿using System.Collections.Generic;
using CardIndex.Entities;

namespace CardIndex.Services.Interface
{
    public interface IBookService
    {
        IEnumerable<DbBook> GetBooks();
        DbBook GetBookById(long id);
        long GetCount();
        IEnumerable<DbBook> GetSortedBooks(bool sortDirection, int sortColumn);
        void CreateBook(DbBook book);
        void UpdateBook(DbBook book);
        void DeleteBook(long id);
        void SaveBook();
    }
}
