using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Moysklad.Constants;
using OrchardCore.Moysklad.Models;

namespace OrchardCore.Moysklad.Migrations
{
    public class MigrationAssortmentQuery : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public MigrationAssortmentQuery(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterTypeDefinition(DefineContentType.MoyskladAssortmentQuery, type => type
                .Securable()
                .DisplayedAs("Assortment Query")
                .WithDescription("Moysklad Assortment Query")
                .WithPart(DefineContentPart.TitlePart)
                .WithPart(nameof(MoyskladAssortmentQueryPart)));

            return 1;
        }
    }
}
