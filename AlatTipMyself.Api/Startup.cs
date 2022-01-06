using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.Extensions;
using AlatTipMyself.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api
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
            services.AddDbContext<TipMySelfContext>(options =>
            options.UseSqlServer(@"Server=tcp:tip-dev-sqlserver.database.windows.net,1433;Initial Catalog=tip-dev-sqldatabase;Persist Security Info=False;User ID=TIP2021;Password=TipProject20@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30")
            );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlatTipMyself.Api", Version = "v1" });
            });
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITipWalletService, TipWalletService>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors(options => {
                options.AddPolicy("sample", x => x.AllowAnyOrigin().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlatTipMyself.Api v1"));
            }

            app.ConfigureCustomMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("sample");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //PrepDb.PrepPopulation(app);
        }
    }
}
