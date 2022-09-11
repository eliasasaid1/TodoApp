namespace TodoApp.Models.Options
{
    public class HomeworkOptions
    {
        public int PerPage { get; set; } = 4;
        public HomeworkOrderOptions Order { get; set; }
    }

    public class HomeworkOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}
