using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.Migrations
{
    public class MigrationProductFolderPart : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public MigrationProductFolderPart(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(MoyskladProductFolderPart), builder => builder
                .Attachable()
                .WithDisplayName("Product Folder Part")
                .WithDescription("Moysklad Product Folder Part"));

            return 1;
        }
    }
}
