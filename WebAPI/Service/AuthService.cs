using Core.Entidade;
using Core.Output;
using Core.Repository;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Interface;

namespace WebAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INivelAcessoRepository _nivelAcessoRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, INivelAcessoRepository nivelAcessoRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _nivelAcessoRepository = nivelAcessoRepository;
            _configuration = configuration;
        }

        public async Task<AuthReturn?> Login(string email, string senha)
        {
            Usuario? usuario = await _usuarioRepository.ValidaEmailSenha(email, senha);

            if (usuario == null) return null;

            NivelAcesso acesso = await _nivelAcessoRepository.GetById(usuario.NivelAcessoId);


            string jwt = GerarJWT(usuario.Email, acesso.Nome);


            AuthReturn auth = new AuthReturn
            {
                NomeUsuario = usuario.Nome,
                Token = jwt
            };


            return auth;
        }

        private string GerarJWT(string username, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
