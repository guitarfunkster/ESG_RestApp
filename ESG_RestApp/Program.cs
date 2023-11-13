using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ESG_RestApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ESG_RestAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ESG_RestAppContext") ?? throw new InvalidOperationException("Connection string 'ESG_RestAppContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
