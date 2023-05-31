using INFORCE_.NET_TASK.DataDomain.Database;
using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model;
using INFORCE_.NET_TASK.Services.Model.Configuration;
using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Model.ViewModel;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace INFORCE_.NET_TASK.Services.Services
{
    public class AuthService : DbService<AppDBContext>, IAuthService
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserService _userService;
        public AuthService(DbContextOptions<AppDBContext> dbContextOptions, 
            IUserService userService, 
            TokenConfiguration tokenConfiguration) : base(dbContextOptions)
        {
            _userService = userService;
            _tokenConfiguration = tokenConfiguration;
        }

        public async Task<TokenViewModel> LoginAsync(UserLoginInputModel inputModel)
        {
            var user = await _userService.GetUserByNameAsync(inputModel.Name);

            if (user == null)
                throw new Exception("User doesn't exist!");

            if (!VerifyPasswordHash(inputModel.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Password is incorrect!");

            var token = new TokenViewModel()
            {
                AccessToken = CreateAccessToken(user),
                UserRole = user.Role,
            };

            return token;
        }

        public async Task<UserViewModel> RegisterAsync(UserRegistrationInputModel inputModel)
        {
            if (await _userService.IsUserExistsAsync(inputModel.Name))
                throw new Exception("User is already registered!");

            if (inputModel.Password != inputModel.ConfirmPassword)
                throw new Exception("Both of passwords must be equal!");

            CreatePasswordHash(inputModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userModel = new UserEntity()
            {
                Name = inputModel.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
            };

            await _userService.CreateAsync(userModel);

            var viewModel = userModel.MapTo<UserViewModel>();
            return viewModel;
        }

        #region Private
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateAccessToken(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion
    }
}
