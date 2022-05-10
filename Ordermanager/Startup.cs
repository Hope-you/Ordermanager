using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ordermanager.Bll;
using Ordermanager.Dal;
using Ordermanager.Model;

namespace Ordermanager
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Swagger接口文档",
                    Version = "v1",
                    Description = "Core.WebApi HTTP API V1",
                });
                options.OrderActionsBy(o => o.RelativePath);
            });

            //services.AddBusiness();

            //注册DapperExtHelper
            services.AddScoped(typeof(IDapperExtHelper<>), typeof(DapperExtHelper<>));
            services.AddScoped<DapperExtHelper<User>>();
            //services.AddScoped(typeof(IDal<>), typeof(BaseDal<>));
            services.AddScoped<UserDal>();
            services.AddScoped<UserBll>();

            ////注册jwt服务
            var token = Configuration.GetSection("tokenConfig").Get<TokenManagement>();

            //启用JWT
            services.AddAuthentication(Options =>
                {
                    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = token.Issuer,
                        ValidAudience = token.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });


            //配置数据库连接字符串
            services.AddDbcontext(Configuration.GetConnectionString("Default"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/V1/swagger.json", "Webapi-V1");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            //认证
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
