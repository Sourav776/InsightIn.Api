using InsightIn.Api.Helpers;
using InsightIn.Api.Interfaces;
using InsightIn.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _userList = new();
        public bool SignUp(User model)
        {
            try
            {
                var salt = HashingHelper.GenerateSalt();
                var passwordHash = HashingHelper.HashUsingPbkdf2(model.Password, salt);
                model.PasswordSalt = salt;
                model.HashPassword = passwordHash;
                _userList.Add(model);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public object LogIn(User model)
        {
            var customer = _userList.SingleOrDefault(x => x.UserName == model.UserName);

            if (customer == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(model.Password, customer.PasswordSalt);

            if (customer.HashPassword != passwordHash)
            {
                return null;
            }
            var token = TokenHelper.GenerateToken(customer);
            var obj = new 
            {
                Username = customer.UserName,
                Token = token
            };
            return obj;
        }
    }
}
