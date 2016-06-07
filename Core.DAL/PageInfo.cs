using System;
using System.Linq;

namespace Core.DAL
{
    public class PageInfo<T>
    {
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemPerPage); }
        }
        public IQueryable<T> Entity { get; set; }
    }
}
