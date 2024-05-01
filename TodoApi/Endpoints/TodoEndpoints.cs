using Microsoft.EntityFrameworkCore;

public static class TodoEndpoints
{
    // https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio-code#add-the-api-code
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        // we added MapGroup, so we do not need to repeat the route prefix again and again
        var group = routes.MapGroup("/min-api/todo").WithTags(nameof(Todo));        

        // get a list of all todos
        group.MapGet("/", async (TodoDb db) =>
           await db.Todos.ToListAsync());

        // get a list of all todos that are complete
        group.MapGet("/complete", async (TodoDb db) =>
            await db.Todos.Where(t => t.IsComplete).ToListAsync());

        //get details of a specific todo by id
        group.MapGet("/{id}", async (int id, TodoDb db) =>
            await db.Todos.FindAsync(id)
                is Todo todo
                    ? Results.Ok(todo)
                    : Results.NotFound());
        // add a new todo
        group.MapPost("/", async (Todo todo, TodoDb db) =>
        {
            db.Todos.Add(todo);
            await db.SaveChangesAsync();

            return Results.Created($"/{todo.Id}", todo);
        });

        // update a todo
        group.MapPut("/{id}", async (int id, Todo inputTodo, TodoDb db) =>
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return Results.NotFound();

            todo.Name = inputTodo.Name;
            todo.IsComplete = inputTodo.IsComplete;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        // delete a todo item by id
        group.MapDelete("/{id}", async (int id, TodoDb db) =>
        {
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}