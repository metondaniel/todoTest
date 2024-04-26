using Microsoft.Extensions.Options;
using TodoProject.Domain;
using TodoProject.Domain.Interfaces.Repositories;
using TodoProject.Domain.Interfaces.Services;
using ToDoProject.Repository;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ITodoService,TodoService>();

// Configurações do MongoDB
builder.Services.Configure<MongoSettings>( builder.Configuration.GetSection( "MongoDb" ) );

// Repositórios
builder.Services.AddScoped(
typeof(ITodoRepository), sp =>
{
var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
return new TodoRepository( settings );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors( options =>
{
    options.AddPolicy( "AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader() );
} );
var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
