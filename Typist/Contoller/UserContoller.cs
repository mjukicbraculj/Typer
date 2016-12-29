using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Data;

namespace Typist.Contoller
{
    class UserContoller
    {

        public static string VerifyLogin(string username, string password)
        {
            User user = new User(username, password);
            return UserAccess.VerifyLogin(user);
        }

        public static string AddUser(string username, string password)
        {
            if (username.Length > 20 || username.Length < 5)
                return "Username lenght should be between 5 and 20 caracters!";
            else if(password.Length > 20 || username.Length < 5)
                return "Password lenght should be between 5 and 20 caracters!";
            User user = new User(username, password);
            return UserAccess.AddUser(user);
        }

        public static int GetUserId(string username)
        {
            return UserAccess.GetUserId(username);
        }
    }
}
