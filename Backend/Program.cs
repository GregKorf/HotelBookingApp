
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using HotelBookingApp.Configuration;
using HotelBookingApp.Data;
using HotelBookingApp.Helpers;
using HotelBookingApp.Repositories;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace HotelBookingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Congif Serilog
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.

            //Connection with DB
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            
            builder.Services.AddDbContext<HotelBookingDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();
            builder.Services.AddScoped<IAdminService, AdminService>();

            // Add Mapper
            builder.Services.AddScoped(provider =>
              new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile(new MapperConfig());
              })
            .CreateMapper());

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


            // JWT add
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Authentication");
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = jwtSettings["Issuer"],

                    ValidateAudience = false,
                    ValidAudience = jwtSettings["Audience"],

                    ValidateLifetime = true, // ensure not expired

                    ValidateIssuerSigningKey = true,

                   
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings["SecretKey"]!))
                  

                };
            });


            //Cors add
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .WithOrigins("https://localhost:5002", "http://localhost:5002") // Add your frontend's URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // If using cookies or authorization headers
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularClient",
                    b => b.WithOrigins("http://localhost:4200") // Assuming Angular runs on localhost:4200
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            //Serialization with NewtonsoftJSON
            builder.Services.AddControllers()
                 .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                     options.SerializerSettings.Converters.Add(new StringEnumConverter());
                 });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
                // Non-nullable reference are properly documented
                options.SupportNonNullableReferenceTypes();
                options.OperationFilter<AuthorizeOperationFilter>();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AngularClient");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
