using ERP.API.Models;

namespace ERP.API.Helpers
{
    // Helper methods for filtering, sorting, and pagination
    public class JsonFilterSortPagination
    {
        public static readonly HashSet<string> AllowedFields = new() { "name", "department", "salary", "joiningDate", "job" };

        private const int MaxPageSize = 100;
        public IQueryable<T> ApplyJsonFilter<T>(IQueryable<T> query, LogicalFilter? filter)
        {
            if (filter == null) return query;
            // Only support AND for simplicity; extend as needed
            if (filter.And != null)
            {
                foreach (var cond in filter.And)
                {
                    foreach (var field in cond.Keys)
                    {
                        if (!AllowedFields.Contains(field)) continue;
                        var value = cond[field];
                        if (value.Eq != null)
                            query = query.WhereDynamic(field, "==", value.Eq);
                        if (value.Gt != null)
                            query = query.WhereDynamic(field, ">", value.Gt);
                        if (value.Gte != null)
                            query = query.WhereDynamic(field, ">=", value.Gte);
                        if (value.Lt != null)
                            query = query.WhereDynamic(field, "<", value.Lt);
                        if (value.Lte != null)
                            query = query.WhereDynamic(field, "<=", value.Lte);
                        if (value.Neq != null)
                            query = query.WhereDynamic(field, "!=", value.Neq);
                        if (value.In != null && value.In.Count > 0)
                            query = query.WhereDynamicIn(field, value.In);
                    }
                }
            }
            // TODO: Add OR support if needed
            return query;
        }

        private IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortOption? sort)
        {
            if (sort == null || !AllowedFields.Contains(sort.Field)) return query;
            return sort.Order.ToLower() == "desc"
                ? query.OrderByDescendingDynamic(sort.Field)
                : query.OrderByDynamic(sort.Field);
        }

        private IQueryable<T> ApplyPagination<T>(IQueryable<T> query, Pagination? pagination)
        {
            if (pagination == null) return query.Take(20);
            var page = Math.Max(1, pagination.Page);
            var pageSize = Math.Min(MaxPageSize, Math.Max(1, pagination.PageSize));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

    }
}
