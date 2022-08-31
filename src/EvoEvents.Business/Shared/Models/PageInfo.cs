using System.Collections.Generic;

namespace EvoEvents.Business.Shared.Models
{
    public class PageInfo<T>
    {
        public List<T> Items { get; set; }
        public int TotalNoItems { get; set; }

        public PageInfo(List<T> items, int totalNoItems)
        {
            Items = items;
            TotalNoItems = totalNoItems;
        }

        public PageInfo()
        {
            Items = new List<T>();
            TotalNoItems = 0; 
        }
    }
}