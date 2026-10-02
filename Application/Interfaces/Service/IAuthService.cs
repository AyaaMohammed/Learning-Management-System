using Application.DTOs.Auth;
using Application.Results;

namespace Application.Interfaces.Service
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> AuthenticateAsync(string userName,string password);
        Task<Result<TokenResponse>> CreateTokenAsync(UserInfoDTO userInfo);
    }
}

