namespace ApiEstudo.Services
{
    using System.Security.Claims;

    public interface ITokenService
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
