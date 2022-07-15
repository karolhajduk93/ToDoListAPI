using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI
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
			services.Configure<ToDoListsDatabaseSettings>(
				Configuration.GetSection(nameof(ToDoListsDatabaseSettings)));

            //not sure about this
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSingleton<IToDoListsDatabaseSettings>(sp =>
				sp.GetRequiredService<IOptions<ToDoListsDatabaseSettings>>().Value);

			services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder
						.WithOrigins("https://localhost:5001", "http://localhost:5000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
						//.AllowCredentials()
                        .WithMethods("GET, PATCH, DELETE, PUT, POST, OPTIONS"));
            });

            services.AddSingleton<ToDoListService>();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoListAPI", Version = "v1"});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoListAPI v1"));
			}
			app.UseCors(policy =>
				policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
					.AllowAnyMethod()
					.AllowAnyHeader());
					

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
