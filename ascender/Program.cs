using ascender.core.repositories;
using ascender.core.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMetricRepository, InMemoryMetricRepository>();
builder.Services.AddScoped<MetricService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-cq2m3y8hdh5dmazd.us.auth0.com/";
    options.Audience = "ascender-test";
});

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

var endpointBuilder = app.MapControllers();

if (app.Environment.IsDevelopment())
{
    endpointBuilder.AllowAnonymous();
}

app.Run();

namespace ascender
{
    public partial class Program { }
}