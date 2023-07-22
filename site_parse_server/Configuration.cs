using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using site_parse_server.Models;
using site_parse_server.Models.Authorization;
using site_parse_server.Repositories;
using site_parse_server.Repositories.Interfaces;
using site_parse_server.Services;
using site_parse_server.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Configuration
{
    public static void Configurate(IServiceCollection services)
    {
        services.AddDbContext<DataBaseContext>();
        services.AddControllersWithViews();
        services.AddControllers();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRequestService, RequestService>();
        
        
        /* services.AddMvc().AddJsonOptions(options => {
             options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;


             });*/


        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });


        services.AddCors(options =>
        {
            options.AddPolicy(name: "MyCors",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5035", "http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                   
                    
                    
                    
                           
                });
        });
        services.AddAuthorization();
    }
}


