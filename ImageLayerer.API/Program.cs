using ImageLayerer.Application.Factories;
using ImageLayerer.Application.Interfaces;
using ImageLayerer.Application.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IImageSourceService, ImageSourceService>();
builder.Services.AddScoped<ILocalFileService, LocalFileService>();
builder.Services.AddScoped<IRemoteFileService, RemoteFileService>();
builder.Services.AddScoped<IAzureService, AzureService>();
builder.Services.AddScoped<IImageDrawingService, ImageDrawingService>();
builder.Services.AddScoped<AzureBlobClientFactory>();

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
