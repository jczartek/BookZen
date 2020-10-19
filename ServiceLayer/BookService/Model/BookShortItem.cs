using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.BookService.Model
{
    public class BookShortItem
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public string Authors { get; set; }
        public bool IsRead { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
