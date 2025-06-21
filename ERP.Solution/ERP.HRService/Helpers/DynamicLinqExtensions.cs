using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Helpers
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

        // IEnumerable<T> version for in-memory filtering
        public static IEnumerable<T> WhereDynamic<T>(this IEnumerable<T> source, string field, string op, string value)
        {
            foreach (var item in source)
            {
                var prop = typeof(T).GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (prop == null) continue;
                var propValue = prop.GetValue(item);
                var compareValue = Convert.ChangeType(value, prop.PropertyType);
                int cmp = 0;
                if (propValue is IComparable comp)
                    cmp = comp.CompareTo(compareValue);
                else if (propValue != null && propValue.Equals(compareValue))
                    cmp = 0;
                else
                    cmp = -1;
                bool match = op switch
                {
                    "==" => Equals(propValue, compareValue),
                    "!=" => !Equals(propValue, compareValue),
                    ">" => cmp > 0,
                    ">=" => cmp >= 0,
                    "<" => cmp < 0,
                    "<=" => cmp <= 0,
                    _ => false
                };
                if (match) yield return item;
            }
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

        // IEnumerable<T> version for in-memory filtering
        public static IEnumerable<T> WhereDynamicIn<T>(this IEnumerable<T> source, string field, List<string> values)
        {
            foreach (var item in source)
            {
                var prop = typeof(T).GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (prop == null) continue;
                var propValue = prop.GetValue(item)?.ToString();
                if (propValue != null && values.Contains(propValue))
                    yield return item;
            }
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string field)
        {
            return source.OrderBy(e => EF.Property<object>(e, field));
        }
        public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string field)
        {
            return source.OrderByDescending(e => EF.Property<object>(e, field));
        }

        // IEnumerable<T> versions for in-memory sorting
        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source, string field)
        {
            var prop = typeof(T).GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return prop == null ? source : source.OrderBy(e => prop.GetValue(e));
        }
        public static IEnumerable<T> OrderByDescendingDynamic<T>(this IEnumerable<T> source, string field)
        {
            var prop = typeof(T).GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return prop == null ? source : source.OrderByDescending(e => prop.GetValue(e));
        }
    }
} 