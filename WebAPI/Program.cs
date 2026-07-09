using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interface;
using WebAPI.Respository;
using WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IUsuarioService, UsuarioService>();


builder.Services.AddScoped<IJogoRepository, JogoRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Server=(localdb)\\MSSQLLocalDB;User ID=JULIANA\\_julia;Database=ProjetoTeste;Integrated Security=True;"));
});

builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
