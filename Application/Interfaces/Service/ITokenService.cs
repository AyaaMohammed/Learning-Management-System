using Application.DTOs.Auth;
using Application.Results;

namespace Application.Interfaces.Service
{
    public interface ITokenService
    {
        Task<Result<TokenResponse>> CreateTokenAsync(UserInfoDTO userInfo);
    }
}

