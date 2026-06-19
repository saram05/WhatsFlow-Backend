using WhatsFlow.Domain.Entities;
using WhatsFlow.Application.DTOs.Auth;
using WhatsFlow.Application.Interfaces;

namespace WhatsFlow.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var token = _tokenService.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                UserName = user.Name,
                Role = user.Role.Name
            };
        }

        public async Task<User> RegisterAsync(RegisterUserRequest request)
        {
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing is not null)
                throw new InvalidOperationException("El email ya está registrado.");

            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role is null)
                throw new InvalidOperationException("El rol no existe.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = User.Create(request.Name, request.Email, passwordHash, request.RoleId);

            await _userRepository.AddAsync(user);

            return user;
        }
    }
}

