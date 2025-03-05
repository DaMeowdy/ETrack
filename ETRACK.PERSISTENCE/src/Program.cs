using Microsoft.AspNetCore.Http.HttpResults;
using src;
using src.models;
using src.services.concrete;
using src.services.interfaces;
using src.routers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RailwayContext>();
builder.Services.AddSingleton<ICryptographyService, CryptographyService>();
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRecoveryService, RecoveryService>();
builder.Services.AddScoped<IDosageService, DosageService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAuthenticationRoutes();
app.MapDosageRoutes();
app.UseHttpsRedirection();

app.Run();

