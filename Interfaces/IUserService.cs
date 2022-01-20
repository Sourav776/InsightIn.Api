using InsightIn.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Interfaces
{
    public interface IUserService
    {
        bool SignUp(User model);
        object LogIn(User model);
    }
}
