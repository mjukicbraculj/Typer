using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Model;

namespace Typist.Contoller
{
    class UserContoller
    {

        /// <summary>
        /// Method verifies user login using
        /// class UserAcess
        /// </summary>
        /// <param name="username">given username</param>
        /// <param name="password">given password</param>
        /// <returns></returns>
        public static string VerifyLogin(string username, string password)
        {
            User user = new User(username, password);
            return UserAccess.VerifyLogin(user);
        }

        /// <summary>
        /// Methods validates wanted username and password.
        /// Adds new user using
        /// UserAccess class.
        /// </summary>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public static string AddUser(string username, string password)
        {
            if (username.Length > 20 || username.Length < 5)
                return "Username lenght should be between 5 and 20 caracters!";
            else if(password.Length > 20 || username.Length < 5)
                return "Password lenght should be between 5 and 20 caracters!";
            User user = new User(username, password);
            return UserAccess.AddUser(user);
        }


        /// <summary>
        /// Method gets user id by username
        /// </summary>
        /// <param name="username">user username</param>
        /// <returns>user id</returns>
        public static int GetUserId(string username)
        {
            return UserAccess.GetUserId(username);
        }
    }
}
