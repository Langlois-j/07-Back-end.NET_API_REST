using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IRepository<BidList>, BidListRepository>();
builder.Services.AddScoped<IRepository<CurvePoint>, CurvePointRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
builder.Services.AddScoped<IRepository<RuleName>, RuleNameRepository>();
builder.Services.AddScoped<IRepository<Trade>, TradeRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

// Mappers
builder.Services.AddScoped<IMapper<User, UserDTO>, UserMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
