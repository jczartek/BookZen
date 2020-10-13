using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer
{
    public abstract class FilterSpecification<TEntity> where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> Specification { get; }

        public static implicit operator Expression<Func<TEntity, bool>>(FilterSpecification<TEntity> spec) => spec.Specification;
    }
}
