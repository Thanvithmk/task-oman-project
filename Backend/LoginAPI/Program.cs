using Microsoft.EntityFrameworkCore;
using LoginAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("OracleDB")
    )
);

var app = builder.Build();

app.MapControllers();
app.Run();
