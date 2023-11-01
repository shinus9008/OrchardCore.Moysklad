using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCore.Moysklad.Settings
{
    public class MoyskladPartSettingsViewModel
    {
        public string MySetting { get; set; }

        [BindNever]
        public MoyskladPartSettings MoyskladPartSettings { get; set; }
    }
}
