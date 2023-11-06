using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.Migrations
{
    public class MigrationAssortmentQueryPart : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public MigrationAssortmentQueryPart(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(MoyskladAssortmentQueryPart), builder => builder
                .Attachable()
                .WithDisplayName("Assortment Query Part")
                .WithDescription("Moysklad Assortment Query Part"));

            return 1;
        }
    }
}
