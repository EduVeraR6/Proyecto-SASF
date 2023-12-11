using AGE.Middleware;
using AGE.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Repositories.AnimalesRepository;
using Proyecto_SASF.Repositories.RazaRepository;
using Proyecto_SASF.Repositories.ZoologicoSecuenciasRepository;
using Proyecto_SASF.Services.AnimalesService;
using Proyecto_SASF.Services.RazasService;
using Proyecto_SASF.Services.ZoologicoSecuenciasService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

//dbContext
builder.Services.AddSqlServer<ZoologicoContext>(builder.Configuration.GetConnectionString("conexion"));




builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Add services to the container.
builder.Services.AddScoped<IZoologicoSecuenciasRepository, ZoologicoSecuenciasRepository>();
builder.Services.AddScoped<IZoologicoSecuenciasService, ZoologicoSecuenciasService>();

builder.Services.AddScoped<IAnimalesRepository, AnimalesRepository>();
builder.Services.AddScoped<IAnimalesService, AnimalesService>();

builder.Services.AddScoped<IRazaRepository, RazaRepository>();
builder.Services.AddScoped<IRazasService, RazasService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Proyecto_SASF", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

builder.Services.AddHttpClient();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWTIssuer"),
        ValidAudience = Environment.GetEnvironmentVariable("JWTAudience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKey")))
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseGlobalExceptionHandler();
app.UseAuthenticationFilter();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthorization();

app.MapControllers();

app.Run();