using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Data
{
    public class IUserRepository
    {
        internal Task<object?> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        internal Task<object?> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public interface IAuthRepository
        {
        Task<ServiceResponse<int>>Register(User user, string password);
        Task<ServiceResponse<string>>Login(string username, string password);
        Task<bool> UserExists(string username);
        }
    }
}