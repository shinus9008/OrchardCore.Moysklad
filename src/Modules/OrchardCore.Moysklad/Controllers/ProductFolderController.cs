using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Moysklad.Configuration;
using OrchardCore.Moysklad.ViewModels;
using System.Net;

namespace OrchardCore.Moysklad.Controllers
{
    /// <summary>
    /// Provides access to Product Folder Api
    /// </summary>
    [Admin]
    public class ProductFolderController : Controller
    {
        private readonly MoyskladSettings options;
        private readonly IAuthorizationService _authorizationService;

        public ProductFolderController(
            IOptions<MoyskladSettings> options,
            IAuthorizationService authorizationService)
        {
            this.options = options.Value;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Запрашивает со склада папки и отображет их
        /// </summary>   
        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Проверка разрешений!
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.AccessToProductFolderApi))
            {
                return Forbid();
            }

            var api = GetApi();

            try
            {
                var response = await api.GetAllAsync();

                var viewModel = new ProductFolderListViewModel()
                {
                    ProductFolders = response.Payload.Rows
                };

                return View(viewModel);
            }
            catch (ApiException ex)
            {
                // обработать код ошибки
                if (ex.ErrorCode == 404) 
                { 

                }

                // обработать ошибки
                foreach (var error in ex.Errors) 
                { 

                }


                // полное описание ошибки
                // cодержит все коды/описания по каждой ошибке из ex.Errors.
                //_logger.Log(ex.Message);

                throw ex;
            }
        }

        //TODO: Через внедрение зависимостей!
        private ProductFolderApi GetApi()
        {
            var httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            var api = new ProductFolderApi(new HttpClient(httpClientHandler), options.Credentials);

            return api;
        }
    }
}
