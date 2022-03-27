using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chapter.WebApi.Controllers
{
    [Produces("aplication/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            //trata o email e senha se existem no banco de dados
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(login.email, login.senha);

                if (usuarioBuscado == null)
                {
                    //return NotFound("E-mail e/ou senha inválidos");
                    //foi criado um método de teste para validar usuarioBuscado
                    return Unauthorized(new { msg = "E-mail e/ou senha inválidos" });
                }

                //Conteudo que passamos para dentro do corpo do token
                var minhasClaims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo.ToString())
                };
                //Variavel que guarda a chave que vai dentro da credencial(cred)
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));
                
                //Cria a credencial passando a chave de acesso(key)
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Criando o token e passando as informações que ele irá conter
                var meuToken = new JwtSecurityToken(
                    issuer: "chapter.webApi",
                    audience: "chapter.webApi",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: cred
                 );

                //Retorna o valor do token criado
                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken),
                    }
                    
                    );

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}
