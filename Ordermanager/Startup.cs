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
using Ordermanager.Api.DependencyInjection;
using Ordermanager.Bll;
using Ordermanager.Dal;
using Ordermanager.Dal.Dal.CommodityDal;
using Ordermanager.Dal.HotelDal;
using Ordermanager.Dal.OrderDal;
using Ordermanager.Model;
using Hotel = Ordermanager.Model.Hotel;
using OrderBll = Ordermanager.Bll.OrderBll;
using UserBll = Ordermanager.Bll.UserBll;

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

            //把几个dal和bll一起注入
            services.BatchInjection();

            ////注册jwt服务
            var token = Configuration.GetSection("tokenConfig").Get<TokenManagement>();

            //启用JWT
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
