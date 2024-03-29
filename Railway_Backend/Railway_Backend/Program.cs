using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Railway_Backend.Collections;
using Railway_Backend.Model;
using Railway_Backend.Repository;
using Railway_Backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings")
);

builder.Services.AddSingleton<IMongoDatabase>(options =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    var client = new MongoClient(mongoSettings.ConnectionString);
    return client.GetDatabase(mongoSettings.DatabaseName);
});

builder.Services.AddScoped<ITicketBookingRepository, TicketBookingRepository>();
builder.Services.AddScoped<ITrainScheduleRepository, TrainScheduleRepository>();
builder.Services.AddScoped<ICancelledTrainsRepository, CancelledTrainRepository>();
builder.Services.AddScoped<IDivertedTrainRepository, DivertedTrainRepository>();
builder.Services.AddScoped<IRegisteredUser, RegisteredUser>();
builder.Services.AddScoped<ICustomerServiceRepository, CustomerServiceRepository>();
BsonClassMap.RegisterClassMap<Station>();
BsonClassMap.RegisterClassMap<TypeOfClass>();
builder.Services.AddHttpClient<LiveStatus>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
