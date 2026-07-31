using BC = BCrypt.Net.BCrypt;
using Core.Entidade;
using Core.Input;
using Infra.Repository;
using WebAPI.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Infra.Exceptions;

namespace WebAPI.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAll()
        {
            
            var usuario = await _usuarioRepository.GetAll();

            if (usuario == null)
                throw new Exception("Nenhum usuário encontrado");

            return usuario;
        }

        public string AddUsuario(UsuarioInput usuarioInput)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nome = usuarioInput.Nome,
                    Email = usuarioInput.Email,
                    Senha = BC.HashPassword(usuarioInput.Senha),
                    NivelAcessoId = usuarioInput.NivelAcessoId
                };

                _usuarioRepository.Add(usuario);

                return "Usuário adicionado com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao adicionar usuário";
            }
        }
        public async Task<Usuario?> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                throw new ExcepetionUsuarioNaoEncontrado("Usuário não encontrado");

            return usuario;
        }

        public async Task<string> UpdateUsuario(UsuarioUpdate usuarioUpdate)
        {
            Usuario usuario = await _usuarioRepository.GetById(usuarioUpdate.Id);

            if (usuario == null) throw new Exception("Usuário não encontrado");
            
            var password = BC.HashPassword(usuarioUpdate.Senha);

            usuario.Atualizar(usuarioUpdate, password);

            _usuarioRepository.Update(usuario);

            return "Usuário atualizado com sucesso";
        }

        public async Task<string> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            usuario.Desativar();
            _usuarioRepository.Update(usuario);

            return "Deletado com sucesso";
        }
    }
}
