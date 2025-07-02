using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services dans les controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connexion à la base de données
builder.Services.AddDbContext<KdAtrio.Data.AppDbContext>(options =>
    options.UseSqlite("Data Source=people.db"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api-doc/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api-doc/v1/swagger.json", "People API V1");
        c.RoutePrefix = "api-doc"; 
    });
//}

// app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();