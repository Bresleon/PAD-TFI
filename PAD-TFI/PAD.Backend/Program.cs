using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

string renaperApiKey = builder.Configuration["RENAPER_API_KEY"]
                        ?? throw new InvalidOperationException("La variable de entorno 'RENAPER_API_KEY' no se ha encontrado.");

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient("RenaperClient", client =>
{
    // Aqui pondremos la url que despliegue el grupo de RENAPER
    client.BaseAddress = new Uri("https://renaper-simulador.onrender.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("X-API-Key", renaperApiKey);
});

builder.Services.AddScoped<RenaperService>();

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
