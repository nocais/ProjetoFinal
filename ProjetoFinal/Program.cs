
// Configura e liga a API, registra os serviços, adiciona middlewares e filtros

using ProjetoFinal.Filters;
using ProjetoFinal.Interfaces;
using ProjetoFinal.Middlewares;
using ProjetoFinal.Repository;
using ProjetoFinal.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var corsOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>()
                  ?? new[] { "https://brunotrbr.github.io" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific", policy =>
    {
        policy.WithOrigins(corsOrigins).AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddScoped<ValidacaoFilter>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseCors("AllowSpecific");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();