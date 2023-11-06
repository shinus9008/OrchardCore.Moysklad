using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.ViewModels
{
    public class MoyskladAssortmentQueryPartViewModel
    {
        //TODO:  Проблема с кодировками!?
        public string? ProductFolder { get; set; }


        [BindNever]
        public ContentItem ContentItem { get; internal set; }

        [BindNever]
        public MoyskladAssortmentQueryPart AssortmentQueryPart { get; internal set; }
    }
}
