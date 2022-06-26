using ADV.BadBroker.WebService.BackgroundServices;
using ADV.BadBroker.WebService.BL;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//checking that our application is working
builder.Services.AddProblemDetails();
//checking that our DB is working
builder.Services.AddHealthChecks().AddDbContextCheck<ADV.BadBroker.DAL.Context>(name: "DataBase", tags: new[] { "database" });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.AddHostedService<BackgroundWriteOff>();

builder.Services.AddTransient<IWriteOff, WriteOff>();
builder.Services.AddTransient<IÑalculationService, ÑalculationService>();
builder.Services.AddTransient<IÑalculationServiceHelper, ÑalculationServiceHelper>();

// here we can connect any database 
//builder.Services.AddDbContext<Context>(options => options.UseSqlite("Filename=BadBrokerTestTask.db"));
builder.Services.AddDbContext<ADV.BadBroker.DAL.Context>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

var url = builder.Configuration.GetSection("Urls");
var keys = builder.Configuration.GetSection("Keys");

//we do this for all requests, we use a pull
builder.Services.AddHttpClient<IExchangeratesapi, Exchangeratesapi>("", client =>
{
    client.BaseAddress = new Uri(url["ExchangeratesapiUrl"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("apikey", keys["ExchangeratesapiKey"]);
})
.AddPolicyHandler(GetRetryPolicy());

//Polly is a .NET library that provides failover and transient failure handling capabilities.
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
