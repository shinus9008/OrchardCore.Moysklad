using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Moysklad.Models;
using OrchardCore.Moysklad.ViewModels;

namespace OrchardCore.Moysklad.Drivers
{
    public class AssortmentQueryPartDisplayDriver : ContentPartDisplayDriver<MoyskladAssortmentQueryPart>
    {
        private readonly IStringLocalizer T;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        public AssortmentQueryPartDisplayDriver(
            IStringLocalizer<AssortmentQueryPartDisplayDriver> stringLocalizer, 
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            T = stringLocalizer;
        }

        public override async Task<IDisplayResult> DisplayAsync(
            MoyskladAssortmentQueryPart part, 
            BuildPartDisplayContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.AccessToAssortmentApi))
            {
                return null!;
            }

            return 
                Initialize<MoyskladAssortmentQueryPartViewModel>(GetDisplayShapeType(context), model =>
                {
                    model.ProductFolder = part.ProductFolder;

                    //
                    model.AssortmentQueryPart = part;
                    model.ContentItem         = part.ContentItem;
                })
                .Location("Detail",       "Content:10")
                .Location("Summary",      "Content:10")
                .Location("SummaryAdmin", "Actions:20");
        }

        public override async Task<IDisplayResult> EditAsync(
            MoyskladAssortmentQueryPart part, 
            BuildPartEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.AccessToAssortmentApi))
            {
                return null!;
            }

            return
                Initialize<MoyskladAssortmentQueryPartViewModel>(GetEditorShapeType(context), model =>
                {
                    model.ProductFolder = part.ProductFolder;

                    //
                    model.AssortmentQueryPart = part;
                    model.ContentItem         = part.ContentItem;
                });
        }

        public override async Task<IDisplayResult> UpdateAsync(
            MoyskladAssortmentQueryPart part, 
            IUpdateModel updater, 
            UpdatePartEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.AccessToAssortmentApi))
            {
                return null!;
            }

            await updater.TryUpdateModelAsync(part, Prefix, t => t.ProductFolder);

            return Edit(part);
        }
    }
}
