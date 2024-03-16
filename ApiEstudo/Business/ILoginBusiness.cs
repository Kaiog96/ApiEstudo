namespace ApiEstudo.Business
{
    using ApiEstudo.Data.VO;

    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userVO);

        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string username);
    }
}
