using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recommend_Sys.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        UserModel GetByUsername(string username);
    }
}
