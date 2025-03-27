﻿using Assignment_Q3_2.Models;
using Assignment_Q3_2.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Assignment_Q3_2.DTOs.AuthDTOs;

namespace Assignment_Q3_2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<string> RegisterUserAsync(RegisterDTO registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Username))
                throw new ArgumentException("Username cannot be empty.");

            if (registerDto.Username.Length < 3)
                throw new ArgumentException("Username must be at least 3 characters long.");

            if (char.IsDigit(registerDto.Username[0]))
                throw new ArgumentException("Username cannot start with a number.");

            if (await _userRepository.UserExistsAsync(registerDto.Username))
                throw new ArgumentException("Username already exists.");

            var hashedPassword = _passwordHasher.HashPassword(null, registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = hashedPassword,
                Role = registerDto.Role
            };

            await _userRepository.AddUserAsync(user);
            return "User registered successfully!";
        }

        public async Task<(string token, string message)> LoginUserAsync(LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Username and password are required.");

            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginDto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJwtToken(user);
            return (token, "Login successful");
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _userRepository.UserExistsAsync(username);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException("GetAllUsers method needs to be implemented in UserRepository first");
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
