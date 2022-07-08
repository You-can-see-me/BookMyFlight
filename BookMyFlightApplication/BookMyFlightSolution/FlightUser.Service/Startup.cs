using Common;
using FlightUser.Service.EntityModels;
using FlightUser.Service.Repository;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightUser.Service
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
            //services.AddMassTransit(x => {
            //    x.AddConsumer<Admin.Service.Controllers>();
            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            //    {
            //        config.UseHealthCheck(provider);
            //        config.Host(new Uri("rabbitmq://localhost/"), h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });
            //        config.ReceiveEndpoint("ticketQueue", ep => {
            //            //ep.PrefetchCount = 16;
            //            //ep.UseMessageRetry(r => r.Interval(2, 100));
            //            ep.ConfigureConsumer<TicketConsumer.Microservice.Consumers.TicketConsumer>(provider);
            //        });
            //    }));
            //});
            //services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddScoped<IFlightBookingRepository, FlightBookingRepository>();
            services.AddSwaggerGen();
            services.AddDbContext<BookMyFlightDBContext>(x => x.UseSqlServer(Configuration.GetConnectionString("BookMyFlightDBConnection")));
            services.AddConsulConfig(Configuration);
            services.AddTransient<IJWTMangerRepository, JWTMangerRepository>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul(Configuration);
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();

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
