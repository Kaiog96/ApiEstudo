namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Configurations;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Repository;
    using ApiEstudo.Services;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;
        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = this._userRepository.ValidateCredentials(userCredentials);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);

            this._userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), acessToken, refreshToken);
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var acessToken = token.AcessToken;
            var refreshToken = token.RefreshToken;

            var principal = this._tokenService.GetPrincipalFromExpiredToken(acessToken);

            var username = principal.Identity.Name;

            var user = this._userRepository.ValidateCredentials(username);

            if (user != null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            acessToken = this._tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = this._tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            this._userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), acessToken, refreshToken);
        }

        public bool RevokeToken(string username)
        {
            return this._userRepository.RevokeToken(username);
        }
    }
}
