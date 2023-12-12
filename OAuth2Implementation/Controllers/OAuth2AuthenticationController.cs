﻿using Microsoft.AspNetCore.Mvc;
using OAuth2CoreLib.Exceptions;
using OAuth2CoreLib.Models;
using OAuth2CoreLib.RequestFields;
using OAuth2CoreLib.Services;
using OAuth2Implementation.RequestFields;

namespace OAuth2Implementation.Controllers
{
    [Route("oauth2/")]
    public class OAuth2AuthenticationController : Controller
    {
        private readonly IOAuth2Service oAuth2Service;
        public OAuth2AuthenticationController(IOAuth2Service oAuth2Service)
        {
            this.oAuth2Service = oAuth2Service;
        }

        [HttpPost("auth")]
        /// <summary>
        /// Так-как в данной реализации не поддерживаются сертификаты, используется логин-пароль
        /// Оно не находится в <see cref="OAuth2CoreLib.Controllers.OAuth2Controller"/> потому что
        /// Реализация зависит от требования сервиса.
        /// </summary>
        public IActionResult Auth(AuthRequest authRequest, string user_id, string? secret)
        {
            secret ??= string.Empty;
            User user = oAuth2Service.GetAuthenticatedUser(user_id, secret);
            if (user != null)
            {
                try
                {
                    return Ok(oAuth2Service.GenerateCode(authRequest, user));
                }
                catch (WrongClientException)
                {
                    return BadRequest("Client not found");
                }
                catch (WrongClientScopeException)
                {
                    return BadRequest("Problem with client scopes");

                }
                catch (WrongUserException)
                {
                    return BadRequest("Problem with user scopes");

                }
                catch (Exception)
                {

                    return BadRequest("unhandled exception raised");
                }
            }

            return BadRequest("user_id or secret is wrong");
        }

        /// <summary>
        /// Необходимо добавить защиту!!!
        /// </summary>
        /// <param name="signInRequest"></param>
        /// <returns></returns>
        [HttpPost("signin")]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            string result = oAuth2Service.AddUser(signInRequest.user_id, signInRequest.secret);

            if (String.IsNullOrEmpty(result))
            {
                return Ok("user added");
            }
            
            return BadRequest($"Sign In failed: {result}");
        }
    }
}
