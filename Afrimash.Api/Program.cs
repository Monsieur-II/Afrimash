using System.Text;
using Afrimash.Api;
using Afrimash.Api.Services;
using Afrimash.Api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddCors(options => options
    .AddPolicy("Afrimash", policy => policy
        // Public API: Allowing any origin is intentional and documented.
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("Content-Disposition")));

builder.Services.AddSingleton<FileReaderService>();

builder.Services.Configure<ExternalUrls>(builder.Configuration.GetSection(nameof(ExternalUrls)));
builder.Services.AddControllers();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

// builder.Services.AddAuthentication(x =>
//     {
//         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(x =>
//     {
//         x.SaveToken = true;
//         x.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = CommonConstants.Issuer,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CommonConstants.SigningKey)),
//             ValidAudience = CommonConstants.Audience,
//             ValidateAudience = true,
//             ValidateLifetime = false,
//             RequireExpirationTime = true
//         };
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthentication();
// app.UseAuthorization();

app.UseRouting();

app.UseCors("Afrimash");

app.MapControllers();

app.LoadDataAsync();

await app.RunAsync();
