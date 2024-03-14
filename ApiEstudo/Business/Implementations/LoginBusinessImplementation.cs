namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Configurations;
    using ApiEstudo.Data.VO;
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
            var user = _userRepository.ValidateCredentials(userCredentials);

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

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), acessToken, refreshToken);
        }
    }
}
