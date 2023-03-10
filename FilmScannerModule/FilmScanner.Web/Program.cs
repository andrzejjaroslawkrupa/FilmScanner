using FilmScanner.Web.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var AllowFrontEndOrigin = "_allowFrontendOrigin";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFilmScannerDb();
builder.Services.AddCors(a => a.AddPolicy(name: AllowFrontEndOrigin, policy =>
{
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader();
}));
builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secret = Environment.GetEnvironmentVariable("FILMSCANNER_SECRET");
    if (secret == null)
        throw new NullReferenceException(nameof(secret));
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        ValidateIssuer = true,
        ValidIssuer = "filmscanner",
        ValidateAudience = true,
        ValidAudience = "filmscanner-consumer",
        ValidateLifetime = true,
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowFrontEndOrigin);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
