using Confiti.MoySklad.Remap.Entities;

namespace OrchardCore.Moysklad.ViewModels
{
    public class MoyskladProductFolderViewModel
    {
        public required IReadOnlyList<ProductFolder> ProductFolders { get; init; }
    }
}
