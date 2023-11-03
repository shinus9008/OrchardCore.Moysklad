using Microsoft.Extensions.Localization;
using OrchardCore.Moysklad.Drivers;
using OrchardCore.Navigation;

namespace OrchardCore.Moysklad
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
        {
            T = stringLocalizer;
        }


        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(T["Security"], security => security
                    .Add(T["Settings"], settings => settings
                        .Add(T["Moysklad"], T["Moysklad"].PrefixPosition(), registration => registration
                            .Permission(Permissions.ManageMoyskladSettings)
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = MoyskladSettings_Credentials_DisplayDriver.GroupId })
                            .LocalNav()
                        )));


            return Task.CompletedTask;
        }
    }
}
