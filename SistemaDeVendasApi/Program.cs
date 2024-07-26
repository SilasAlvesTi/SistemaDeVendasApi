using Microsoft.EntityFrameworkCore;
using SistemaDeVendasApi.Data;
using SistemaDeVendasApi.EndPoints.ClienteEndPoints;
using SistemaDeVendasApi.Repositories;
using SistemaDeVendasApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("SistemaDeVendasContext");
builder.Services.AddDbContext<SistemaDeVendasContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();


builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<VendaRepository>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<ItemVendaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
