using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft;
using InstrumentTracking.Infrastructure;

using InstrumentTracking.Infrastructure.Repositories;
using InstrumentTracking.Services;
using InstrumentTracking.Services.Implementations;
using Microsoft.OpenApi.Models;
using InstrumentTracking.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace InstrumentsTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IRequestRepository), typeof(RequestRepository));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IEngineerRepository), typeof(EngineerRepository));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IEquipmentRepository), typeof(EquipmentRepository));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.ISamplingActsRepository), typeof(SamplingActsRepository));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(InstrumentTracking.Domain.Interfaces.IRequestHistoryRepository), typeof(RequestsHistoryRepository));

            services.AddScoped<IEngineerService, EngineerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<ISamplingActService, SamplingActService>();
            services.AddScoped<IRequestHistoryService, RequestsHistoryService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "INST", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Here enter JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

	        //Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
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

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });

            services.AddControllers();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuartzProjcect v1"));
            }

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
