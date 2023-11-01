using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Moysklad.Models;
using System;
using System.Threading.Tasks;

namespace OrchardCore.Moysklad.Settings
{
    public class MoyskladPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(MoyskladPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            return Initialize<MoyskladPartSettingsViewModel>("MoyskladPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<MoyskladPartSettings>();

                model.MySetting = settings.MySetting;
                model.MoyskladPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(MoyskladPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            var model = new MoyskladPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
            {
                context.Builder.WithSettings(new MoyskladPartSettings { MySetting = model.MySetting });
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
