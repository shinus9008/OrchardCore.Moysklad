using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Moysklad.Models;
using OrchardCore.Moysklad.Settings;
using OrchardCore.Moysklad.ViewModels;
using System.Threading.Tasks;

namespace OrchardCore.Moysklad.Drivers
{
    public class MoyskladPartDisplayDriver : ContentPartDisplayDriver<MoyskladPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public MoyskladPartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(MoyskladPart part, BuildPartDisplayContext context)
        {
            return Initialize<MoyskladPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content:10")
                .Location("Summary", "Content:10")
                ;
        }

        public override IDisplayResult Edit(MoyskladPart part, BuildPartEditorContext context)
        {
            return Initialize<MoyskladPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Show = part.Show;
                model.ContentItem = part.ContentItem;
                model.MoyskladPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(MoyskladPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);

            return Edit(model);
        }

        private static void BuildViewModel(MoyskladPartViewModel model, MoyskladPart part, BuildPartDisplayContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<MoyskladPartSettings>();

            model.ContentItem = part.ContentItem;
            model.MySetting = settings.MySetting;
            model.Show = part.Show;
            model.MoyskladPart = part;
            model.Settings = settings;
        }
    }
}
