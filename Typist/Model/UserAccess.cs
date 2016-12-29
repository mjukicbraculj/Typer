using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Typist.Objects;

namespace Typist.Data
{
    class UserAccess
    {

        static public string AddUser(User user)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    string selectUsername = @"select username from users 
                                                where username like '" + user.Username + "'";
                    SQLiteCommand command = new SQLiteCommand(selectUsername, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        return "Username is already in use!";
                    string insertUser = @"insert into users(username, password)
                                            values ('" + user.Username + "', '" + user.Password + "')";
                    SQLiteCommand command1 = new SQLiteCommand(insertUser, connection);
                    command1.ExecuteNonQuery();
                }
                catch(Exception)
                {
                    return "Unknown database error!";
                }
                return null;
            }
        }

        static public string VerifyLogin(User user)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    string selectUsername = @"select * from users
                                               where username like '" + user.Username + "'";
                    SQLiteCommand command = new SQLiteCommand(selectUsername, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["password"].ToString().Equals(user.Password))
                            return null;
                        else
                            return "Password or username is not correct!";
                    }
                    else
                    {
                        return "Password or username is not correct!";
                    }
                }
                catch(Exception)
                {
                    return "Unknown database error!";
                }
            }
        }

        static public int GetUserId(string username)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    string selectId = @"select id from users
                                               where username like '" + username + "'";
                    SQLiteCommand command = new SQLiteCommand(selectId, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return Convert.ToInt32(reader["id"]);
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
    }
}
