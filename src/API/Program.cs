using System.Text;
using API.Configuration;
using Application.Configuration;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddEnvironmentVariables();

    var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
    var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

    builder
        .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey =
                    jwtKey != null ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) : null
            };
        });

    builder
        .Services.AddDatabaseInfrastracture(builder.Configuration)
        .AddApplicationServices()
        .AddAPI();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer().AddCustomSwaggerGen();
    }

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoCreateAPI v1"));
    }

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CoCreateDbContext>(); // Replace YourDbContext with your actual DbContext class
        dbContext.Database.Migrate();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Finally");
}
