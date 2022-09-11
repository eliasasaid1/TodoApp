using TodoApp.Objects;
using TodoApp.Utilities;

namespace TodoApp.Models
{
    public class HomeworkListViewModel : IPaginationInfo
    {
        public ListViewModel<Homework>? Todos { get; set; }
        public int? SelectedId { get; set; }

        public HomeworkListInputModel? Input { get; set; }

        #region Implementazione IPaginationInfo
        int IPaginationInfo.CurrentPage => Input?.Page ?? 0;

        int IPaginationInfo.TotalResults => Todos?.TotalCount ?? 0;

        int IPaginationInfo.ResultsPerPage => Input?.Limit ?? 0;

        string IPaginationInfo.Search => Input?.Search ?? string.Empty;

        string IPaginationInfo.OrderBy => Input?.OrderBy ?? string.Empty;

        bool IPaginationInfo.Ascending => Input?.Ascending ?? false;

        #endregion
    }
}
