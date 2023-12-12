using Microsoft.AspNetCore.Mvc;
using OAuth2CoreLib.Models;
using OAuth2CoreLib.RequestFields;
using OAuth2CoreLib.Services;
using OAuth2Implementation.RequestFields;

namespace OAuth2Implementation.Controllers
{
    [Route("dev/")]
    public class DevController : Controller
    {
        private readonly IOAuth2Service oAuth2Service;

        public DevController(IOAuth2Service oAuth2Service)
        {
            this.oAuth2Service = oAuth2Service;
        }

        /// <summary>
        /// Тут нет никакой защиты так-как это эндпоинты с помощью которого можно настроить сервер
        /// для тестирования
        /// </summary>
        /// <param name="mcsRequest"></param>
        /// <returns></returns>
        [HttpPost("modify_client")]
        public IActionResult ModifyClient(ModifyClientAndScopesRequest mcsRequest)
        {
            Client client = oAuth2Service.CreateOrModifyClient(
                mcsRequest.client_id,
                mcsRequest.secret,
                mcsRequest.enabled,
                mcsRequest.scopes != null? mcsRequest.scopes.Split(" "): new string[0]);
            
            return Json(client);
        }

        [HttpPost("modify_user")]
        public IActionResult ModifyUser(ModifyUserAndScopesRequest musRequest)
        {
            User user = oAuth2Service.CreateOrModifyUser(
                musRequest.user_id,
                musRequest.secret,
                musRequest.scopes != null ? musRequest.scopes.Split(" ") : new string[0]);
            return Json(user);
        }
    }
}
