namespace ApiEstudo.Repository
{
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;

    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO userVO);

        User? ValidateCredentials(string username);

        bool RevokeToken(string username);

        User? RefreshUserInfo(User user);
    }
}
