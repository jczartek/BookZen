using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ServiceLayer.PersistenceService
{
    class PersistenceService : IPersistenceService
    {
        private readonly IBookService _bookService;

        public PersistenceService(IBookService bookService)
        {
            _bookService = bookService;
        }
        public void Export(string filename)
        {
            throw new NotImplementedException();
        }

        public void Import(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
