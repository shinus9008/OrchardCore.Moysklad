using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.Moysklad.Configuration;
using OrchardCore.Moysklad.Constants;
using OrchardCore.Moysklad.Models;
using System.Net;

namespace OrchardCore.Moysklad.Controllers
{
    /// <summary>
    /// Provides access to Assortment Api
    /// </summary>
    [Admin]    
    public class AssortmentController : Controller
    {
        private readonly MoyskladSettings _options;        
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentManager _contentManager;      

        public AssortmentController(
            IAuthorizationService authorizationService,
            IContentManager contentManager,         
            IOptions<MoyskladSettings> options)
        {
            _options = options.Value;          
            _authorizationService = authorizationService;
            _contentManager = contentManager;          
        }

        public async void Uploading() 
        {
            // Получаем API функцию
            var api = GetApi();

            // Получаем запрос
            var query = GetQueryFor(null);

            // TODO: Запрос постраничный!

            // ВЫполняем запрос
            var response = await api.GetAllAsync(query);

            // TODO: Проверка результатов!

            foreach (var assortment in response.Payload.Rows) 
            {
                if (assortment == null)
                {
                    continue;
                }

                //assortment.Product
            }
        }


        /// <summary>
        /// Создает запрос асортимена товаров из конкретной папки!
        /// </summary>  
        [HttpPost]        
        public async Task<IActionResult> CreateAsFolder(string hRef)
        {
            if (string.IsNullOrWhiteSpace(hRef))
            {
                return NotFound();
            }

            // Проверяем разрешение
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.AccessToAssortmentApi))
            {
                return this.ChallengeOrForbid();
            }

            // Создаем элемент контента
            var newContentItem = await _contentManager.NewAsync(DefineContentType.MoyskladAssortmentQuery);
            if (newContentItem == null)
            {
                return NotFound();
            }

            // Элемент контента должен содержать Query Part
            var queryPart = newContentItem.As<MoyskladAssortmentQueryPart>();
            if (queryPart == null)
            {
                return NotFound();
            }

            // Query Part Config
            queryPart.ProductFolder = hRef;
            newContentItem.Apply(queryPart);
            //TODO: Add Title part Config

            
            await _contentManager.CreateAsync(newContentItem, VersionOptions.Draft);

            return RedirectToAction("Edit", "Admin", new { area = "OrchardCore.Contents", contentItemId = newContentItem.ContentItemId });
        }

        /// <summary>
        /// Запрашивает асортимент товара и выводит результат в виде списка
        /// </summary> 
        [HttpGet]
        public async Task<IActionResult> List(string contentItemId)
        {
            // Проверяем аргументы
            if (contentItemId == null)
            {
                return NotFound();
            }

            // Проверяем разрешение
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.AccessToAssortmentApi))
            {
                return this.ChallengeOrForbid();
            }

            // Запрашиваем элемент контента
            var contentItem = await _contentManager.GetAsync(contentItemId, VersionOptions.Latest);
            if (contentItem == null)
            {
                return NotFound();
            }

            // Элемент контента должен содержать Query Part
            var queryPart = contentItem.As<MoyskladAssortmentQueryPart>();
            if (queryPart == null)
            {
                return NotFound();
            }

            // Создаем запрос
            var query = GetQueryFor(queryPart);
            if (query == null) 
            {
                return NotFound();
            }

            var api = GetApi();

            try
            {
                var response = await api.GetAllAsync(query);

                return View();
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

        private AssortmentApi GetApi()
        {
            var httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            var assortmentApi = new AssortmentApi(new HttpClient(httpClientHandler), _options.Credentials);




            return assortmentApi;
        }
        private AssortmentApiParameterBuilder? GetQueryFor(MoyskladAssortmentQueryPart queryPart)
        {
            if (queryPart.ProductFolder == null)
                return null;

            // Запрос на получение товаров и услуг
            var query = new AssortmentApiParameterBuilder();

            // Запрашиваем сортимент из папки!
            query.Parameter(x => x.ProductFolder).Should().Be(queryPart.ProductFolder);

            return query;
        }
    }
}
