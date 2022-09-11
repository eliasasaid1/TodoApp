using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.Options;
using TodoApp.Objects;
using TodoApp.Utilities.ModelBinders;

namespace TodoApp.Models
{
    //[ModelBinder(BinderType = typeof(HomeworkListInputModelBinder<HomeworkListInputModel, HomeworkOptions>))]
    //public class HomeworkListInputModel : BaseListInputModel<HomeworkListInputModel, HomeworkOptions>
    //{
    //    public HomeworkListInputModel() { }
    //}
    [ModelBinder(BinderType = typeof(HomeworkListInputModelBinder))]
    public class HomeworkListInputModel
    {
        public HomeworkListInputModel(string search, int page, string orderby, bool ascending, int limit)
        {
            Search = search ?? "";
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);
            OrderBy = orderby;
            Ascending = ascending;
        }
        public string Search { get; }
        public int Page { get; set; }
        public string OrderBy { get; }
        public bool Ascending { get; }

        public int Limit { get; }
    }

}
