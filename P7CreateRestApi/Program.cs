using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;
using Dot.Net.WebApi.Mappers;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LocalDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
var secretKey = configuration["Jwt:SecretKey"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Repositories
builder.Services.AddScoped<IRepository<BidList>, BidListRepository>();
builder.Services.AddScoped<IRepository<CurvePoint>, CurvePointRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
builder.Services.AddScoped<IRepository<RuleName>, RuleNameRepository>();
builder.Services.AddScoped<IRepository<Trade>, TradeRepository>();

// Mappers
builder.Services.AddScoped<IMapper<BidList, BidListDTO>, BidListMapper>();
builder.Services.AddScoped<IMapper<CurvePoint, CurveDTO>, CurveMapper>();
builder.Services.AddScoped<IMapper<Rating, RatingDTO>, RatingMapper>();
builder.Services.AddScoped<IMapper<RuleName, RuleNameDTO>, RuleNameMapper>();
builder.Services.AddScoped<IMapper<Trade, TradeDTO>, TradeMapper>();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // ← avant UseAuthorization !
app.UseAuthorization();

app.MapControllers();

app.Run();