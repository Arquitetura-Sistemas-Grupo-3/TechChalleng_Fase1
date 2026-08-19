using BC = BCrypt.Net.BCrypt;
using Core.Entidade;
using Core.Input;
using Infra.Repository;
using WebAPI.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Infra.Exceptions;
using Core.Output;
using Microsoft.AspNetCore.Identity;
using Core.Entidade;

namespace WebAPI.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioReturn>> GetAll(string? Nome = null, string? Email = null, string? NivelAcesso = null)
        {
            
            var usuario = await _usuarioRepository.GetAllUsuario();

            if (usuario == null)
                throw new Exception("Nenhum usuário encontrado");

            if (!string.IsNullOrWhiteSpace(Nome))
                usuario = usuario
                    .Where(u => u.Nome.Contains(Nome, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(Email))
                usuario = usuario
                    .Where(u => u.Email.Contains(Email, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(NivelAcesso))
                usuario = usuario
                    .Where(u => u.NivelAcesso.Contains(NivelAcesso, StringComparison.OrdinalIgnoreCase))
                    .ToList();


            return usuario;
        }

        public async Task<string> AddUsuario(UsuarioInput usuarioInput)
        {
          
            var usuario = await _usuarioRepository.ValidaEmail(usuarioInput.Email);

            if(usuario != null)
                throw new ExceptionEmailCadastrado("E-mail já cadastrado");

            if (usuarioInput.NivelAcessoId >= 3)
                throw new ExceptionNivelAcessoInvalido("Nível de acesso inválido");

            try
            {                     
                usuario = Usuario.AddUsuario(usuarioInput);

                _usuarioRepository.Add(usuario);

                return "Usuário adicionado com sucesso";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex}");
            }   
         
        }
        public async Task<UsuarioReturn?> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
                throw new ExcepetionUsuarioNaoEncontrado("Usuário não encontrado");

            return usuario;
        }

        public async Task<string> UpdateUsuario(UsuarioUpdate usuarioUpdate, int id)
        {
            Usuario usuario = await _usuarioRepository.GetById(id);

            if (usuario == null) throw new ExcepetionUsuarioNaoEncontrado("Usuário não encontrado");

            string? password;

            if (!string.IsNullOrEmpty(usuarioUpdate.Senha)) password = BC.HashPassword(usuarioUpdate.Senha);
            else password = null;

            usuario.Atualizar(usuario,usuarioUpdate, password);

            _usuarioRepository.Update(usuario);

            return "Usuário atualizado com sucesso";
        }

        public async Task<string> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            
            if (usuario == null)
                throw new ExcepetionUsuarioNaoEncontrado("Usuário não encontrado");
           
            usuario.Desativar();
            _usuarioRepository.Update(usuario);

            return "Deletado com sucesso";
        }

        public async Task<UsuarioReturn?> GetMe(string email)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmail(email);

            if (usuario == null)
                throw new ExcepetionUsuarioNaoEncontrado("Usuário não encontrado");

            return usuario;
        }
    }
}
