using FakeIt.Repository.CosmosConnector;
using FakeIt.Repository.CreateAPI;
using FakeIt.Repository.ReadAPI;
using FakeIt.Service.CreateAPI;
using FakeIt.Service.ReadAPI;
using FakeIt.Web;
using FakeIt.Web.Filters;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add application's services to the container.
builder.Services.AddTransient<ICreateAPIServiceInterface, CreateAPIServiceImplementation>();
builder.Services.AddTransient<ICreateAPIRepositoryInterface, CreateAPIRepositoryImplementation>();

builder.Services.AddTransient<IReadAPIServiceInterface, ReadAPIServiceImplementation>();
builder.Services.AddTransient<IReadAPIRepositoryInterface, ReadAPIRepositoryImplementation>();


// Application settings
builder.Services.AddControllers(options => 
{
    options.Filters.Add<CustomValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var configuration = builder.Configuration;

//We really don't need to create cosmos client for each need
builder.Services.AddSingleton<CosmosClient>(sp =>
{
    var connectionString = configuration["CosmosDb:ConnectionString"];
    return new CosmosClient(connectionString);
});

//Inject cosmos Connect
builder.Services.AddTransient<CosmosConnect>(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    var databaseId = configuration["CosmosDb:DatabaseId"];

    if (databaseId == null)
        throw new Exception("Error in cosmos db setup. Database name missing in config");

    return new CosmosConnect(cosmosClient, databaseId);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
