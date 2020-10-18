using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer
{
    public abstract class FilterSpecification<TEntity> where TEntity : class
    {
        private class ConstructedSpecification<TType> : FilterSpecification<TType> where TType : class
        {
            private readonly Expression<Func<TType, bool>> specificationExpression;
            public ConstructedSpecification(Expression<Func<TType, bool>> specificationExpression)
            {
                this.specificationExpression = specificationExpression;
            }

            protected override Expression<Func<TType, bool>> SpecificationExpression 
                => specificationExpression;
        }

        protected abstract Expression<Func<TEntity, bool>> SpecificationExpression { get; }

        public static implicit operator Expression<Func<TEntity, bool>>(FilterSpecification<TEntity> spec) 
            => spec.SpecificationExpression;

        public static FilterSpecification<TEntity> operator &(FilterSpecification<TEntity> left, FilterSpecification<TEntity> right)
        {
            return CombineSpecification(left, right, Expression.AndAlso);
        }

        public static FilterSpecification<TEntity> operator |(FilterSpecification<TEntity> left, FilterSpecification<TEntity> right)
        {
            return CombineSpecification(left, right, Expression.OrElse);
        }

        public static FilterSpecification<TEntity> operator !(FilterSpecification<TEntity> original)
        {
            var arg = Expression.Parameter(typeof(TEntity));
            var expr = original.SpecificationExpression;
            var notExpr = Expression.Not(new ReplaceParameterVisitor { { expr.Parameters.Single(), arg } }.Visit(expr.Body));

            return new ConstructedSpecification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(notExpr, arg));
        }

        private static FilterSpecification<TEntity> CombineSpecification(FilterSpecification<TEntity> left, FilterSpecification<TEntity> right, Func<Expression, Expression, BinaryExpression> combiner)
        {
            var expr1 = left.SpecificationExpression;
            var expr2 = right.SpecificationExpression;
            var arg = Expression.Parameter(typeof(TEntity));
            var combined = combiner.Invoke(
                new ReplaceParameterVisitor { { expr1.Parameters.Single(), arg } }.Visit(expr1.Body),
                new ReplaceParameterVisitor { { expr2.Parameters.Single(), arg } }.Visit(expr2.Body));

            return new ConstructedSpecification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(combined, arg));
        }

        private class ReplaceParameterVisitor : ExpressionVisitor, IEnumerable<KeyValuePair<ParameterExpression, ParameterExpression>>
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> parameterMappings = new Dictionary<ParameterExpression, ParameterExpression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (parameterMappings.TryGetValue(node, out var newValue))
                    return newValue;

                return node;
            }

            public void Add(ParameterExpression parameterToReplace, ParameterExpression replaceWith)
                => parameterMappings.Add(parameterToReplace, replaceWith);

            public IEnumerator<KeyValuePair<ParameterExpression, ParameterExpression>> GetEnumerator()
                => parameterMappings.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
