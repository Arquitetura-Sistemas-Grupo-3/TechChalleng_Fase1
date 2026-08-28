using Core.Output;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<AutenticarResposta>> Autenticar(string email, string senha);
    }
}
