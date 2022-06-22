using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BackgroundServices;
using ADV.BadBroker.WebService.BL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<BackgroundWriteOff>();

builder.Services.AddTransient<IWriteOff, WriteOff>();

/// <summary>
/// here we can connect any database
/// </summary>
//builder.Services.AddDbContext<Context>(options => options.UseSqlite("Filename=BadBrokerTestTask.db"));
builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("InMemoryDatabase"));


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
