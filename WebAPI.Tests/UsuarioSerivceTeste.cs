using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebAPI.Interface;
using Core.Input;
using Core.Entidade;
using Core.Output;
using Core.ValueObjects;

namespace WebAPI.Tests
{
    public class UsuarioSerivceTeste
    {
        [Fact(DisplayName = "Validação de criação de usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void Create_ShouldReturnSuccessMessage()
        {
         
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AdicionarUsuario(It.IsAny<UsuarioAdicionarRequisicao>(), It.IsAny<int>())).ReturnsAsync(ServiceResponse<UsuarioAdicionarResposta>.Ok(new UsuarioAdicionarResposta { Id = 1 }, "Usuário adicionado com sucesso"));
            var usuarioService = mockUsuarioService.Object;

            int nivelAcessoId = 1;
            var resultado = usuarioService.AdicionarUsuario(new UsuarioAdicionarRequisicao { Email = "ju@gmail.com", Nome = "ju.hernandesmh@gmail", Senha = "123" }, nivelAcessoId);

            Assert.True(resultado.Result.Success);
            Assert.Equal("Usuário adicionado com sucesso", resultado.Result.Message);
        }


        [Fact(DisplayName = "Validação de adicionar usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void Add_ShouldReturnSuccess()
        {
            var moackUsuario = new Mock<Usuario>();

            moackUsuario.Setup( u=> u.AdicionarUsuario(It.IsAny<UsuarioAdicionarRequisicao>(),It.IsAny<int>(), It.IsAny<string>())).Returns(new Usuario { Nome = "Juli", Email = new Email("juli@gmail.com"),  Senha = "123", NivelAcessoId = 1 });

            var usuario = moackUsuario.Object;
            var resultado = usuario.AdicionarUsuario(new UsuarioAdicionarRequisicao { Nome = "", Email = "", Senha = "" }, 1, "");

            Assert.Equal("Juli", resultado.Nome);
            Assert.Equal("123", resultado.Senha);
            Assert.Equal(1, resultado.NivelAcessoId);
        }

        [Fact(DisplayName = "Validação de atualização do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void Update_ShouldReturnSucess()
        {
            var moackUsuario = new Mock<Usuario>();

            moackUsuario.Setup(u => u.Atualizar(It.IsAny<Usuario>(), It.IsAny<UsuarioAtualizarRequisicao>(), It.IsAny<string>())).Callback<Usuario,UsuarioAtualizarRequisicao, string>((usuario, usuarioAntigo, nome) => 
            { 
                usuario.Nome = "Ju";
                usuario.Email = new Email("ju.hernandesmh@gmail.com");
                usuario.Senha = "123";
                usuario.NivelAcessoId =1;
            });

            var usuario = moackUsuario.Object;
            usuario.Atualizar(new Usuario { Nome = "Juli", Email = new Email("ju.hernandesmh@gmail.com"), Senha = "123", NivelAcessoId = 1 }, new UsuarioAtualizarRequisicao { Nome = "Juli", Email = "", Senha = "", NivelAcessoId = 1 }, "");
            
            moackUsuario.Verify(u => u.Atualizar(It.IsAny<Usuario>(), It.IsAny<UsuarioAtualizarRequisicao>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Validação de criação de usuário com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public void Create_ShouldReturnErrorMessage()
        {
           
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AdicionarUsuario(It.IsAny<UsuarioAdicionarRequisicao>(),It.IsAny<int>())).ReturnsAsync(ServiceResponse<UsuarioAdicionarResposta>.Fail("Erro ao adicionar usuário"));
            var usuarioService = mockUsuarioService.Object;

            var resultado = usuarioService.AdicionarUsuario(new UsuarioAdicionarRequisicao { Email = "ju", Nome = "ju", Senha = "" },3);

            Assert.False(resultado.Result.Success);
            Assert.Equal("Erro ao adicionar usuário", resultado.Result.Message);

        }

        [Fact(DisplayName = "Validação de retorno do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public void GetAll_ShouldReturnValueSuccess()
        {
          
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.Listar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>() )).ReturnsAsync(ServiceResponse<List<UsuarioListarResposta>>.Ok(new List<UsuarioListarResposta>
                {
                    new UsuarioListarResposta
                    {
                        Id = 1,
                        Nome = "Juli",
                        Email = "juli@gmail.com",
                        NivelAcesso = "Admin"
                    }
                }));

            var usuarioService = mockUsuarioService.Object;


            var resultado = usuarioService.Listar().Result;


            Assert.NotNull(resultado.Data);
            Assert.Single(resultado.Data);
            Assert.Equal("Juli", resultado.Data[0].Nome);
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID")]
        [Trait("Categoria", "Validação Usuário")]
        public void GetById_ShouldReturnValueSuccess()
        {
          
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.BuscarPorId(1)).ReturnsAsync(ServiceResponse<UsuarioBuscarPorIdResposta>.Ok(new Core.Output.UsuarioBuscarPorIdResposta
            {
                Id = 1,
                Nome = "Juli"

            }));

            var usuario = mockUsuarioService.Object;

            var resultado = usuario.BuscarPorId(1).Result;

            Assert.NotNull(resultado.Data);
            Assert.Equal(1, resultado.Data.Id);
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task GetById_ShouldReturnValueError()
        {
          
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.BuscarPorId(3)).ThrowsAsync(new Exception("Usuário não encontrado"));
            var usuario = mockUsuarioService.Object;

            var resultado = usuario.BuscarPorId(3);

            var teste = await Assert.ThrowsAsync<Exception>(() => usuario.BuscarPorId(3));

            Assert.Equal("Usuário não encontrado", teste.Message);
        }

        [Fact(DisplayName = "Validação de atualização do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public async void Update_ShouldReturnSuccessMessage()
        {
          
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AtualizarUsuario(It.IsAny<UsuarioAtualizarRequisicao>(),1)).ReturnsAsync(ServiceResponse.Ok("Usuário atualizado com sucesso"));
            var usuarioService = mockUsuarioService.Object;

            var usuario = await usuarioService.AtualizarUsuario(new UsuarioAtualizarRequisicao {Nome = "Juli", Email = "ju.hernandesmh@gmail.com", NivelAcessoId = 1, Senha = "123" },1);

            Assert.True(usuario.Success);
            Assert.Equal("Usuário atualizado com sucesso", usuario.Message);
        }

        [Fact(DisplayName = "Validação de deleção")]
        [Trait("Categoria", "Validação Usuário")]
        public void Delete_ShouldReturnDelete()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.RemoverUsuario(1)).ReturnsAsync(ServiceResponse.Ok("Deletado com sucesso"));

            var usuarioService = mockUsuarioService.Object;

            var usuario = usuarioService.RemoverUsuario(1);

            Assert.True(usuario.Result.Success);
            Assert.Equal("Deletado com sucesso", usuario.Result.Message);
        }
    }
}
