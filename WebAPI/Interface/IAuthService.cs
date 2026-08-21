using Core.Output;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthReturn>> Login(string email, string password);
    }
}
