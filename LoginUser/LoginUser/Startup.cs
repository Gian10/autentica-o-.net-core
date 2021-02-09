using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoginUser.Data;
using Newtonsoft.Json.Serialization;
using LoginUser.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LoginUser.Helpers;

namespace LoginUser
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        

            services.AddDbContext<LoginUserContext>(options =>
           options.UseMySql(Configuration.GetConnectionString("LoginUserContext"), builder =>
           builder.MigrationsAssembly("LoginUser")));

            services.AddMvc()
              .AddJsonOptions(options => options.SerializerSettings.ContractResolver
                                        = new DefaultContractResolver());

            services.AddScoped<UserService>();
            services.AddScoped<LoginService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // injeção do Middleware personalizado
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
