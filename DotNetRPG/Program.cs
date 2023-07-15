using DotNetRPG.Data;
using DotNetRPG.Helper;
using DotNetRPG.Services.Implementations;
using DotNetRPG.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Runtime.Intrinsics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = "Standard authorization header using the bearer scheme, e.g. \"bearer {token} \"",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Name = "Authorization",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
	});
    options.OperationFilter<SecurityRequirementsOperationFilter>();

	options.SwaggerDoc("v1",new OpenApiInfo
	{
		Version = "v1.0",
		Title = "Dotnet.RPG_v1",
		Description = "DotnetRpg Game V1 Description"
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Dotnet.RPG_v2",
        Description = "DotnetRpg Game V1 Description"
    });
});

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IFightService, FightService>(); 
builder.Services.AddScoped<IskillService, SkillService>();
builder.Services.AddScoped<HelperMethods>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddApiVersioning(options =>
{
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
}
);
builder.Services.AddVersionedApiExplorer(options =>
	options.GroupNameFormat = "'v'VVV"
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
		.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
		ValidateIssuer = false,
		ValidateAudience = false
	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI( options =>
	{
	  options.SwaggerEndpoint("/swagger/v1/swagger.json","Dotnet.RPG_v1");
	  options.SwaggerEndpoint("/swagger/v2/swagger.json","Dotnet.RPG_v2");
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
