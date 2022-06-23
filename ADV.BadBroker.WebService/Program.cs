using ADV.BadBroker.WebService.BackgroundServices;
using ADV.BadBroker.WebService.BL;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//checking that our application is working
builder.Services.AddProblemDetails();
//checking that our DB is working
builder.Services.AddHealthChecks().AddDbContextCheck<ADV.BadBroker.DAL.Context>(name: "DataBase", tags: new[] { "database" });

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
builder.Services.AddDbContext<ADV.BadBroker.DAL.Context>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

var url = builder.Configuration.GetSection("Urls");
var keys = builder.Configuration.GetSection("Keys");

/// <summary>
/// we do this for all requests, we use a pull
/// </summary>
builder.Services.AddHttpClient<IExchangeratesapi, Exchangeratesapi>("", client =>
{
    client.BaseAddress = new Uri(url["ExchangeratesapiUrl"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("apikey", keys["ExchangeratesapiKey"]);
})
.AddPolicyHandler(GetRetryPolicy());

/// <summary>
/// Polly is a .NET library that provides failover and transient failure handling capabilities.
/// </summary>
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
          .HandleTransientHttpError()
          .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

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
