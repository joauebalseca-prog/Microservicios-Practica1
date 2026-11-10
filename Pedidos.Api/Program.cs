using Pedidos.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar los controladores
builder.Services.AddControllers();

// Conexion a SQL Server
builder.Services.AddDbContext<PedidosDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PedidosConnection")
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