using Microsoft.EntityFrameworkCore;

namespace MovieManagementAPI.Shared
{
    public static class IQueryableExtensions
    {

        public static async Task<IEnumerable<T>> ApplyPagingAsync<T> (this IQueryable<T> query , int pageNumber , int pageSize)
        {

            return await query.Skip((pageNumber - 1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        }
    }
}
