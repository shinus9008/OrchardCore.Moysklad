using OrchardCore.ContentManagement;
using OrchardCore.Moysklad.Models;
using YesSql.Indexes;

namespace OrchardCore.Moysklad.Indexes
{
    public class MoyskladAssortmentQueryPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) => context.For<MoyskladAssortmentQueryPartIndex>().Map(contentItem =>
        {
            // Если элемент контента не содержит QueryPart, то индексацию не производим!
            var personPart = contentItem.As<MoyskladAssortmentQueryPart>();
            if (personPart == null)
                return null!;

            return new MoyskladAssortmentQueryPartIndex
            {
                ContentItemId = contentItem.ContentItemId
            };
        });
    }
}
