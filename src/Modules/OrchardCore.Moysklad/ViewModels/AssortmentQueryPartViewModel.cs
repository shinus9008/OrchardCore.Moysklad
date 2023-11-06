using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.ViewModels
{
    public class AssortmentQueryPartViewModel
    {
        //TODO: Добавить!

        [BindNever]
        public ContentItem ContentItem { get; internal set; }

        [BindNever]
        public MoyskladAssortmentQueryPart AssortmentQueryPart { get; internal set; }
    }
}
