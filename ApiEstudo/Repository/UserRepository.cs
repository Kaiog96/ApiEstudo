namespace ApiEstudo.Repository
{
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Model.Context;
    using System.Security.Cryptography;
    using System.Text;

    public class UserRepository : IUserRepository
    {
        private readonly MysqlContext _context;

        public UserRepository(MysqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO userVO)
        {
            var pass = ComputeHash(userVO.Password, new SHA256CryptoServiceProvider());

            return _context.Users.FirstOrDefault(u => (u.UserName == userVO.UserName) && (u.Password == pass));
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
