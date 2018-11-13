using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using LiderHD.CORE.Repository;
using LiderHD.CORE.Models;
using SmitAPI.Autenticate;
using SmitAPI.Models;

namespace LiderHD.Controllers
{
    [Route("api/[controller]")]
    public class LoginController
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]User usuario,[FromServices]UserRepository usersDAO,[FromServices]SigningConfigurations signingConfigurations,[FromServices]TokenConfigurations tokenConfigurations)
        {
            var usuarioBase = new User();
            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                usuarioBase = usersDAO.getUserByEmailPass(usuario);
                credenciaisValidas = (usuarioBase != null && usuario.Email == usuarioBase.Email && usuario.Pass == usuarioBase.Pass);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Email, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                    }
                );
                try
                {
                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = DateTime.Now.AddDays(365);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = "cdn31wb21rcIss",
                        Audience = "cdn31wb21rcAud",
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    return new
                    {
                        user = usuarioBase,
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        message = "OK"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        authenticated = false,
                        message = ex.InnerException
                    }; 
                }

            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }

}
