using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add the API controllers to the app...
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// Add the TodoEndpoints (minimal API) to the app... (https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio-code#add-the-api-code)
app.MapTodoEndpoints();

// Add the API controllers to the app...
app.MapControllers();

app.Run();
