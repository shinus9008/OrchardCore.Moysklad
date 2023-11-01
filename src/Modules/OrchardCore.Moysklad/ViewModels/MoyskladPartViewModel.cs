using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.Moysklad.Models;
using OrchardCore.Moysklad.Settings;

namespace OrchardCore.Moysklad.ViewModels
{
    public class MoyskladPartViewModel
    {
        public string MySetting { get; set; }

        public bool Show { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public MoyskladPart MoyskladPart { get; set; }

        [BindNever]
        public MoyskladPartSettings Settings { get; set; }
    }
}
