using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.Migrations
{
    public class MigrationProductPart : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public MigrationProductPart(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(MoyskladProductPart), builder => builder
                .Attachable()
                .WithDisplayName("Product Part")
                .WithDescription("Moysklad Product Part"));

            return 1;
        }
    }
}
