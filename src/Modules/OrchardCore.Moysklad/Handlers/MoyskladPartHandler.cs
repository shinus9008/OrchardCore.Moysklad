using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Moysklad.Models;
using System.Threading.Tasks;

namespace OrchardCore.Moysklad.Handlers
{
    public class MoyskladPartHandler : ContentPartHandler<MoyskladPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, MoyskladPart part)
        {
            part.Show = true;

            return Task.CompletedTask;
        }
    }
}