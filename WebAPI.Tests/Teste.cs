using Core.Input;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WebAPI.Interface;

namespace WebAPI.Tests
{
    [Binding]
    public class Teste
    {
        [Given(@"que o usuário preencheu todos os campos obrigatórios corretamente")]
        public void GivenQueOUsuarioPreencheuTodosOsCamposObrogatoriosCorretamente()
        {
            // Arrange
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AddUsuario(It.IsAny<UsuarioInput>())).Returns("Usuário adicionado com sucesso");
            var usuarioService = mockUsuarioService.Object;
            // Act
            string resultado = usuarioService.AddUsuario(new UsuarioInput { Email = "ju@gmail.com", Nome = "ju.hernandesmh@gmail", NivelAcessoId = 1, Senha = "123" });
            // Assert
            Assert.Equal("Usuário adicionado com sucesso", resultado);
        }
}   }
