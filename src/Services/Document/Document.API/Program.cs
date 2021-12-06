using Document.API.Services;
using Document.API.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<DocumentDatabaseSettings>(
    configuration.GetSection(nameof(DocumentDatabaseSettings)));
builder.Services.AddSingleton<IDocumentDatabaseSettings>(provider =>
    provider.GetRequiredService<IOptions<DocumentDatabaseSettings>>().Value);
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<DocumentBsonService>();
builder.Services.AddScoped<DocumentExpandoService>();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
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
