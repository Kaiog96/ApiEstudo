namespace ApiEstudo.Repository
{
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;

    public interface IUserRepository
    {
        User ValidateCredentials(UserVO userVO);
    }
}
