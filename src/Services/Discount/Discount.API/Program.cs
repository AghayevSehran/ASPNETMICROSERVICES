using Discount.API.Extentions;
using Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDiscountRepository, DiscoutRepository>();
// Normal AddLogging
builder.Services.AddLogging();

// Additional code to register the ILogger as a ILogger<T> where T is the Startup class
builder.Services.AddSingleton(typeof(ILogger), typeof(Logger<Program>));

// ...

var app = builder.Build();
app.MigrateDatabase<Program>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
