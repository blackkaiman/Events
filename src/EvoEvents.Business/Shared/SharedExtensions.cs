using System;
using System.Linq;

namespace EvoEvents.Business.Shared
{
    public static class SharedExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> itemList, int pageNumber, int pageSize)
        {
            return itemList
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);
        }
    }
}