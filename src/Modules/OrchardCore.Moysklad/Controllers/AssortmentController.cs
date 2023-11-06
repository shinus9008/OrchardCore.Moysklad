using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Moysklad.Configuration;
using OrchardCore.Moysklad.Constants;
using OrchardCore.Moysklad.Models;
using System.Net;
using YesSql;

namespace OrchardCore.Moysklad.Controllers
{
    /// <summary>
    /// Provides access to Assortment Api
    /// </summary>
    [Admin]
    public class AssortmentController : Controller
    {
        private readonly MoyskladSettings _options;
        private readonly YesSql.ISession _session;
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;

        public AssortmentController(
            IAuthorizationService authorizationService,
            IContentManager contentManager,
            ISession session,
            IOptions<MoyskladSettings> options,
            IContentItemDisplayManager contentItemDisplayManager,
            IUpdateModelAccessor updateModelAccessor)
        {
            _options = options.Value;
            _session = session;
            _authorizationService = authorizationService;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _updateModelAccessor = updateModelAccessor;
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

        
        



        public async Task<IActionResult> CreateQuery(string hRef)
        {
            // Permissions:
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.AccessToAssortmentApi))
            {
                return this.ChallengeOrForbid();
            }

            if(!ModelState.IsValid)
            {
                //?
                return Forbid();
            }
            

            var contentItem = await _contentManager.NewAsync(DefineContentType.MoyskladAssortmentQuery);

            // Добавляем обертку
            // Добавляем обертку
            dynamic model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);


            return View(model);
        }





        /// <summary>
        /// Запрашивает асортимент товара и выводит результат в виде списка
        /// </summary>        
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
