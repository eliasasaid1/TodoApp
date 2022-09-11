using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.Objects;
using TodoApp.Utilities;
using System.Collections.Generic;
using TodoApp.Models.Options;

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

        public async Task<IActionResult> IndexAsync(HomeworkListInputModel model, int? selectedId)
        {
            ViewData["Title"] = "Todo App";
            var todos = await _homeworkRepository.GetAllAsync(model);
            var page = model?.Page;
            if (selectedId != default && model != null)
            {
                page = Convert.ToInt32(Math.Floor(Convert.ToDecimal(todos.Select(d => d.Id).ToList().IndexOf(selectedId.GetValueOrDefault()) / model?.Limit))) + 1;
                model.Page = page.GetValueOrDefault();
            }
            var todosList = PaginatedList<Homework>.Create(todos.ToList(), page ?? 0, model?.Limit ?? 0);
            HomeworkListViewModel view = new()
            {
                Todos = new ListViewModel<Homework> { Results = todosList, TotalCount = todos.ToList().Count },
                SelectedId = selectedId,
                Input = model
            };
            return View(view);
        }
        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View("EditCreate", new Homework());
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Homework entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("EditCreate", entity);

                await TryUpdateModelAsync(entity);
                entity.DateCreated = DateTime.Now;
                entity.DateModified = DateTime.Now;
                var id = await _homeworkRepository.InsertAsync(entity);
                return RedirectToAction(nameof(Index), new { selectedId = id });
            }
            catch (Exception e)
            {
                throw new Exception("error while adding entry " + e.Message);
            }
        }

        // GET: HomeController1/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var entity = await _homeworkRepository.GetByIdAsync(id);
            if (entity != null)
                return View("EditCreate", entity);

            throw new Exception("entry not found");
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Homework entity ,int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("EditCreate", entity);

                await TryUpdateModelAsync(entity);
                entity.DateModified = DateTime.Now;
                await _homeworkRepository.UpdateAsync(entity);    
                return RedirectToAction(nameof(Index), new { selectedId = id });
            }
            catch(Exception e)
            {
                throw new Exception("error while updating " + e.Message);
            }
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var entity = await _homeworkRepository.GetByIdAsync(id);
                if (entity == null)
                    return View("Error", "entry not found");

                await _homeworkRepository.DeleteAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw new Exception("error while deleting " + e.Message);
            }
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