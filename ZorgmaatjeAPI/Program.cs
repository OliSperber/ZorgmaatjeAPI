using Microsoft.AspNetCore.Mvc.Authorization;
using ZorgmaatjeAPI.Extensions;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter()); // Enforce authorization globally
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read SQL connection string & Create service
var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
builder.Services.AddSingleton<ConnectionStringService>();


var jwtSecret = builder.Configuration.GetValue<string>("SecretJwtKey");

builder.Services.AddCustomIdentity(sqlConnectionString);
builder.Services.AddJwtAuthentication(builder.Configuration, jwtSecret);

// Register Services
builder.Services.AddScoped<TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<UserIdService>();

// Register Repositories
builder.Services.AddScoped<DiaryEntryRepository>();
builder.Services.AddScoped<ChildRepository>();
builder.Services.AddScoped<ChildLevelCompletionRepository>();
builder.Services.AddScoped<GuardianRepository>();
builder.Services.AddScoped<StickerRepository>();
builder.Services.AddScoped<LevelRepository>();
builder.Services.AddScoped<AppointmentRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// API status endpoint
app.MapGet("/", () => new { Status = "API is running", ConnectionStringFound = sqlConnectionStringFound })
    .AllowAnonymous();


app.Run();
