using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Objects
{
    [Table("Homework")]
    public class Homework
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("homework")]
        public string Title { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Column("date_modified")]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}