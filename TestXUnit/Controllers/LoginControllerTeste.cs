using Chapter.WebApi.Controllers;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestXUnit.Controllers
{
    public class LoginControllerTeste
    {
        [Fact]
        //Avalia se o usuário é inválido
        public void LoginController_Retornar_UsuarioInvalido()
        {
            //Arrange
            var repositorioFalso = new Mock<IUsuarioRepository>();
            //guarda os valores de e-mail e senha do login /.Returns = define que o retorno do metodo é nulo
            repositorioFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            //cria um usuario para teste
            LoginViewModel dadosUsuarios = new LoginViewModel();
            dadosUsuarios.email = "email@email.com";
            dadosUsuarios.senha = "1234";

            //cria variavel para chamar o controller
            var controller = new LoginController(repositorioFalso.Object);

            //Act
            var resultado = controller.Login(dadosUsuarios);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
            
        }

        [Fact]
        public void LoginController_Retornar_UsuarioValido()
        {
            //Pré-condição / Arrange
            string issuerValidacao = "chapter.webApi";

            //Arrange
            Usuario usuarioFake = new Usuario();
            usuarioFake.Email = "email@email.com";
            usuarioFake.Senha = "1234";

            var repositorioFalso = new Mock<IUsuarioRepository>();
            //guarda os valores de e-mail e senha do login /.Returns = define que o retorno do metodo é o objeto usuarioFake
            repositorioFalso.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioFake);

            var controller = new LoginController(repositorioFalso.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.email = "email@email.com";
            dadosUsuario.senha = "1234";

            //Act
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosUsuario);

            var token = resultado.Value.ToString().Split(' ')[3];

            var jstHandler = new JwtSecurityTokenHandler();

            var jwtToken = jstHandler.ReadJwtToken(token);

            //Assert
            Assert.Equal(issuerValidacao, jwtToken.Issuer);

        }
    }
}
