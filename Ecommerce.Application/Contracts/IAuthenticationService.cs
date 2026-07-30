using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);
        Task<Result<bool>> CheckEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken cancellationToken = default);
        Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken cancellationToken = default);
        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken cancellationToken = default);

    }
}
