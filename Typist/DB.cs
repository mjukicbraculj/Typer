using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Typist
{
    static class DB
    {
        static string connectionString = "Data Source=MyDatabase.sqlite;Version=3;";

        public static void Prepare()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    string createUsers = @"create table if not exists users(
                                        id integer primary key autoincrement,
                                        username varchar(20) not null,
                                        password varchar(255) not null,
                                        created timestamp default current_timestamp)";
                    SQLiteCommand command = new SQLiteCommand(createUsers, connection);
                    command.ExecuteNonQuery();
                    string createLessons = @"create table if not exists lessons(
                                        id integer primary key autoincrement,
                                        name varchar(30) not null unique,
                                        parent varchar(30) not null,
                                        created timestamp default current_timestamp)";
                    SQLiteCommand command1 = new SQLiteCommand(createLessons, connection);
                    command1.ExecuteNonQuery();
                    string createTexts = @"create table if not exists texts(
                                        id integer primary key autoincrement,
                                        lessonId integer not null,
                                        text varchar(1000) not null,
                                        created timestamp default current_timestamp,
                                        foreign key(lessonId) references lessons(id))";
                    SQLiteCommand command2 = new SQLiteCommand(createTexts, connection);
                    command2.ExecuteNonQuery();
                    string createLessonDetails = @"create table if not exists lessondetails(
                                                id integer primary key autoincrement,
                                                lessonId integer not null,
                                                userId integer not null,
                                                speed real not null,
                                                errors integer not null,
                                                time varchar(30) not null,
                                                created timestamp default current_timestamp,
                                                foreign key(lessonId) references lessons(id),
                                                foreign key(userId) references users(id))";
                    SQLiteCommand command3 = new SQLiteCommand(createLessonDetails, connection);
                    command3.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //error log
                }

            }
        }

        public static void Drop()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                string createUsers = @"drop table users";
                SQLiteCommand command = new SQLiteCommand(createUsers, connection);
                command.ExecuteNonQuery();
            }
        }


        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            return connection;
        }
    }
}
