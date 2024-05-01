using Microsoft.EntityFrameworkCore;

//This is  C# 12 primary Constructor
public class TodoDb(DbContextOptions<TodoDb> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}