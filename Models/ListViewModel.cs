using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Models
{
    public class ListViewModel<T>
    {
        public List<T> Results { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
