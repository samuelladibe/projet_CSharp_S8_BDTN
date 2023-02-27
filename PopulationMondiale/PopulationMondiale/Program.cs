using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PopulationMondiale.Data;
using PopulationMondiale.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PopulationMondialeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PopulationMondialeContext") ?? throw new InvalidOperationException("Connection string 'PopulationMondialeContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
