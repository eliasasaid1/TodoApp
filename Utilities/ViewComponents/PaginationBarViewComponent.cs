using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Utilities.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaginationInfo model)
        {
            return View(model);
        }
    }
}
