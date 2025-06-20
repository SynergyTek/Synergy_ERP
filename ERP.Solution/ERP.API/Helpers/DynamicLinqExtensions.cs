using ERP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Helpers
{
    // Dynamic LINQ helpers (for demo; use a library like System.Linq.Dynamic.Core in production)
    public static class DynamicLinqExtensions
    {
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> source, string field, string op, string value)
        {
            // Only string and numeric for demo; extend as needed
            var param = System.Linq.Expressions.Expression.Parameter(typeof(T), "e");
            var prop = System.Linq.Expressions.Expression.Property(param, field);
            var constant = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(value, prop.Type));
            System.Linq.Expressions.Expression body = op switch
            {
                "==" => System.Linq.Expressions.Expression.Equal(prop, constant),
                "!=" => System.Linq.Expressions.Expression.NotEqual(prop, constant),
                ">" => System.Linq.Expressions.Expression.GreaterThan(prop, constant),
                ">=" => System.Linq.Expressions.Expression.GreaterThanOrEqual(prop, constant),
                "<" => System.Linq.Expressions.Expression.LessThan(prop, constant),
                "<=" => System.Linq.Expressions.Expression.LessThanOrEqual(prop, constant),
                _ => throw new NotSupportedException($"Operator {op} not supported")
            };
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, param);
            return source.Where(lambda);
        }

        public static IQueryable<T> WhereDynamicIn<T>(this IQueryable<T> source, string field, List<string> values)
        {
            var param = System.Linq.Expressions.Expression.Parameter(typeof(T), "e");
            var prop = System.Linq.Expressions.Expression.Property(param, field);
            var containsMethod = typeof(List<string>).GetMethod("Contains", new[] { typeof(string) });
            var valuesExpr = System.Linq.Expressions.Expression.Constant(values);
            var body = System.Linq.Expressions.Expression.Call(valuesExpr, containsMethod!, prop);
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, param);
            return source.Where(lambda);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string field)
        {
            return source.OrderBy(e => EF.Property<object>(e, field));
        }
        public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string field)
        {
            return source.OrderByDescending(e => EF.Property<object>(e, field));
        }
      
    }
}
