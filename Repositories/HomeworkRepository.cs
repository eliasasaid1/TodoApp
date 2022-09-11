using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Dapper;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Objects;

namespace TodoApp.Repositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly ToDoAppContext _toDoAppContext;

    public HomeworkRepository(ToDoAppContext toDoAppContext)
    {
        _toDoAppContext = toDoAppContext;
        SqlMapper.SetTypeMap(typeof(Homework), new CustomPropertyTypeMap(
    typeof(Homework), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
    prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
    }

    public async Task<Homework> GetByIdAsync(int id)
    {
        string query = "select * from dbo.Homework where id = @Id";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var homework = await connection.QuerySingleOrDefaultAsync<Homework>(query, new { id });
        return homework;
    }

    public async Task<IEnumerable<Homework>> GetAllAsync()
    {
        string query = "select * from dbo.Homework";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var homeworks = await connection.QueryAsync<Homework>(query);
        return homeworks.ToList();
    }

    public async Task<int> InsertAsync(Homework homework)
    {
        string query = "insert into dbo.Homework(homework,date_created,date_modified) values(@Homework,@DateCreated,@DateModified)";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var result = await connection.QuerySingleAsync(query , new { Homework =  homework.Title, homework.DateCreated, homework.DateModified });
        return result;
    }

    public async Task<int> UpdateAsync(Homework homework)
    {
        string query = "update dbo.Homework set homework = @Homework, date_modified = @DateModified where id = @Id";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { Homework = homework.Title, homework.Id, homework.DateModified });
        return result;
    }

    public async Task<int> DeleteAsync(Homework homework)
    {
        string query = "delete from dbo.Homework where id = @Id";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { homework.Id });
        return result;
    }
}
