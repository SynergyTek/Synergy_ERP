using System.Collections.Generic;

namespace ERP.API.Models
{
    /// <summary>
    /// Comparison operators for filtering fields.
    /// </summary>
    public class ComparisonOperator
    {
        public string? Eq { get; set; }
        public string? Gt { get; set; }
        public string? Gte { get; set; }
        public string? Lt { get; set; }
        public string? Lte { get; set; }
        public string? Neq { get; set; }
        public List<string>? In { get; set; }
    }

    /// <summary>
    /// A filter condition mapping field names to comparison operators.
    /// </summary>
    public class FilterCondition : Dictionary<string, ComparisonOperator> { }

    /// <summary>
    /// Logical filter supporting AND/OR of filter conditions.
    /// </summary>
    public class LogicalFilter
    {
        public List<FilterCondition>? And { get; set; }
        public List<FilterCondition>? Or { get; set; }
    }

    /// <summary>
    /// Sorting options for queries.
    /// </summary>
    public class SortOption
    {
        public string Field { get; set; } = "name";
        public string Order { get; set; } = "asc"; // asc or desc
    }

    /// <summary>
    /// Pagination options for queries.
    /// </summary>
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// The main filter request for dynamic querying.
    /// </summary>
    public class FilterRequest
    {
        public LogicalFilter? Filter { get; set; }
        public SortOption? Sort { get; set; }
        public Pagination? Pagination { get; set; }
    }
} 