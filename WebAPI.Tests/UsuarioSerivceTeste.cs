using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebAPI.Interface;
using Core.Input;
using Core.Entidade;

namespace WebAPI.Tests
{
    public class UsuarioSerivceTeste
    {
        [Fact(DisplayName = "Validação de criação de usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void Create_ShouldReturnSuccessMessage()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AddUsuario(It.IsAny<UsuarioInput>())).Returns("Usuário adicionado com sucesso");
            var usuarioService = mockUsuarioService.Object;
            // Act
            string resultado = usuarioService.AddUsuario(new UsuarioInput { Email = "ju@gmail.com", Nome = "ju.hernandesmh@gmail", NivelAcessoId = 1, Senha = "123" });
            // Assert
            Assert.Equal("Usuário adicionado com sucesso", resultado);
        }

        [Fact(DisplayName = "Validação de criação de usuário com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public void Create_ShouldReturnErrorMessage()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AddUsuario(It.IsAny<UsuarioInput>())).Returns("Erro ao adicionar usuário");
            var usuarioService = mockUsuarioService.Object;
            // Act
            string resultado = usuarioService.AddUsuario(new UsuarioInput { Email = "ju", Nome = "ju", NivelAcessoId = 3, Senha = "" });

            Assert.Equal("Erro ao adicionar usuário", resultado);

        }

        [Fact(DisplayName = "Validação de retorno do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void GetAll_ShouldReturnValueSuccess()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.GetAll(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>() )).ReturnsAsync(new List<Usuario>
                {
                    new Usuario
                    {
                        Id = 1,
                        Nome = "Juli",
                        Email = "juli@gmail.com",
                        Senha = "123",
                        NivelAcessoId = 1,
                        Data = DateTime.Now,
                        Jogo = new List<Jogo>()
                    }
                });

            var usuarioService = mockUsuarioService.Object;

            // Act
            var resultado = usuarioService.GetAll().Result;

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("Juli", resultado[0].Nome);
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID")]
        [Trait("Categoria", "Validação Usuário")]
        public void GetById_ShouldReturnValueSuccess()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.GetById(1)).ReturnsAsync(new Usuario
            {
                Id = 1,
                Nome = "Juli"

            });

            var usuario = mockUsuarioService.Object;

            var resultado = usuario.GetById(1).Result;

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task GetById_ShouldReturnValueError()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.GetById(3)).ThrowsAsync(new Exception("Usuário não encontrado"));
            var usuario = mockUsuarioService.Object;

            var resultado = usuario.GetById(3);

            var teste = await Assert.ThrowsAsync<Exception>(() => usuario.GetById(3));

            Assert.Equal("Usuário não encontrado", teste.Message);
        }

        [Fact(DisplayName = "Validação de atualização do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public async void Update_ShouldReturnSuccessMessage()
        {
            // Arrange
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.UpdateUsuario(It.IsAny<UsuarioUpdate>())).ReturnsAsync("Usuário atualizado com sucesso");
            var usuarioService = mockUsuarioService.Object;
            // Act
            var usuario = await usuarioService.UpdateUsuario(new UsuarioUpdate { Id = 1, Nome = "Juli", Email = "ju.hernandesmh@gmail.com", NivelAcessoId = 1, Senha = "123" });

            Assert.Equal("Usuário atualizado com sucesso", usuario);
        }

        [Fact(DisplayName = "Validação de deleção")]
        [Trait("Categoria", "Validação Usuário")]
        public void Delete_ShouldReturnDelete()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.DeleteUsuario(1)).ReturnsAsync("Deletado com sucesso");

            var usuarioService = mockUsuarioService.Object;

            var usuario = usuarioService.DeleteUsuario(1);

            Assert.Equal("Deletado com sucesso", usuario.Result);
        }
    }
}
