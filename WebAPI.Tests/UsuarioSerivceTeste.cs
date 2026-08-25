using Core.Entidade;
using Core.Input;
using Core.Output;
using Core.ValueObjects;
using Infra.Exceptions;
using Infra.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Service;


namespace WebAPI.Tests
{
    public class UsuarioSerivceTeste
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepository;
        private readonly Mock<ILogger<UsuarioService>> _logger;
        private readonly UsuarioService _serviceUsuario;
        public UsuarioSerivceTeste()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _logger = new Mock<ILogger<UsuarioService>>();
            _serviceUsuario = new UsuarioService(_usuarioRepository.Object, _logger.Object);
        }

        [Fact(DisplayName = "Criação de Usuario - Sucesso")]
        [Trait("Categoria", "Criacao")]
        public async Task Create_ShouldReturnSuccessMessage()
        {
            var usuarioRequisicao = new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "ju.hernandesmh@gmail.com",
                Senha = "203040#@!Asd"
            };

            _usuarioRepository
                .Setup(r => r.ValidaEmail(usuarioRequisicao.Email))
                .ReturnsAsync((Usuario)null);

            Usuario usuarioAdicionado = null;
            _usuarioRepository
                .Setup(r => r.Add(It.IsAny<Usuario>()))
                .Callback<Usuario>( u =>
                {
                    u.Id = 42; //Id gerado pelo banco
                    usuarioAdicionado = u;
                });

            var retorno = await _serviceUsuario.AdicionarUsuario(usuarioRequisicao, nivelAcesso: NivelAcessoEnum.Admin);

            Assert.True(retorno.Success);
            Assert.Equal("Usuário adicionado com sucesso", retorno.Message);

            Assert.NotNull(usuarioAdicionado);
            Assert.NotEqual(usuarioRequisicao.Senha, usuarioAdicionado.Senha);

            _usuarioRepository.Verify(r => r.ValidaEmail(usuarioRequisicao.Email), Times.Once);
            _usuarioRepository.Verify(r => r.Add(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact(DisplayName = "Criação de Usuario - E-mail existente")]
        [Trait("Categoria", "Criacao")]
        public async Task Create_ShouldReturnErrorMessageForExistingEmail()
        {
            var usuarioRequisicao = new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "ju.hernandesmh@gmail.com",
                Senha = "203040#@!Asd"
            };

            _usuarioRepository
                .Setup(repo => repo.ValidaEmail(usuarioRequisicao.Email))
                .ReturnsAsync(new Usuario());

            await Assert.ThrowsAsync<ExceptionEmailCadastrado>(() => _serviceUsuario.AdicionarUsuario(usuarioRequisicao, nivelAcesso: NivelAcessoEnum.Admin));

            // Garante que o Add nunca foi chamado nesse cenário
            _usuarioRepository.Verify(r => r.Add(It.IsAny<Usuario>()), Times.Never);
        }


        [Fact(DisplayName = "Criação de Usuario - Senha vazia")]
        [Trait("Categoria", "Criacao")]
        public async Task Validator_ShouldReturnPasswordEmptyErrorMessage()
        {
            var usuarioRequisicao = new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "juli@gmail.com",
                Senha = ""
            };


            _usuarioRepository
                .Setup(r => r.ValidaEmail(usuarioRequisicao.Email))
                .ReturnsAsync((Usuario)null);


            await Assert.ThrowsAsync<ExceptionSenhaInvalida>(() => _serviceUsuario.AdicionarUsuario(usuarioRequisicao, nivelAcesso: NivelAcessoEnum.Admin));

            _usuarioRepository.Verify(r => r.Add(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact(DisplayName = "Atualização de Usuario - Sucesso")]
        [Trait("Categoria", "Atualizacao")]
        public async Task Update_ShouldReturnSucess()
        {
            int idUsuario = 1;
            var usuarioAtualizarRequisicao = new UsuarioAtualizarRequisicao
            {
                Email = "ju.menezes@gmail.com",
                Senha = "123",
                NivelAcessoId = 1,
                Nome = "Juli"
            };

            _usuarioRepository
                .Setup(repo => repo.GetById(idUsuario))
                .ReturnsAsync(new Usuario { Email = new Email("email@email.com"), Nome = "" });


            Usuario usuarioAtualizado = null;
            _usuarioRepository
                .Setup(r => r.Update(It.IsAny<Usuario>()))
                .Callback<Usuario>(u =>
                {
                    usuarioAtualizado = u;
                });

            var retorno = await _serviceUsuario.AtualizarUsuario(usuarioAtualizarRequisicao, idUsuario);

            Assert.True(retorno.Success);
            Assert.Equal("Usuário atualizado com sucesso", retorno.Message);

            Assert.NotNull(usuarioAtualizado);
            Assert.Equal(usuarioAtualizarRequisicao.Nome, usuarioAtualizado.Nome);
            Assert.Equal(usuarioAtualizarRequisicao.Email, usuarioAtualizado.Email.Endereco);
            Assert.NotEqual(usuarioAtualizarRequisicao.Senha, usuarioAtualizado.Senha);

            _usuarioRepository.Verify(r => r.GetById(idUsuario), Times.Once);
            _usuarioRepository.Verify(r => r.Update(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact(DisplayName = "Atualização de Usuario - Usuário Inexistente")]
        [Trait("Categoria", "Atualizacao")]
        public async Task Update_ShouldReturnErrorMessage()
        {

            int idInexistente = 3;
            var usuarioAtualizarRequisicao = new UsuarioAtualizarRequisicao();

            _usuarioRepository
                .Setup(repo => repo.GetById(idInexistente))
                .ReturnsAsync((Usuario?)null);

            await Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.AtualizarUsuario(usuarioAtualizarRequisicao, idInexistente));

            _usuarioRepository.Verify(repo => repo.GetById(idInexistente), Times.Once);
            _usuarioRepository.Verify(repo => repo.Update(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact(DisplayName = "Busca Usuario por Id - Sucesso")]
        [Trait("Categoria", "Busca")]
        public async Task GetById_ShouldReturnValueSuccess()
        {
            int idExistente = 3;
            var usuarioEncontrado = new UsuarioBuscarPorIdResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana"
            };


            _usuarioRepository
                .Setup(f => f.BuscarUsuarioPorId(idExistente))
                .ReturnsAsync(usuarioEncontrado);

            var resultado = await _serviceUsuario.BuscarPorId(idExistente);

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.Nome);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.Email);

            _usuarioRepository.Verify(repo => repo.BuscarUsuarioPorId(idExistente), Times.Once);
        }

        [Fact(DisplayName = "Busca Usuario por Id - Usuário Inexistente")]
        [Trait("Categoria", "Busca")]
        public async Task GetById_ShouldReturnValueError()
        {
            int idInexistente = 3;
            _usuarioRepository
                .Setup(f => f.BuscarUsuarioPorId(idInexistente))
                .ReturnsAsync((UsuarioBuscarPorIdResposta?)null);

            await Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.BuscarPorId(idInexistente));
            _usuarioRepository.Verify(f => f.BuscarUsuarioPorId(idInexistente), Times.Once);
        }

        [Fact(DisplayName = "Deleta Usuário - Sucesso")]
        [Trait("Categoria", "Delecao")]
        public async Task Delete_ShouldReturnDelete()
        {
            int idUsuarioDeletado = 3;
            var usuario = new Usuario
            {
                Nome = "Juliana",
                Email = new Email("ju.hernandesmh@gmail.com")
            };

            _usuarioRepository
                .Setup(f => f.GetById(idUsuarioDeletado))
                .ReturnsAsync(usuario);

            Usuario usuarioDeletado = null;
            _usuarioRepository
                .Setup(r => r.Update(It.IsAny<Usuario>()))
                .Callback<Usuario>(u =>
                {
                    usuarioDeletado = u;
                });


            var resultado = await _serviceUsuario.RemoverUsuario(idUsuarioDeletado);

            Assert.True(resultado.Success);
            Assert.Equal("Deletado com sucesso", resultado.Message);

            Assert.NotNull(usuarioDeletado);
            Assert.False(usuarioDeletado.Ativo);

            _usuarioRepository.Verify(repo => repo.GetById(idUsuarioDeletado), Times.Once);
            _usuarioRepository.Verify(repo => repo.Update(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact(DisplayName = "Deleta Usuário - Erro usuário não encontrado")]
        [Trait("Categoria", "Delecao")]
        public async Task Delete_ShouldReturnError()
        {
            int idInexistente = 3;
            _usuarioRepository
                .Setup(f => f.GetById(idInexistente))
                .ReturnsAsync((Usuario?)null);

            await Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.RemoverUsuario(idInexistente));

            _usuarioRepository.Verify(repo => repo.GetById(idInexistente), Times.Once);
        }

        [Fact(DisplayName = "Busca usuário para Autenticação - Sucesso")]
        [Trait("Categoria", "Busca")]
        public async Task Search_ShouldReturnUser()
        {
            var usuario = new UsuarioBuscarAutenticadoResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana"
            };

            _usuarioRepository
                .Setup(f => f.BuscarUsuarioPorEmail(usuario.Email))
                .ReturnsAsync(usuario);

            var resultado = await _serviceUsuario.BuscarAutenticado(usuario.Email);

            Assert.True(resultado.Success);
            Assert.Equal("Juliana", resultado.Data.Nome);
            Assert.Equal("ju.hernandesmh@gmail.com", resultado.Data.Email);

            _usuarioRepository.Verify(repo => repo.BuscarUsuarioPorEmail(usuario.Email), Times.Once);
        }

        [Fact(DisplayName = "Busca usuário para Autenticação - Usuário não encontrado")]
        [Trait("Categoria", "Busca")]
        public async Task Search_ShouldReturnError()
        {     
            var usuario = new UsuarioBuscarAutenticadoResposta
            {
                Email = "ju.hernandesmh@gmail.com",
                Nome = "Juliana"
            };

            _usuarioRepository
                .Setup(f => f.BuscarUsuarioPorEmail(usuario.Email))
                .ReturnsAsync((UsuarioBuscarAutenticadoResposta?)null);

            await Assert.ThrowsAsync<ExcepetionUsuarioNaoEncontrado>(() => _serviceUsuario.BuscarAutenticado(usuario.Email));

            _usuarioRepository.Verify(repo => repo.BuscarUsuarioPorEmail(usuario.Email), Times.Once);
        }

        [Fact(DisplayName = "Listar Usuários - Sucesso total")]
        [Trait("Categoria", "Listagem")]
        public async Task List_ShouldReturnList()
        {
            List<UsuarioListarResposta> retorno = [
                new() {Id = 1, Nome="Marcos", Email= "marcos@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 2, Nome="Juliana", Email= "juliana@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 3, Nome="Murilo", Email= "murilo@gmail.com",NivelAcesso = "Usuário"},
                new() {Id = 4, Nome="Jose", Email= "jose@gmail.com",NivelAcesso = "Usuário"}
            ];

            _usuarioRepository
                .Setup(l => l.ListarUsuario())
                .ReturnsAsync(retorno);

            var resultado = await _serviceUsuario.Listar();

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);
            Assert.Equal(4, resultado.Data.Count);

            _usuarioRepository.Verify(r => r.ListarUsuario(), Times.Once);
        }

        [Fact(DisplayName = "Listar Usuários - Filtragem por nome")]
        [Trait("Categoria", "Listagem")]
        public async Task List_ShouldReturnListFilter()
        {
            List<UsuarioListarResposta> retorno = [
                new() {Id = 1, Nome="Marcos", Email= "marcos@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 2, Nome="Juliana", Email= "juliana@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 3, Nome="Murilo", Email= "murilo@gmail.com",NivelAcesso = "Usuário"},
                new() {Id = 4, Nome="Jose", Email= "jose@gmail.com",NivelAcesso = "Usuário"},
                new() {Id = 5, Nome="Juliana", Email= "juliana2@gmail.com",NivelAcesso = "Usuário"},
            ];

            _usuarioRepository
                .Setup(l => l.ListarUsuario())
                .ReturnsAsync(retorno);

            var resultado = await _serviceUsuario.Listar("juliana");
            
            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);

            Assert.All(resultado.Data, u => Assert.Equal("Juliana", u.Nome));
            Assert.Equal(2, resultado.Data.Count);

            _usuarioRepository.Verify(r => r.ListarUsuario(), Times.Once);
        }

        [Fact(DisplayName = "Listar Usuários - Filtragem por email")]
        [Trait("Categoria", "Listagem")]
        public async Task List_ShouldReturnListFilterEmail()
        {
            List<UsuarioListarResposta> retorno = [
                new() {Id = 1, Nome="Marcos", Email= "marcos@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 2, Nome="Juliana", Email= "juliana@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 3, Nome="Murilo", Email= "murilo@gmail.com",NivelAcesso = "Usuário"},
                new() {Id = 4, Nome="Jose", Email= "jose@gmail.com",NivelAcesso = "Usuário"}
            ];

            _usuarioRepository
                .Setup(l => l.ListarUsuario())
                .ReturnsAsync(retorno);

            var resultado = await _serviceUsuario.Listar(null, "juliana@gmail.com", null);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);

            var usuarioFiltrado = Assert.Single(resultado.Data);
            Assert.Equal(2, usuarioFiltrado.Id);
            Assert.Equal("juliana@gmail.com", usuarioFiltrado.Email);
            Assert.Equal("Juliana", usuarioFiltrado.Nome);
        }

        [Fact(DisplayName = "Listar Usuários - Filtragem nível de acesso")]
        [Trait("Categoria", "Listagem")]
        public async Task List_ShouldReturnListFilterNivelAcesso()
        {
            List<UsuarioListarResposta> retorno = [
                new() {Id = 1, Nome="Marcos", Email= "marcos@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 2, Nome="Juliana", Email= "juliana@gmail.com",NivelAcesso = "Admin"},
                new() {Id = 3, Nome="Murilo", Email= "murilo@gmail.com",NivelAcesso = "Usuário"},
                new() {Id = 4, Nome="Jose", Email= "jose@gmail.com",NivelAcesso = "Usuário"}
            ];

            _usuarioRepository
                .Setup(l => l.ListarUsuario())
                .ReturnsAsync(retorno);

            var resultado = await _serviceUsuario.Listar(null, null, "Admin");


            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);
            Assert.Equal(2, resultado.Data.Count);
            Assert.All(resultado.Data, u => Assert.Equal("Admin", u.NivelAcesso));
        }

    }
}

