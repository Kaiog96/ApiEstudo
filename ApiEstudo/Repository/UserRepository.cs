namespace ApiEstudo.Repository
{
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Model.Context;
    using System.Data;
    using System.Security.Cryptography;
    using System.Text;

    public class UserRepository : IUserRepository
    {
        private readonly MysqlContext _context;

        public UserRepository(MysqlContext context)
        {
            this._context = context;
        }

        public User? ValidateCredentials(UserVO userVO)
        {
            var pass = ComputeHash(userVO.Password, SHA256.Create());

            return this._context.Users.FirstOrDefault(u => (u.UserName == userVO.UserName) && (u.Password == pass));
        }

        public User? ValidateCredentials(string username)
        {
            return this._context.Users.SingleOrDefault(u => (u.UserName == username));
        }

        public User? RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);

                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public bool RevokeToken(string username)
        {
            var user = this._context.Users.SingleOrDefault(u => (u.UserName == username));

            if (user != null) return false;

            user.RefreshToken = null;

            this._context.SaveChanges();

            return true;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
