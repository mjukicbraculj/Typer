using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typist.Objects
{
    class User
    {
        public int Id { get; set; }
        private string username;
        private string password;
        public string Username 
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password 
        {
            get 
            {
                return password;
            }
            set
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                password = System.Text.Encoding.ASCII.GetString(data);
            }
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        
    }

}
