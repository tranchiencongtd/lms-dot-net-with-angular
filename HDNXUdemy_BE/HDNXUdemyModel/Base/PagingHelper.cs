using System.Linq.Dynamic.Core;

namespace HDNXUdemyModel.Base
{
    public static class PagingHelper
    {
        public static PagedResult<T> GetPagingPaged<T>(this IQueryable<T> query, int page, int pageSize, List<OptionFilters> optionFilters) where T : class
        {
            string filter = string.Empty;
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            if (optionFilters != null && optionFilters.Any())
            {
                filter = string.Join(" && ", optionFilters.Select(x => x.ConvertOperator(x.FieldName ?? string.Empty, x.Operator ?? string.Empty, x.Value ?? string.Empty)));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(filter);
            }

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public static PagedResult<T> GetPagingPaged<T>(this IEnumerable<T> query, int page, int pageSize, List<OptionFilters>? optionFilters)
            where T : class
        {
            string filter = string.Empty;
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            if (optionFilters != null && optionFilters.Any())
            {
                filter = string.Join(" || ", optionFilters.Select(x => x.ConvertOperator(x.FieldName!, x.Operator!, x.Value!)));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.AsQueryable().Where(filter);
            }

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public static PagedResult<T> GetPagingPaged<T>(this IList<T> query, int page, int pageSize, List<OptionFilters> optionFilters) where T : class
        {
            string filter = string.Empty;
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (optionFilters != null && optionFilters.Any())
            {
                filter = string.Join(" && ", optionFilters.Select(x => x.ConvertOperator(x.FieldName!, x.Operator!, x.Value!)));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.AsQueryable().Where(filter).ToList();
            }

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
    }
}