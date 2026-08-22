using Core.Entidade;
using Core.Input;
using Core.Output;
using Core.Repository;
using Infra.Exceptions;
using Infra.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Interface;
using WebAPI.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebAPI.Tests
{
    public class UsuarioSerivceTeste
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepository;
        private readonly Mock<ILogger<UsuarioService>> _logger;
        private readonly UsuarioService _serviceUsuario;
        private readonly Mock<UsuarioAdicionarRequisicao> _usuarioAdicionarRequisicao;
        public UsuarioSerivceTeste()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _logger = new Mock<ILogger<UsuarioService>>();
            _serviceUsuario = new UsuarioService(_usuarioRepository.Object, _logger.Object);
        }

        [Fact(DisplayName = "Validação de criação de usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Create_ShouldReturnSuccessMessage()
        {/*
         
            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(service => service.AdicionarUsuario(It.IsAny<UsuarioAdicionarRequisicao>(), It.IsAny<int>())).ReturnsAsync(ServiceResponse<UsuarioAdicionarResposta>.Ok(new UsuarioAdicionarResposta { Id = 1 }, "Usuário adicionado com sucesso"));
            var usuarioService = mockUsuarioService.Object;

            var mock = _usuarioRepository.Setup(repo => repo.ValidaEmail(It.IsAny<string>())).ReturnsAsync((Usuario?)null);

                var usuarioRequisicao = _usuarioAdicionarRequisicao.Setup(f => new UsuarioAdicionarRequisicao
                {
                    Nome = "Juliana",
                    Email = "ju.hernandesmh@gmail.com",
                    Senha = "203040#@!As"
                });*/

            var usuarioRequisicao = new Usuario
            {
                Nome = "Juliana",
                Email = "ju.hernandesmh@gmail.com",
                Senha = "203040#@!Asd"
            };

            _usuarioRepository.Setup(repo => repo.Add(usuarioRequisicao)).CallBase();

            var retorno = await _serviceUsuario.AdicionarUsuario(new UsuarioAdicionarRequisicao { Email = "julianamenezeshernandes@gmail.com", Nome = "Juliana", Senha = "203040#@!Asd" }, 1);

            //  int nivelAcessoId = 1;
            //    var resultado = usuarioService.AdicionarUsuario(new UsuarioAdicionarRequisicao { Email = "ju@gmail.com", Nome = "ju.hernandesmh@gmail", Senha = "123" }, nivelAcessoId);

            Assert.True(retorno.Success);
            Assert.Equal("Usuário adicionado com sucesso", retorno.Message);
        }

        [Fact(DisplayName = "Validação de criação de usuário com e-mail já existente")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Create_ShouldReturnErrorMessageForExistingEmail()
        {
            var usuarioRequisicao = new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "ju.hernandesmh@gmail.com",
                Senha = "203040#@!Asd"
            };

            _usuarioRepository.Setup(repo => repo.ValidaEmail(usuarioRequisicao.Email)).ReturnsAsync(new Usuario { Email = usuarioRequisicao.Email });

            Assert.ThrowsAsync<ExceptionEmailCadastrado>(() => _serviceUsuario.AdicionarUsuario(usuarioRequisicao, 1));
        }


        [Fact(DisplayName = "Validação em senha vazia")]
        [Trait("Categoria", "Validação Usuário")]
        public void Validator_ShouldReturnPasswordEmptyErrorMessage()
        {
            var usuarioRequisicao = new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "juli@gmail.com",
                Senha = ""
            };

            Assert.ThrowsAsync<ExceptionSenhaInvalida>(() => _serviceUsuario.AdicionarUsuario(usuarioRequisicao, 1));

        }

        [Fact(DisplayName = "Validação de teste de atualização do usuário")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Update_ShouldReturnSucess()
        {
            var usuarioAtualizarRequisicao = new UsuarioAtualizarRequisicao
            {
                Email = "ju.menezes@gmail.com",
                Senha = "123",
                NivelAcessoId = 1,
                Nome = "Juli"
            };

            _usuarioRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(new Usuario { Email = "", Nome = "" });

            var retorno = await _serviceUsuario.AtualizarUsuario(usuarioAtualizarRequisicao, 1);

            Assert.True(retorno.Success);
            Assert.Equal("Usuário atualizado com sucesso", retorno.Message);
        }

        [Fact(DisplayName = "Validação de atualização do usuário com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Update_ShouldReturnErrorMessage()
        {
            var usuarioAtualizarRequisicao = new UsuarioAtualizarRequisicao
            {
                Email = "",
                Senha = "",
                Nome = ""
            };

            _usuarioRepository.Setup(repo => repo.GetById(3)).ReturnsAsync((Usuario?)null);

            Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.AtualizarUsuario(usuarioAtualizarRequisicao, 1));
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task GetById_ShouldReturnValueSuccess()
        {
            _usuarioRepository.Setup(f => f.BuscarUsuarioPorId(1)).ReturnsAsync(new UsuarioBuscarPorIdResposta { Email = "ju.hernandesmh@gmail.com", Nome = "Juliana" });

            var resultado = await _serviceUsuario.BuscarPorId(1);

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.Nome);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.Email);
        }

        [Fact(DisplayName = "Validação de retorno do usuário por ID com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task GetById_ShouldReturnValueError()
        {

            _usuarioRepository.Setup(f => f.BuscarUsuarioPorId(3)).ReturnsAsync((UsuarioBuscarPorIdResposta?)null);

            var resultado = _serviceUsuario.BuscarPorId(3);

            Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.BuscarPorId(3));
        }

        [Fact(DisplayName = "Validação de deleção")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Delete_ShouldReturnDelete()
        {
            _usuarioRepository.Setup(f => f.GetById(3)).ReturnsAsync(new Usuario { Id = 3, Nome = "Juliana", Email = "ju.hernandesmh@gmail.com" });

            var resultado = await _serviceUsuario.RemoverUsuario(3);

            Assert.True(resultado.Success);
            Assert.Equal("Deletado com sucesso", resultado.Message);
        }

        [Fact(DisplayName = "Validação de deleção com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Delete_ShouldReturnError()
        {
            _usuarioRepository.Setup(f => f.GetById(3)).ReturnsAsync((Usuario?)null);

            Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.RemoverUsuario(3));
        }

        [Fact(DisplayName = "Validação buscar autenticado")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Search_ShouldReturnUser()
        {
            _usuarioRepository.Setup(f => f.BuscarUsuarioPorEmail("ju.hernandesmh@gmail.com")).ReturnsAsync(new UsuarioBuscarAutenticadoResposta { Email = "ju.hernandesmh@gmail.com", Nome = "Juliana" });

            var resultado = await _serviceUsuario.BuscarAutenticado("ju.hernandesmh@gmail.com");

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.Nome);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.Email);
        }

        [Fact(DisplayName = "Validação em buscar autenticado com erro")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task Search_ShouldReturnError()
        {
            _usuarioRepository.Setup(f => f.BuscarUsuarioPorEmail("")).ReturnsAsync((UsuarioBuscarAutenticadoResposta?)null);

            Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.BuscarAutenticado(""));
        }

        [Fact(DisplayName = "Validação de listagem de usuários")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task List_ShouldReturnList()
        {
            var usuario = new UsuarioListarResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana",
                NivelAcesso = "Admin"
            };
            _usuarioRepository.Setup(l => l.ListarUsuario()).ReturnsAsync(new List<UsuarioListarResposta> { usuario });

            var resultado = await _serviceUsuario.Listar();

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.FirstOrDefault().Nome);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.FirstOrDefault().Email);
            Assert.Equal("Admin", resultado.Data.FirstOrDefault().NivelAcesso);
        }

        [Fact(DisplayName = "Validação de listagem de usuário com filtro nome")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task List_ShouldReturnListFilter()
        {
            var usuario = new UsuarioListarResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana",
                NivelAcesso = "Admin"
            };

            _usuarioRepository.Setup(l => l.ListarUsuario()).ReturnsAsync(new List<UsuarioListarResposta> { usuario });

            var resultado = await _serviceUsuario.Listar("Juliana");

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.FirstOrDefault().Nome);
        }

        [Fact(DisplayName = "Validação de listagem de usuário com filtro email")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task List_ShouldReturnListFilterEmail()
        {
            var usuario = new UsuarioListarResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana"
            };

            _usuarioRepository.Setup(l => l.ListarUsuario()).ReturnsAsync(new List<UsuarioListarResposta> { usuario });

            var resultado = await _serviceUsuario.Listar(null, "ju.hernandesmh@gmail.com", null);

            Assert.True(resultado.Success);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.FirstOrDefault().Email);
        }

        [Fact(DisplayName = "Validação de listagem de usuário com filtro nível de acesso")]
        [Trait("Categoria", "Validação Usuário")]
        public async Task List_ShouldReturnListFilterNivelAcesso()
        {
            var usuario = new UsuarioListarResposta
            {
                NivelAcesso = "Admin",
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana"
            };

            _usuarioRepository.Setup(l => l.ListarUsuario()).ReturnsAsync(new List<UsuarioListarResposta> { usuario });

            var resultado = await _serviceUsuario.Listar(null, null, "Admin");

            Assert.True(resultado.Success);
            Assert.Equal("Admin", resultado.Data.FirstOrDefault().NivelAcesso);
        }

    }
}

