using OrchardCore.Security.Permissions;

namespace OrchardCore.Moysklad
{
    public class Permissions : IPermissionProvider
    {
        public static Permission ManageMoyskladSettings { get; } =
            new Permission("ManageMoyskladSettings", "Manage Moysklad Settings");

        #region Access_To_Moysklad_Api

        /// <summary>
        /// Provides access to Assortment Api
        /// </summary>
        public static Permission AccessToAssortmentApi { get; } =
            new Permission("AccessToAssortmentApi", "Provides access to Assortment Api");

        /// <summary>
        /// Provides access to Product Folder Api
        /// </summary>
        public static Permission AccessToProductFolderApi { get; } =
            new Permission("AccessToProductFolderApi", "Provides access to Product Folder Api");

        #endregion

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] 
                    { 
                        ManageMoyskladSettings,

                        // Moysklad API
                        AccessToAssortmentApi,
                        AccessToProductFolderApi
                    },
                }
            };
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageMoyskladSettings,

                // Moysklad API
                AccessToAssortmentApi,
                AccessToProductFolderApi,
            }
            .AsEnumerable());
        }
    }
}
