using Catalogo.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar los controladores
builder.Services.AddControllers();

// Configurar la conexión a SQL Server
builder.Services.AddDbContext<CatalogoDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CatalogoConnection")
    )
);

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();