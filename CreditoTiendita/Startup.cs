using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CreditoTiendita.Domain.Persistance.Context;
using CreditoTiendita.Domain.Repositories;
using CreditoTiendita.Domain.Services;
using CreditoTiendita.Extensions;
using CreditoTiendita.Persistance.Repositories;
using CreditoTiendita.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreditoTiendita
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
            services.AddCustomSwagger();
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
            });

            services.AddScoped<IFeeTypeRepository, FeeTypeRepository>();
            services.AddScoped<IFeeTypeService, FeeTypeService>();

            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IFeeService, FeeService>();

            services.AddScoped<IPeriodRepository, PeriodRepository>();
            services.AddScoped<IPeriodService, PeriodService>();

            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));
            services.AddRouting(opt => opt.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomSwagger();
        }
    }
}
