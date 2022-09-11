using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using TodoApp.Models;
using TodoApp.Models.Options;
using TodoApp.Objects;

namespace TodoApp.Utilities.ModelBinders
{
    public class HomeworkListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<HomeworkOptions> todoOptions;
        public HomeworkListInputModelBinder(IOptionsMonitor<HomeworkOptions> coursesOptions)
        {
            this.todoOptions = coursesOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext?.ValueProvider.GetValue("Search").FirstValue ?? string.Empty;
            string orderBy = bindingContext?.ValueProvider.GetValue("OrderBy").FirstValue ?? nameof(Homework.Title);
            _ = int.TryParse(bindingContext?.ValueProvider.GetValue("Page").FirstValue, out int page);
            _ = bool.TryParse(bindingContext?.ValueProvider.GetValue("Ascending").FirstValue ?? "true", out bool ascending);

            var options = todoOptions.CurrentValue;
            var inputModel = new HomeworkListInputModel(search, page, orderBy, ascending, options.PerPage);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            //Restituiamo un task completato
            return Task.CompletedTask;
        }
    }
}
