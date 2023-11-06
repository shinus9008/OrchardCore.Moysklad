using Confiti.MoySklad.Remap.Entities;

namespace OrchardCore.Moysklad.ViewModels
{
    public class ProductFolderListViewModel
    {
        public required IReadOnlyList<ProductFolder> ProductFolders { get; init; }
    }
}
