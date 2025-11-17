using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Services;
using PAD.Backend.ThirdPartyServiceCommunication.MercadoPago.Service;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

string renaperApiKey = builder.Configuration["RENAPER_API_KEY"]
                        ?? throw new InvalidOperationException("La variable de entorno 'RENAPER_API_KEY' no se ha encontrado.");
string mercadoPagoAccessToken = builder.Configuration["MP_ACCESS_TOKEN"]
                                ?? throw new InvalidOperationException("La variable de entorno 'MP_ACCESS_TOKEN' no se ha encontrado.");

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<TransaccionService>();
builder.Services.AddScoped<TitularService>();
builder.Services.AddScoped<PatenteService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<MercadoPagoService>();
builder.Services.AddScoped<MarcaService>();
builder.Services.AddScoped<ModeloService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient("RenaperClient", client =>
{
    client.BaseAddress = new Uri("https://renaper-simulador.onrender.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("X-API-Key", renaperApiKey);
});


builder.Services.AddScoped<RenaperService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            // Hay que ver que puertos usa el front en local
            policy.WithOrigins("http://localhost:7121", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
