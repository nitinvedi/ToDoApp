using TodoApi.Models;

namespace TodoApi.Services;

public static class TodoService
{
    private static readonly List<TodoItem> Todos = new()
    {
        new TodoItem { Id = 1, Title = "Learn .NET Basics", IsCompleted = true },
        new TodoItem { Id = 2, Title = "Build a Vue.js Frontend", IsCompleted = false }
    };
    
    private static int _nextId = 3;

    public static List<TodoItem> GetAll() => Todos;
    
    public static TodoItem? Get(int id) => Todos.FirstOrDefault(t => t.Id == id);

    public static void Add(TodoItem item)
    {
        item.Id = _nextId++;
        Todos.Add(item);
    }

    public static void Update(TodoItem item)
    {
        var index = Todos.FindIndex(t => t.Id == item.Id);
        if (index != -1)
        {
            Todos[index] = item;
        }
    }

    public static void Delete(int id)
    {
        var item = Get(id);
        if (item != null)
        {
            Todos.Remove(item);
        }
    }
}
