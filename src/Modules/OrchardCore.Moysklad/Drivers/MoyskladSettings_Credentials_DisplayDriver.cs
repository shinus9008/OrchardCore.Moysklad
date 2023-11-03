using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Moysklad.Configuration;
using OrchardCore.Moysklad.ViewModels;
using OrchardCore.Settings;

namespace OrchardCore.Moysklad.Drivers
{
    public class MoyskladSettings_Credentials_DisplayDriver : SectionDisplayDriver<ISite, MoyskladSettings>
    {
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        public const string GroupId = "Moysklad";

        private readonly IHttpContextAccessor _hca;
        private readonly IAuthorizationService _authorizationService;

        public MoyskladSettings_Credentials_DisplayDriver(IHttpContextAccessor hca,
                                             IAuthorizationService authorizationService,
                                             IShellHost shellHost,
                                             ShellSettings shellSettings)
        {
            _hca = hca;
            _authorizationService = authorizationService;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
        }

        public override async Task<IDisplayResult> EditAsync(MoyskladSettings section, BuildEditorContext context)
        {
            var user = _hca.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageMoyskladSettings))
            {
                return null; //TODO: Что делать тут?
            }

            return 
                Initialize<MoyskladSettings_Credentials_ViewModel>("MoyskladSettings_Credentials_Edit", model =>
                {
                    if (section.Credentials != null)
                    {
                        model.Username = section.Credentials.Username;
                        model.Password = section.Credentials.Password;
                    }

                    
                })
                .Location("Content")
                .OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(MoyskladSettings section, BuildEditorContext context)
        {
            var user = _hca.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageMoyskladSettings))
            {
                return null;
            }

            if (context.GroupId.Equals(GroupId, StringComparison.OrdinalIgnoreCase))
            {
                var model = new MoyskladSettings_Credentials_ViewModel();

                if (await context.Updater.TryUpdateModelAsync(model, Prefix))
                {
                    if (section.Credentials == null)
                        section.Credentials = new Confiti.MoySklad.Remap.Client.MoySkladCredentials();

                    section.Credentials.Username = model.Username?.Trim();
                    section.Credentials.Password = model.Password?.Trim();

                    // Release the tenant to apply settings.
                    await _shellHost.ReleaseShellContextAsync(_shellSettings);
                }
            }

            return await EditAsync(section, context);
        }
    }
}
