using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ServiceLayer.Specifications
{
    public class BorrowedBooksFilter : FilterSpecification<Book>
    {
        protected override Expression<Func<Book, bool>> SpecificationExpression => x => x.BorrowerId != null;
    }
}
