using FinalProjectProgWeb3.Controllers.Filters;
using FinalProjectProgWeb3.Core.Interfaces;
using FinalProjectProgWeb3.Core.Services;
using FinalProjectProgWeb3.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<GeneralExceptionFilter>();
});

builder.Services.AddScoped<ICityEventServices, CityEventServices>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepositoy>();
builder.Services.AddScoped<IEventReservationServices, EventReservationServices>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();

builder.Services.AddScoped<CheckingCityEventExistsFilter>();
builder.Services.AddScoped<CheckingCityEventHasReservations>();
builder.Services.AddScoped<CheckingEventReservationExistsFilter>();


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

app.Run();
