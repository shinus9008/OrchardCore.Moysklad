using YesSql.Indexes;

namespace OrchardCore.Moysklad.Indexes
{
    public class MoyskladAssortmentQueryPartIndex : MapIndex
    {
        public required string ContentItemId { get; init; }
    }
}
