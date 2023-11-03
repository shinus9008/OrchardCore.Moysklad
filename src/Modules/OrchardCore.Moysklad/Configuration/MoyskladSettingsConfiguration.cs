using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCore.Moysklad.Configuration
{
    public class MoyskladSettingsConfiguration : IConfigureOptions<MoyskladSettings>
    {
        private readonly ISiteService _site;

        public MoyskladSettingsConfiguration(ISiteService site)
        {
            _site = site;
        }

        public void Configure(MoyskladSettings options)
        {
            var settings = _site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<MoyskladSettings>();

            options.Credentials = settings.Credentials;


            //TODO: Decrypt the password
        }
    }
}
