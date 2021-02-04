using System.Reflection;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using MediatR;
using CosmosDbExample.Settings;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Commands.AddNewUser;
using CosmosDbExample.Cqrs.Handlers.Commands.AddNewArticle;
using CosmosDbExample.Cqrs.Handlers.Commands.AddNewSubscriber;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllUsers;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllArticles;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllSubscribers;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleUser;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleArticle;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleSubscriber;

namespace CosmosDbExample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration AConfiguration)
        {
            Configuration = AConfiguration;
        }

        public void ConfigureServices(IServiceCollection AServices)
        {
            AServices.AddMvc(AOption => AOption.CacheProfiles
            .Add("Standard", new CacheProfile()
            {
                Duration = 10,
                Location = ResponseCacheLocation.Any,
                NoStore = false
            }));

            AServices.AddMvc(AOption => AOption.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            AServices.AddControllers();
            AServices.AddSingleton(Configuration.GetSection("CosmosDb").Get<CosmosDbSettings>());
            AServices.AddScoped<ICosmosDbService, CosmosDbService>();

            AServices.AddMediatR(Assembly.GetExecutingAssembly());
            AServices.AddTransient<IRequestHandler<AddNewUserCommand, Unit>, AddNewUserCommandHandler>();
            AServices.AddTransient<IRequestHandler<AddNewArticleCommand, Unit>, AddNewArticleCommandHandler>();
            AServices.AddTransient<IRequestHandler<AddNewSubscriberCommand, Unit>, AddNewSubscriberCommandHandler>();
            AServices.AddTransient<IRequestHandler<GetAllArticlesQuery, IEnumerable<Articles>>, GetAllArticlesQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<Users>>, GetAllUsersQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetAllSubscribersQuery, IEnumerable<Subscribers>>, GetAllSubscribersQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetSingleArticleQuery, Articles>, GetSingleArticleQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetSingleUserQuery, Users>, GetSingleUserQueryHandler>();
            AServices.AddTransient<IRequestHandler<GetSingleSubscriberQuery, Subscribers>, GetSingleSubscriberQueryHandler>();

            AServices.AddResponseCompression(AOptions =>
            {
                AOptions.Providers.Add<GzipCompressionProvider>();
            });

            AServices.AddSwaggerGen(AOption =>
            {
                AOption.SwaggerDoc("v1", new OpenApiInfo { Title = "Cosmos DB Example", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder AApplication, IWebHostEnvironment AEnvironment)
        {
            if (AEnvironment.IsDevelopment())
            {
                AApplication.UseDeveloperExceptionPage();
            }

            AApplication.UseSwagger();
            AApplication.UseSwaggerUI(AOption =>
            {
                AOption.SwaggerEndpoint("/swagger/v1/swagger.json", "Cosmos DB Example version 1");
            });

            AApplication.UseResponseCompression();
            AApplication.UseStaticFiles();
            AApplication.UseRouting();

            AApplication.UseEndpoints(AEndpoints =>
            {
                AEndpoints.MapControllers();
            });
        }
    }
}
