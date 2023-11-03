using OrchardCore.Security.Permissions;

namespace OrchardCore.Moysklad
{
    public class Permissions : IPermissionProvider
    {
        public static Permission ManageMoyskladSettings =
            new Permission("ManageMoyskladSettings", "Manage Moysklad Settings");

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageMoyskladSettings },
                }
            };
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageMoyskladSettings,
            }
            .AsEnumerable());
        }
    }
}
