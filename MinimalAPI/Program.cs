using Microsoft.EntityFrameworkCore;
using MinimalAPI.EndPoints;
using MinimalAPI.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<CompanyContext>(
     opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("scon"))
    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapProductsEndpoints();

app.Run();
