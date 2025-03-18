using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

// Initialize MongoDB
await DB.InitAsync("searchDb", MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDbConnection")));

// Ensure indexes are created
await DB.Index<Item>()
        .Key(x => x.Make, KeyType.Text)
        .Key(x => x.Model, KeyType.Text)
        .Key(x => x.Color, KeyType.Text)
        .CreateAsync();

app.Run();
