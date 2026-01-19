using Microsoft.EntityFrameworkCore;
using LoginAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Entity Framework + Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleDB")
    )
);

var app = builder.Build();

// HTTP pipeline
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
