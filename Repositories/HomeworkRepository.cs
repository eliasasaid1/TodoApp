using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Reflection;
using Dapper;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.Objects;

namespace TodoApp.Repositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly ToDoAppContext _toDoAppContext;

    public HomeworkRepository(ToDoAppContext toDoAppContext)
    {
        _toDoAppContext = toDoAppContext;
        SqlMapper.SetTypeMap(typeof(Homework), new CustomPropertyTypeMap(
    typeof(Homework), (type, columnName) => GetMemberInfo(type, columnName)));
    }

    public async Task<Homework> GetByIdAsync(int id)
    {
        string query = "select * from dbo.Homework where id = @Id";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var homework = await connection.QuerySingleOrDefaultAsync<Homework>(query, new { id });
        return homework;
    }

    public async Task<IEnumerable<Homework>> GetAllAsync(HomeworkListInputModel model)
    {
        string query = "select * from dbo.Homework /**where**/ /**orderby**/";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var builder = new SqlBuilder();
        var select = builder.AddTemplate(query);
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(model.Search))
        {
            if (IsDate(model.Search))
            {
                builder.Where($"CAST({GetCustomAttributeName(nameof(Homework.DateCreated))} AS DATE) = @Date or CAST({GetCustomAttributeName(nameof(Homework.DateModified))} AS DATE) = @Date");
                parameters.AddDynamicParams(new { Date = Convert.ToDateTime(model.Search) } );
            }
            else
            {
                builder.Where($"{GetCustomAttributeName(nameof(Homework.Title))} like @Name ");
                parameters.AddDynamicParams(new { Name = $"%{Convert.ToString(model.Search)}%" });
            }
        }
        if (!string.IsNullOrEmpty(model.OrderBy))
        {
            builder.OrderBy($"{GetCustomAttributeName(model.OrderBy)} {(model.Ascending ? "asc": "desc")}");
        }
        var homeworks = await connection.QueryAsync<Homework>(select.RawSql, parameters);

        return homeworks.ToList();
    }

    public async Task<int> InsertAsync(Homework homework)
    {
        string query = "insert into dbo.Homework(homework,date_created,date_modified) values(@Homework,@DateCreated,@DateModified);SELECT SCOPE_IDENTITY()";
        using IDbConnection connection = _toDoAppContext.CreateConnection();
        var result = await connection.QuerySingleAsync<int>(query , new { Homework =  homework.Title, homework.DateCreated, homework.DateModified });
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

    private bool IsDate(string date)
    {
        try
        {
            string[] formats = { "yyyy-MM-dd" };
            return DateTime.TryParseExact(date, formats, new CultureInfo("en-ca"), DateTimeStyles.None, out DateTime parsedDateTime);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private string? GetCustomAttributeName(string columnName) =>
        typeof(Homework)?.GetProperties().FirstOrDefault(d => d.Name.ToLower() == columnName.ToLower())?.GetCustomAttribute<ColumnAttribute>()?.Name;
    private static PropertyInfo? GetMemberInfo(Type type, string columnName) => type.GetProperties().FirstOrDefault(prop =>
             prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName));
}
