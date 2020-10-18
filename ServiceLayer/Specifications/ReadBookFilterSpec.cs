using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ServiceLayer.Specifications
{
    public class ReadBookFilterSpec : FilterSpecification<Book>
    {
        protected override Expression<Func<Book, bool>> SpecificationExpression => x => x.ReadDate != null;
    }
}
