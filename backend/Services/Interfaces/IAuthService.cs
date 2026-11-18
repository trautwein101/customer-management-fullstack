using CustomerManagement.Api.DTOs;

namespace CustomerManagement.Api.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto? Login(LoginDto loginDto);

    }
}
