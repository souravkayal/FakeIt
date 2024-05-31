using FakeIt.Repository.CreateAPI;
using FakeIt.Service.CreateAPI;

var builder = WebApplication.CreateBuilder(args);

// Add application's services to the container.
builder.Services.AddTransient<ICreateAPIServiceInterface, CreateAPIServiceImplementation>();
builder.Services.AddTransient<ICreateAPIRepositoryInterface, CreateAPIRepositoryImplementation>();


// Application settings
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
