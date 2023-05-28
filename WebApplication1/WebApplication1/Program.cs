using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication1.Common.Converters;
using WebApplication1.Middlewares;
using WebApplication1.Services.AuthService;
using WebApplication1.Services.BookService;
using WebApplication1.Services.PasswordService;
using WebApplication1.Services.PublisherService;
using WebApplication1.Services.ReviewService;
using WebApplication1.Services.VersionServices.Version1;
using WebApplication1.Services.VersionServices.Version2;
using WebApplication1.Services.VersionServices.Version3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IPublisherService, PublisherService>();// Singleton for services with data we want to preserve
builder.Services.AddSingleton<IBookService, BookService>();// Singleton for services with data we want to preserve
builder.Services.AddSingleton<IPasswordService, PasswordService>();// We want to use this inside singleton service
builder.Services.AddSingleton<IAuthService, AuthService>();// Singleton for services with data we want to preserve
builder.Services.AddSingleton<IReviewService, ReviewService>();// Singleton for services with data we want to preserve

builder.Services.AddScoped<IIntegerService, IntegerService>(); // No need to preserve data
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<IExcelService, ExcelService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("secretKey").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1.0", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2.0", Version = "v2" });
    options.SwaggerDoc("v3", new OpenApiInfo { Title = "API v3.0", Version = "v3" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please enter token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2.0");
        options.SwaggerEndpoint("/swagger/v3/swagger.json", "API V3.0");

        options.RoutePrefix = "swagger";
        options.DocumentTitle = "API";
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
