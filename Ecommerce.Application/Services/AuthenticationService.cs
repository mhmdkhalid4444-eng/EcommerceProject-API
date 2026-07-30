using Ecommerce.Application.Common;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken cancellationToken = default)
                => await _identityService.EmailExistsAsync(email, cancellationToken);


        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = await _identityService.FindByEmailAsync(email, cancellationToken);

            if (!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);

            var user = result.data;
            var rolesResult = await _identityService.GetRolesAsync(email, cancellationToken);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);
            var roles = rolesResult.data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            return Result<UserDto>.Ok(new UserDto { DisplayName = user.DisplayName, Email = user.Email, Token = token });

        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = await _identityService.GetAddressByEmailAsync(email, cancellationToken);
            if (!result.IsSuccess)
                return Result<AddressDto>.Fail(result.Errors);
            return Result<AddressDto>.Ok(result.data);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
        {

            var userResult = await _identityService.FindByEmailAsync(loginDto.Email, cancellationToken);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

 
            var passwordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, cancellationToken);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email or Password"));

            var rolesResult = await _identityService.GetRolesAsync(loginDto.Email, cancellationToken);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);
            var roles = rolesResult.data;
            var user = userResult.data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            return Result<UserDto>.Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
        {
            var result = await _identityService.CreateUserAsync(registerDto, cancellationToken);
            if (!result.IsSuccess || result.data is null)
                return Result<UserDto>.Fail(result.Errors);

            return Result<UserDto>.Ok(new UserDto { Email = result.data.Email, DisplayName = result.data.DisplayName, Token = "Token" });

        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken cancellationToken = default)
                => await _identityService.UpSertAddressAsync(email, addressDto, cancellationToken);
    }
}
