using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Moysklad.Configuration;
using OrchardCore.Moysklad.Controllers;
using OrchardCore.Moysklad.Drivers;
using OrchardCore.Moysklad.Handlers;
using OrchardCore.Moysklad.Migrations;
using OrchardCore.Moysklad.Models;
using OrchardCore.Moysklad.Settings;
using OrchardCore.Moysklad.ViewModels;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;

namespace OrchardCore.Moysklad
{
    public class Startup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            //
            services.AddScoped<IDisplayDriver<ISite>, MoyskladSettings_Credentials_DisplayDriver>();

            // Options:
            services.AddTransient<IConfigureOptions<MoyskladSettings>, MoyskladSettingsConfiguration>();

            // Content Parts
            services.AddContentPart<MoyskladAssortmentQueryPart>()
                    .UseDisplayDriver<AssortmentQueryPartDisplayDriver>();

            services.AddContentPart<MoyskladProductFolderPart>();
            services.AddContentPart<MoyskladProductPart>();

            // Migrations:
            services.AddDataMigration<MigrationAssortmentQuery>();
            services.AddDataMigration<MigrationAssortmentQueryPart>();
            services.AddDataMigration<MigrationProductFolderPart>();
            services.AddDataMigration<MigrationProductPart>();







            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<MoyskladPartViewModel>();
            });

            services.AddContentPart<MoyskladPart>()
                .UseDisplayDriver<MoyskladPartDisplayDriver>()
                .AddHandler<MoyskladPartHandler>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, MoyskladPartSettingsDisplayDriver>();
            services.AddDataMigration<Migration>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            

            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "OrchardCore.Moysklad",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );



            var assortmentControllerName = typeof(AssortmentController).ControllerName();


            routes.MapAreaControllerRoute(
                name: "AssortmentCreate",
                areaName: "OrchardCore.Moysklad",
                pattern: _adminOptions.AdminUrlPrefix + "/Assortment/CreateAsFolder",
                defaults: new { controller = assortmentControllerName, action = nameof(AssortmentController.CreateAsFolder) });


            routes.MapAreaControllerRoute(
                name: "AssortmentList",
                areaName: "OrchardCore.Moysklad",
                pattern: _adminOptions.AdminUrlPrefix + "/Assortment/List/{id}",
                defaults: new { controller = assortmentControllerName, action = nameof(AssortmentController.List) });
        }
    }
}
