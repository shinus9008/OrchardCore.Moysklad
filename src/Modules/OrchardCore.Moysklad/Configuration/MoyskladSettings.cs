using Confiti.MoySklad.Remap.Client;

namespace OrchardCore.Moysklad.Configuration
{
    public class MoyskladSettings //TODO: IValidatableObject
    {
        public MoySkladCredentials Credentials { get; set; } = new MoySkladCredentials();
    }
}
