using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.Objects;
using TodoApp.Utilities;
using System.Collections.Generic;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeController(ILogger<HomeController> logger, IHomeworkRepository homeworkRepository)
        {
            _logger = logger;
            this._homeworkRepository = homeworkRepository;
        }

        public async Task<IActionResult> IndexAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Homework>? todos = await _homeworkRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                todos = todos.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    todos = todos.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    todos = todos.OrderBy(s => s.DateModified);
                    break;
                case "date_desc":
                    todos = todos.OrderByDescending(s => s.DateModified);
                    break;
                default:
                    todos = todos.OrderBy(s => s.DateCreated);
                    break;
            }

            int pageSize = 3;
            return View(PaginatedList<Homework>.Create(todos.ToList(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}