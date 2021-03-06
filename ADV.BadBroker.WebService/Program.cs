using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BackgroundServices;
using ADV.BadBroker.WebService.BL;
using ADV.BadBroker.WebService.BL.ParametrsRate;
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
builder.Services.AddTransient<IŅalculationService, ŅalculationService>();
builder.Services.AddTransient<IŅalculationServiceHelper, ŅalculationServiceHelper>();

// here we can connect any database 
//builder.Services.AddDbContext<ADV.BadBroker.DAL.Context>(options => options.UseSqlite("Filename=BadBrokerTestTask.db"));
builder.Services.AddDbContext<ADV.BadBroker.DAL.Context>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

AddUserForTest(builder.Services);

builder.Services.Configure<ParametrsRates>(builder.Configuration.GetSection("ParametrsRates"));

//we do this for all requests, we use a pull
builder.Services.AddHttpClient<IExchangeratesapi, Exchangeratesapi>()
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

/// <summary>
/// add one user for testing
/// </summary>
static void AddUserForTest(IServiceCollection services)
{
    var sp = services.BuildServiceProvider();
    using var scope = sp.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<ADV.BadBroker.DAL.Context>();

    var user = new User()
    {
        Name = "First Galactic Emperor"
    };

    db.Users.Add(user);

    var listCurrency = new List<CurrencyReference>(){
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 15),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 16),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 72.99m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 17),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 66.01m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 18),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 61.44m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 19),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 59.79m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 20),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 59.79m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 21),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 59.79m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 22),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 54.78m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 23),
                ŅurrencyValues = new List<ŅurrencyValue>
                {
                    new ŅurrencyValue() { Ņurrency = Ņurrency.RUB, Value = 54.80m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.EUR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.GBR, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.JPY, Value = 60.17m},
                    new ŅurrencyValue() { Ņurrency = Ņurrency.USD, Value = 60.17m},
                }},
            };
    db.CurrencyReference.AddRange(listCurrency);

    db.Settings.Add(new Settings() { LimitHistoricalPeriod = new TimeSpan(60, 0, 0, 0), SpecifiedPeriod = new TimeSpan(1, 0, 0) });
    db.SaveChanges();
}